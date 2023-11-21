using System;
using System.IO;
using _Game._Scripts;
using Ipfs;
// using Ipfs.Engine;
// using Ipfs;
// using Ipfs.Http;
using Sirenix.OdinInspector;
using UnityEngine;

using UnityEngine.Video;


public class IpfsTest : MonoBehaviour
{
    [SerializeField]
    private string video = "http://195.24.65.200:8081/ipns/k2k4r8ngp9yf0hv8vvmjwlpjeucb564zb7i4bdtahri2ejeb2z8sa6eg";

    // private IpfsClient ipfs = new IpfsClient() { ApiUri = new Uri("http://195.24.65.200:8081/") };
    [SerializeField] private VideoPlayer videoPlayer;

    [Button]
    async void Start()
    {
        using (var ipfs = new IpfsClient( new Uri(Config.SERVER_IPFS_DOWNLOAD)))
        {
            //Name of the file to add
            // string fileName = "test.txt";
           // Debug.LogError(await  ipfs.Config.Show());
            //Wrap our stream in an IpfsStream, so it has a file name.
            // IpfsStream inputStream = new IpfsStream(fileName, File.OpenRead(fileName));
        
        //     MerkleNode node = await ipfs.Add(inputStream);
        // Debug.LogError(node.Hash.ToString());
            Stream outputStream = await ipfs.Cat("QmdZLtkAaGRU5bQLRxxwEdMtoPiACJQLkEeNqpfG2Hr3LK");
        
            using(StreamReader sr = new StreamReader(outputStream))
            {
                string contents = sr.ReadToEnd();
        
                Debug.LogError(contents); //Contents of test.txt are printed here!
            }
        }
    }

    
    //
    // private void Handler(IPublishedMessage obj)
    // {
    //     Debug.Log(obj.Sender);
    // }
    //
    // async void Update()
    // {
    //     await _ipfs.PubSub.PublishAsync("test", "Hello! ");
    // }
}