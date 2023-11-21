using System.Threading.Tasks;

public interface IJsonCreator
{
    string Path { get; }
    string CurrentJson { get; }
    void CreateDirectory();
    void CreateDirectory(string str = null);
    string[] GetFile(string pathStr = null);
    Task<string> CreateFolderAndJsonString(string name, string directory = null);
    string SetJsonName(string name, string directory);
    Task SaveFile(string path, string text);
}