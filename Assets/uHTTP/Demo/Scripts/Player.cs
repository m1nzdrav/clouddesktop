using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    public Transform nozzle;
    public GameObject bullet;

    public float speed = 10;
    public float rotationSpeed = 60;
    public bool isKeyboardControllable;

    private bool engineL, engineR;
    public int ammo = 3;
    public float reloadTime = 7;

    public void StartEngineL() { engineL = true; }
    public void StopEngineL() { engineL = false; }
    public void StartEngineR() { engineR = true; }
    public void StopEngineR() { engineR = false; }

    private int lives = 3;

    private Rigidbody r;

    private GameManager gameManager;

    public void FireTankGun() {
        if(ammo <= 0){
            return;
        }
        Instantiate(bullet, nozzle.position, Quaternion.Euler(0, transform.eulerAngles.y, 0));
        ammo--;
        this.DoDelayed(() => ammo++, reloadTime);
    }

    public void Damage() {
        lives--;
        if(lives <= 0) {
            Destroy(gameObject);
            gameManager.OnGameOver();
        }
    }

    private void ConstrainToStage() {
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, gameManager.stage.center.x - gameManager.stage.size.x / 2, gameManager.stage.center.x + gameManager.stage.size.x / 2),
            transform.position.y,
            Mathf.Clamp(transform.position.z, gameManager.stage.center.z - gameManager.stage.size.z / 2, gameManager.stage.center.z + gameManager.stage.size.z / 2)
        );
    }

    // Start is called before the first frame update
    void Start()
    {
        r = GetComponent<Rigidbody>();
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(isKeyboardControllable) {
            engineL = Input.GetKey("1");
            engineR = Input.GetKey("3");
            if(Input.GetKeyDown("2")){
                FireTankGun();
            }
        }
        if(engineL && engineR) {
            r.MovePosition(transform.position + transform.forward * speed * Time.fixedDeltaTime);
        }
        else if(engineL) {
            r.MoveRotation(Quaternion.Euler(transform.eulerAngles + Vector3.up * rotationSpeed * Time.fixedDeltaTime));
        }
        else if(engineR) {
            r.MoveRotation(Quaternion.Euler(transform.eulerAngles - Vector3.up * rotationSpeed * Time.fixedDeltaTime));
        }
        else {
            r.velocity = Vector3.zero;
            r.angularVelocity = Vector3.zero;
        }
        ConstrainToStage();
    }
}
