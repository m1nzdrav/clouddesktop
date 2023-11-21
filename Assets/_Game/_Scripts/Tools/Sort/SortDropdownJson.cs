using System.IO;
using System.Text;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using UnityEngine;


public class SortDropdownJson : MonoBehaviour
{
    [SerializeField] private string nameJson;

    [Button]
    public void Sort()
    {
        // JsonSerializer jsonSerializer = JsonSerializer.CreateDefault();
        string main = File.ReadAllText(Application.dataPath + "/" + nameJson + ".json");

        Country country = JsonUtility.FromJson<Country>(main);

        country.countryCodes.Sort((code, countryCode) => code.name.CompareTo(countryCode.name));

        File.WriteAllText(Application.dataPath + "/" + nameJson + ".json", JsonUtility.ToJson(country), Encoding.UTF8);
    }
}