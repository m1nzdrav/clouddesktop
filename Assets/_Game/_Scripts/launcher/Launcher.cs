using System;
using System.Collections;
using System.IO;
// using Ipfs.Http;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


public class Launcher : MonoBehaviour
{
    const string passphrase = "this is not a secure pass phrase";
    // IpfsEngine ipfsEngine;

    // public static IpfsEngine ipfsEngine =new IpfsEngine(passphrase.ToCharArray());

    // private IpfsClient _ipfs = new IpfsClient() {ApiUri = new Uri("http://45.138.26.140:8080/")};

    private String first = "1";

    [SerializeField] private Button launch;

    [SerializeField] private Image loading;

    private Stream readet;


    private async void Start()
    {
      var   fileStream =File.ReadAllText(@"C:\Users\slavk\clouddesktop\bundle.js") ;
        
        // Engine engine=new Engine();
        Debug.LogError("read");
        // engine.Execute(fileStream);
        // JsValue exports = engine.CommonJS().RunMain("./bundle.js");
        
      //   SQLitePCL.Batteries.Init();
      //  
      //   Debug.LogError("start");
      //
      //   ipfsEngine = new IpfsEngine("".ToCharArray());
      //
      //   await ipfsEngine.StartAsync();
      //   Debug.LogError(ipfsEngine);
      //
      //
      //   var a = await ipfsEngine.FileSystem.AddTextAsync("12lopkpk23123");
      // // Cid test=  new Cid();
      // // test.Hash = "base58btc cidv0 dag-pb sha2-256 QmWTsw8XD7EZLztC5G8Ye6xKZ47k7wpG9etyah7svVS3EX";
      //   Debug.LogError(a.Id.Hash);
      //   
      //   Debug.LogError(await ipfsEngine.FileSystem.ReadAllTextAsync("QmQ4ay3mpgp8A214qDBaLuMvjeLf7VWwLWff8ueTpSLN4M"));
      //   // .ReadAllTextAsync("QmWTsw8XD7EZLztC5G8Ye6xKZ47k7wpG9etyah7svVS3EX").Result);
    }

    [Button]
    public async void AddFileToIpfs()
    {
        // var directory = await ipfsEngine.FileSystem.AddDirectoryAsync(@"C:\Users\slavk\Desktop");
        // Debug.LogError(directory.Id.Hash);
    }

    [Button]
    public async void GetFileFromIpfs()
    {
        // IpfsEngine ipfs = new IpfsEngine("".ToCharArray());
        //
        // string message = await ipfs.FileSystem.ReadAllTextAsync("QmWTsw8XD7EZLztC5G8Ye6xKZ47k7wpG9etyah7svVS3EX");
        //
        // Debug.LogError(message);
    }

    IEnumerator Download()
    {
        UnityWebRequest www =
            UnityWebRequest.Get(
                "https://gateway.ipfs.io/ipns/k51qzi5uqu5di8uvbt7fnshrte4dym1v56zoy8jp2750l9m5olnq41t2fo3ib9/Teszt");

        Debug.LogError("Before");

        yield return www.SendWebRequest();

        Debug.LogError("After");

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.LogError(www.error);
        }

        while (www.downloadProgress < 0.9)
        {
            Debug.LogError(www.downloadProgress);
        }

        Debug.LogError(www.downloadHandler.text);
        byte[] results = www.downloadHandler.data;

        File.WriteAllBytes(Application.dataPath + "/sss", results);

        Debug.LogError("End");
    }
}