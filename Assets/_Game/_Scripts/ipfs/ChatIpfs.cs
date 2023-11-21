
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChatIpfs : MonoBehaviour
{
    // private IpfsClient _ipfs = new IpfsClient() {};

    [SerializeField] private TextMeshProUGUI _chatText;

    [SerializeField] private InputField _chatInputField;

    async void Start()
    {
        // await _ipfs.PubSub.SubscribeAsync("test", Handler, new CancellationToken());
        // AddFileOptions testOptions=new AddFileOptions();
        // Debug.LogError(Application.dataPath );
        // var test = await _ipfs.FileSystem.AddDirectoryAsync(Application.dataPath + Config.PATH_TO_MAIN_JSON);
        // Debug.LogError("post");
        // Debug.LogError(test.Id.Encoding);
        // await _ipfs.FileSystem.AddDirectoryAsync(Application.dataPath + Config.PATH_TO_DIRECTORY_JSON);
        // await test;
        // Debug.LogError(test.Result.Id);
        // StartCoroutine(SendFile());
    }

    // private async void Handler(IPublishedMessage obj)
    // {
    //     string txt = Encoding.UTF8.GetString(obj.DataBytes);
    //     _chatText.text += "Other: " + txt + "\n";
    //     _chatText.text = _chatText.text;
    // }

    // Update is called once per frame
    public void SendMsg()
    {
        // _ipfs.PubSub.PublishAsync("test", _chatInputField.text);
        // _chatText.text += _chatInputField.text + "\n";
        // _chatText.text = _chatText.text;
        

        // StartCoroutine(SendFile());
    }

    // IEnumerator SendFile()
    // {
    //     // Debug.LogError("Start");
    //     // var test = await _ipfs.FileSystem.AddFileAsync(Application.dataPath + Config.PATH_TO_MAIN_JSON);
    //     //
    //     // yield return test;
    //     // Debug.LogError(test.Result.IsDirectory);
    // }
}