using System.IO;
using UnityEditor;

public class CreateAssetBundle
{
    [MenuItem("AssetsBundle/Build AssetBundles")]
    static void BuildAllAssetBundles() // Упаковка
    {
        string dirWeb = "AssetBundles/web";
        string dirPC = "AssetBundles/PC";
        // Определяем, существует ли каталог
        if (Directory.Exists(dirPC) == false)
        {
            Directory.CreateDirectory(dirPC); // Создаем каталог AssetBundles в проекте
        }

        if (Directory.Exists(dirWeb) == false)
        {
            Directory.CreateDirectory(dirWeb); // Создаем каталог AssetBundles в проекте
        }

        // Первый параметр - это путь к пакету, второй параметр, параметр сжатия, третий параметр, цель платформы
        BuildPipeline.BuildAssetBundles(dirWeb, BuildAssetBundleOptions.ChunkBasedCompression, BuildTarget.WebGL);
        BuildPipeline.BuildAssetBundles(dirPC, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64);
    }
}