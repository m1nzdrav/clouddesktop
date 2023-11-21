using System;
using _Game._Scripts;
using Cysharp.Threading.Tasks;
// using Ipfs;
// using Ipfs.Http;
// using Ipfs.Engine;
// using Ipfs.Http;
using UnityEngine;

public class IpfsLoader
{
    // IpfsEngine ipfsDownload = new IpfsEngine();
    // private readonly IpfsClient ipfsUpload;
    // private readonly IpfsClient ipfsDownload;

    public IpfsLoader()
    {
        // ipfsDownload;
        
        // ipfsUpload = new IpfsClient();
        // ipfsDownload = new IpfsClient();
        // ipfsUpload = new IpfsClient() { ApiUri = new Uri(Config.SERVER_IPFS_UPLOAD)};
        // ipfsDownload = new IpfsClient() { ApiUri = new Uri(Config.SERVER_IPFS_DOWNLOAD) };
    }

    public async UniTask<string> DownloadIpns(string cidKey)
    {
        // string path = await ipfsDownload.Name.ResolveAsync(cidKey, true);
        // string path = await ipfsDownload.Name.ResolveAsync(cidKey, true);
        // Debug.LogError("getting cid");
        // return await ipfsDownload.FileSystem.ReadAllTextAsync(path);
        // return await ipfsDownload.FileSystem.ReadAllTextAsync(path);
        return null;
    }

    public async UniTask<string> DownloadIpfs(string cidKey)
    {
        // string json = await ipfsDownload.FileSystem.ReadAllTextAsync(cidKey);
        // Debug.LogError("read ipfs file");
        // return json;
        return null;
    }

    public async UniTask<string> UploadText(string nameKey, string fileToUpload, Action<string, string> afterUpload)
    {
        // Cid cidFileToUpload = (await ipfsUpload.FileSystem.AddTextAsync(fileToUpload)).Id;
        // afterUpload.Invoke(cidFileToUpload, nameKey);
        //
        // return cidFileToUpload;
        return null;
    }

    public async UniTask PublishText(string nameKey, string fileToUpload, Action<string, string> afterUpload)
    {
        // Debug.LogError("publish");
        // try
        // {
        //     await ipfsUpload.Key.CreateAsync(nameKey, "ed25519", 3000);
        // }
        // catch (Exception e)
        // {
        //     Debug.LogError("error create key " + e);
        // }
        //
        // Debug.LogError("try add file");
        // var cidFileToUpload = (await ipfsUpload.FileSystem.AddTextAsync(fileToUpload)).Id;
        // Debug.LogError("try publish file");
        // afterUpload.Invoke((await ipfsUpload.Name.PublishAsync(cidFileToUpload, true, nameKey)).NamePath, nameKey);
    }
}