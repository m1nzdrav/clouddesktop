using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public RawWebImageLoader qrCode;
    public NetworkUtility NetworkUtility;
    public StaticWebServer webServer;
    public InboxEndpoint InboxEndpoint;
    public Text lobbyText, gameOverText;
    public GameObject playerPrefab;
    public Bounds stage;

    private GameObject player1, player2;
    private bool isGameRunning = false;

    // Start is called before the first frame update
    void Start()
    {
        InboxEndpoint.postRequestHandler.AddListener((_, msg) => OnMessage(msg));
        string url = "http://" + NetworkUtility.Ip + ":" + webServer.port + "/";
        qrCode.Url = "https://api.qrserver.com/v1/create-qr-code/?format=png&size=150x150&margin=10&data=" + url;
        if(Application.isEditor) {
            // For testing in Editor
            Debug.Log("Please build the game to remote control it from your smartphone.\nUse the number keys 1, 2, 3 to control the tank in the Editor.");
            Debug.Log("Join a second player by visiting: " + url);
            AddPlayer("Editor Player");
            GameObject.FindObjectOfType<Player>().isKeyboardControllable = true;
            lobbyText.transform.parent.gameObject.SetActive(false);
            isGameRunning = true;
        }
    }

    public void OnGameOver() {
        isGameRunning = false;
        this.DoDelayed(() => {
            string winner = FindObjectsOfType(typeof(Player))[0].name;
            gameOverText.text = winner + " wins!";
            gameOverText.gameObject.SetActive(true);
            this.DoDelayed(() => SceneManager.LoadScene(SceneManager.GetActiveScene().name), 5);
        }, 0.5f);
    }

    private void OnMessage(string msg) {
        Debug.Log("Received: " + msg);
        if(msg.StartsWith("new player\n")){
            AddPlayer(msg.Split('\n')[1]);
        }
        else if(!isGameRunning){
            return;
        }
        else {
            GameObject.Find(msg.Split('\n')[0]).SendMessage(msg.Split('\n')[1]);
        }
    }

    private Vector3 GetRandomSpawnpoint() {
        return new Vector3(
            stage.center.x - stage.size.x / 2 + Random.value * stage.size.x,
            0,
            stage.center.z - stage.size.z / 2 + Random.value * stage.size.z);
    }

    private bool AddPlayer(string name) {
        if(player2 != null) {
            return false;
        }
        GameObject player = Instantiate(playerPrefab, GetRandomSpawnpoint(), Quaternion.Euler(0, Random.value * 360, 0));
        player.name = name;
        if(player1 == null) {
            player.transform.Find("Tank").GetComponent<Renderer>().material.color = Color.red;
            player1 = player;
            lobbyText.text = "1/2 Joined";
        }
        else {
            player.transform.Find("Tank").GetComponent<Renderer>().material.color = Color.blue;
            player2 = player;
            lobbyText.text = "2/2 Joined";
            this.DoDelayed(() => {
                lobbyText.transform.parent.gameObject.SetActive(false);
                isGameRunning = true;
            }, 0.5f);
        }
        return true;
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireCube(stage.center, stage.size);
    }
}
