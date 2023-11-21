using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using _Game._Scripts.Panel;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using Random = System.Random;

namespace _Game._Scripts
{
    public class Config : MonoBehaviour
    {
        #region ConstFieldForJson

        public static StringBuilder StringBuilder = new StringBuilder(50);
        public static Random Random = new Random();
        public static IdNameValue IdNameValueName = new IdNameValue();
        public static IdNameValue IdNameValueValue = new IdNameValue();

        #endregion

        #region URL_and_Domain

#if UNITY_WEBGL && !UNITY_EDITOR
        public const string URL_BUNDLE_AUDIO =
            "http://195.24.65.200:8082/ipns/k51qzi5uqu5difoyztaq9i6tqhy2z5lyufjx62ap1mgzc2a2p56mlbxkdkac89/web/audio";

        public const string URL_BUNDLE_MAIN =
            "http://195.24.65.200:8082/ipns/k51qzi5uqu5difoyztaq9i6tqhy2z5lyufjx62ap1mgzc2a2p56mlbxkdkac89/web/main";

        public const string URL_BUNDLE_DEMO =
            "http://195.24.65.200:8082/ipns/k51qzi5uqu5difoyztaq9i6tqhy2z5lyufjx62ap1mgzc2a2p56mlbxkdkac89/web/demo";

        public const string URL_BUNDLE_INVITATION =
            "http://195.24.65.200:8082/ipns/k51qzi5uqu5difoyztaq9i6tqhy2z5lyufjx62ap1mgzc2a2p56mlbxkdkac89/web/invitation";

        public const string URL_BUNDLE_PRESENTATION =
            "http://195.24.65.200:8082/ipns/k51qzi5uqu5difoyztaq9i6tqhy2z5lyufjx62ap1mgzc2a2p56mlbxkdkac89/web/presentation";

        public const string URL_BUNDLE_VIDEO =
            "http://195.24.65.200:8082/ipns/k51qzi5uqu5difoyztaq9i6tqhy2z5lyufjx62ap1mgzc2a2p56mlbxkdkac89/web/video";
        
        public const string URL_BUNDLE_SCENE_MAIN =
            "http://195.24.65.200:8082/ipns/k51qzi5uqu5difoyztaq9i6tqhy2z5lyufjx62ap1mgzc2a2p56mlbxkdkac89/web/scene_main";
        
        public const string URL_BUNDLE_SCENE_DEMO =
            "http://195.24.65.200:8082/ipns/k51qzi5uqu5difoyztaq9i6tqhy2z5lyufjx62ap1mgzc2a2p56mlbxkdkac89/web/scene_demo";

        public const string URL_BUNDLE_SCENE_PRESENTATION =
            "https://globallocalization.com/bundle/web/scene_presentation";

        public const string URL_BUNDLE_FONTS = "http://195.24.65.200:8082/ipns/k51qzi5uqu5difoyztaq9i6tqhy2z5lyufjx62ap1mgzc2a2p56mlbxkdkac89/web/font";

        public const string URL_BUNDLE_SCENE_INVITATION = "https://globallocalization.com/bundle/web/scene_invitation";
#endif
#if !UNITY_WEBGL || UNITY_EDITOR
        public const string URL_BUNDLE_AUDIO =
            "http://195.24.65.200:8082/ipns/k51qzi5uqu5difoyztaq9i6tqhy2z5lyufjx62ap1mgzc2a2p56mlbxkdkac89/PC_Windows/audio";

        public const string URL_BUNDLE_DEMO =
            "http://195.24.65.200:8082/ipns/k51qzi5uqu5difoyztaq9i6tqhy2z5lyufjx62ap1mgzc2a2p56mlbxkdkac89/PC_Windows/demo";

        public const string URL_BUNDLE_INVITATION =
            "http://195.24.65.200:8082/ipns/k51qzi5uqu5difoyztaq9i6tqhy2z5lyufjx62ap1mgzc2a2p56mlbxkdkac89/PC_Windows/invitation";
        
        public const string URL_BUNDLE_MAIN =
            "http://195.24.65.200:8082/ipns/k51qzi5uqu5difoyztaq9i6tqhy2z5lyufjx62ap1mgzc2a2p56mlbxkdkac89/PC_Windows/main";

        public const string URL_BUNDLE_PRESENTATION =
            "http://195.24.65.200:8082/ipns/k51qzi5uqu5difoyztaq9i6tqhy2z5lyufjx62ap1mgzc2a2p56mlbxkdkac89/PC_Windows/presentation";

        public const string URL_BUNDLE_VIDEO =
            "http://195.24.65.200:8082/ipns/k51qzi5uqu5difoyztaq9i6tqhy2z5lyufjx62ap1mgzc2a2p56mlbxkdkac89/PC_Windows/video";

        public const string URL_BUNDLE_FONTS =
            "http://195.24.65.200:8082/ipns/k51qzi5uqu5difoyztaq9i6tqhy2z5lyufjx62ap1mgzc2a2p56mlbxkdkac89/PC_Windows/font";
        
        public const string URL_BUNDLE_SCENE_MAIN =
            "http://195.24.65.200:8082/ipns/k51qzi5uqu5difoyztaq9i6tqhy2z5lyufjx62ap1mgzc2a2p56mlbxkdkac89/PC_Windows/scene_main";

        public const string URL_BUNDLE_SCENE_DEMO =
            "http://195.24.65.200:8082/ipns/k51qzi5uqu5difoyztaq9i6tqhy2z5lyufjx62ap1mgzc2a2p56mlbxkdkac89/PC_Windows/scene_demo";

        public const string URL_BUNDLE_SCENE_PRESENTATION =
            "https://globallocalization.com/bundle/PC/scene_presentation";

        public const string URL_BUNDLE_SCENE_INVITATION = "https://globallocalization.com/bundle/PC/scene_invitation";
#endif
        public const string URL_VIDEO_ACTOR =
            "http://195.24.65.200:8082/ipfs/QmdpBZJZ5U8DbgiHckn47NhzyodhrBsxtKxNozYzjbtx2D";

        public const string URL_VIDEO_NO_ACTOR =
            "http://195.24.65.200:8082/ipfs/QmazN44ptkZ2mwZnRvaZgyRo6JKPyRQ2sox8RghBcY2KWK";

        public const string SERVER_IPFS_UPLOAD = "http://195.24.65.200:5002/";
        public const string SERVER_IPFS_DOWNLOAD = "http://195.24.65.200:8082/";
        public const string URL_RABBIT = "https://globallocalization.com";
        public const string URL_TELEGRAM = "https://t.me/a7772177";
        public const string URL_TELEGRAM_MAIN = "https://t.me/Globallocalization";
        public const string URL_INVITATION = "https://globallocalization.com/invitation";
        public const string URL_SEND_COUNTRY = "https://globallocalization.com/GlobalLocalization/setCountry.php";

        public const string URL_SEND_COUNTRY_CODE =
            "https://globallocalization.com/GlobalLocalization/setCountryCode.php";

        public const string URL_SEND_DESTINATION =
            "https://globallocalization.com/GlobalLocalization/setDestination.php";

        public const string URL_SEND_LINK = "https://globallocalization.com/GlobalLocalization/setLink.php";
        public const string URL_SEND_EMAIL = "https://globallocalization.com/GlobalLocalization/sendEmail.php";
        public const string URL_SET_EMAIL = "https://globallocalization.com/GlobalLocalization/setEmail.php";
        public const string URL_SEND_REF = "https://globallocalization.com/GlobalLocalization/setReferer.php";
        public const string URL_CHECK_LOCKER = "https://globallocalization.com/GlobalLocalization/checkLocker.php";
        public const string URL_GET_LOCKER = "https://globallocalization.com/GlobalLocalization/getLocker.php";
        public const string URL_CHECK_CHECKCODE = "https://globallocalization.com/GlobalLocalization/checkcode.php";
        public const string URL_REF_SAVE = "https://globallocalization.com/GlobalLocalization/saveSelfReferer.php";
        public const string URL_SAVE_LANGUAGE = "https://globallocalization.com/GlobalLocalization/setLanguage.php";
        public const string URL_SET_USERDATA = "https://globallocalization.com/GlobalLocalization/setUserData.php";
        public const string URL_GET_USERDATA = "https://globallocalization.com/GlobalLocalization/getUserData.php";
        public const string URL_REG_USER = "https://globallocalization.com/GlobalLocalization/regUserSendSMS.php";
        public const string URL_READ_FILE = "https://globallocalization.com/GlobalLocalization/readFile.php";

        public const string URL_TEMPLATE_JSON =
            "https://globallocalization.com/StreamingAssets/TemplateJson/TemplateMiniJson.json";

        public const string URL_SEND_MAIN = "https://globallocalization.com/GlobalLocalization/main.php";
        public const string URL_SAVE_FILE = "https://globallocalization.com/GlobalLocalization/saveFile.php";
        public const string URL_CHECK_CHECKCOOKIE = "https://globallocalization.com/GlobalLocalization/checkCookie.php";
        public const string DOMAIN = "www.globallocalization.com/";

        #endregion

        #region JsonName

        public const string SCENE_INVITATION = "SceneInvitation_InvitationFirst_";
        public const string SCENE_TOKENSALE = "SceneTokensale_TokensaleFirst_";

        #endregion

        #region Const

        public const string PREFS_MAIN_JSON_NAME = "NameJson";
        public const string TEMPLATE = @"Box";
        public static Color PINK_COLOR = new Color(0.64f, 0.19f, 0.78f);
        public static Color ANSWER_COLOR = new Color(0.2f, 0.8f, 1f, 0.95f);
        public const float WAIT_LOCKER_SPAWN = 5f;
        public const int CURRENT_YEAR = 2022;
        public const float PAUSE_AFTER_CHANGE_VOLUME = 3f;

        public const float DELAY_FOR_UNLOCK_CHAT = 1.5f;
        public const float DELAY_FOR_SPAWN_ANSWER = 1f;
        public const string STARTER_REF = "a7772166";
        public const string PASSWORD = "a7772177";
        public const string LINK_MAILTO = "mailto:welcometo@globallocalization.com";
        public const string ENGLISH = "ENGLISH";
        public const string KEY_REF = "ref";
        public const string KEY_SELF_REF = "selfRef";
        public const int MAX_COUNT_SOUND_GLOW = 3;

        public const string KEY_LANGUAGE = "language";
        public const string DEFAULT_LANGUAGE = "English";
        // public const string YOUR_PASS = "Ваш пароль ";

        public const string TEXT_PLACEHOLDER = "EnterCodeText";
        public const string DEFAULT_NAME_FOLDER = "New folder";
        public const string DEFAULT_NAME_USER_JSON = "User";
        public static Vector3 HALF_VECTOR_ONE = new Vector3(.5f, .5f, .5f);
        public const string KEY_DISSOLVE_COLOR = "_DissolveColor";
        public const string KEY_DISSOLVE_AMOUNT = "_DissolveAmount";

        public const int COUNT_VALUE_PROFILE = 22;
        public const string KEY_TRUE = "true";
        public const string KEY_ONE = "1";
        public const string KEY_ZERO = "0";
        public const string SEP = " ";

        #endregion

        #region Path

        public const string PATH_TO_MAIN_JSON = "/StreamingAssets/JsonFilesDesktop/Main/";
        public const string PATH_TO_MAIN_RESOURCE_JSON = "JsonFilesDesktop/Main/";
        public const string PATH_TO_DIRECTORY_JSON = "/StreamingAssets/JsonFilesDesktop/";
        public const string PATH_TO_DIRECTORY_USER_JSON = "/StreamingAssets/JsonUserFilesDesktop/";
        public const string PATH_TO_TEMPLATE_JSON = "/StreamingAssets/TemplateJson/TemplateMiniJson.json";
        public const string PATH_TO_LANGUAGE_JSON = "Language/";

        #endregion

        public const float PROP_OF_SELL_SIZE = 2.5f;
        public const int QUEUE_FORM = 1;

        #region JsonConst

        public const string DEFAULT_USER_JSON_MYT_NAME = "MytName";
        public const string DEFAULT_USER_JSON_MYT_ID = "MytID";
        public const string DEFAULT_USER_JSON_GRID_CONTROLLER = "GridControllerInFolderDesktop";
        public const string DEFAULT_USER_JSON_PREFABE_NAME = "PrefabName";
        public const string DEFAULT_USER_JSON_MYT_PREFABE_COLOR_TYPES = "MytPrefabColorTypes";
        public const string DEFAULT_USER_JSON_BACKGROUND = "Background";
        public const string DEFAULT_USER_JSON_COVERS = "Covers";

        #endregion

        #region JsonEntityConst

        public static string JSON_ENTITY_SETTINGS = "Settings";
        public static string JSON_ENTITY_MAIN = "Main";
        public static string JSON_ENTITY_COVER = "Cover";
        public static string JSON_ENTITY_NAME = "Name";
        public static string JSON_ENTITY_SOUND = "Sound";
        public static string JSON_ENTITY_COMMENT = "Comment";
        public static string JSON_ENTITY_PARENT_CHILD = "ParentChild";
        public static string JSON_ENTITY_WHAT = "What";
        public static string JSON_ENTITY_WHO = "Who";
        public static string JSON_ENTITY_WHERE = "Where";
        public static string JSON_ENTITY_WHEN = "When";
        public static string JSON_ENTITY_ATTRIBUTE = "Attributes";
        public static string JSON_ENTITY_EVENTS = "Events";
        public static string JSON_ENTITY_ACTIONS = "Actions";

        #endregion

        #region PoolProperty

        public const int POOL_OBJECT_SIZE = 4;
        public const int POOL_GRID_CELL_SIZE = 105;
        public const int POOL_FORM_COMPONENT_SIZE_ = 105;
        public const int LAST_NUMBER_HIERARCHY = 2;

        #endregion

        public const float DELAY_FOR_SHOWING = 0.5f;
        public const float DELAY_FOR_HIDE_CURSOR = 3.5f;

        #region CircleAnimation

        public static Vector3 VECTOR_ANIMATION_LAMP_ENTER = new Vector3(1, 1.2f, 1);
        public static Vector3 VECTOR_ANIMATION_LAMP_CLICK = new Vector3(1, 1.3f, 1);
        public const float TIME_CIRCLE = 2f;
        public const float PERCENT_TO_FADE = 0f;

        #endregion

        /// <summary>
        /// Поиск первого вхождения префаба отличного от иконки
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static Prefab GetFormatJson(JsonUnity json)
        {
            return (Prefab)Enum.Parse(typeof(Prefab), json.MYTJson
                .Find(x => x.MYTName == "MYTPrefab").MYTValue
                .Find(x => x.MYTName != Prefab.MYTIcon60_60.ToString()).MYTName);
        }

        public static IdNameValue JsonColor(Parent parent)
        {
            IdNameValue json = new IdNameValue("MYTColor");
            if (parent.My.GetComponent<MytPrefabColorTypes>() != null)
            {
                json.MYTValue = new List<IdNameValue>()
                {
                    NameValue("r", parent.My.GetComponent<MytPrefabColorTypes>().LastSelectedColor.r.ToString()),
                    NameValue("g", parent.My.GetComponent<MytPrefabColorTypes>().LastSelectedColor.g.ToString()),
                    NameValue("b", parent.My.GetComponent<MytPrefabColorTypes>().LastSelectedColor.b.ToString()),
                    NameValue("a", parent.My.GetComponent<MytPrefabColorTypes>().LastSelectedColor.a.ToString())
                };
            }

            return json;
        }

        private static IdNameValue NameValue(string name, String value)
        {
            IdNameValue _name = new IdNameValue();
            IdNameValue _value = new IdNameValue();
            _name.MYTName = name;
            _value.MYTName = value;
            _name.MYTValue.Add(_value);

            return _name;
        }

        public static Color ColorFromJson(IdNameValue json)
        {
            Color color = new Color(float.Parse(json.MYTValue.Find(x => x.MYTName == "r").MYTValue[0].MYTName),
                float.Parse(json.MYTValue.Find(x => x.MYTName == "g").MYTValue[0].MYTName),
                float.Parse(json.MYTValue.Find(x => x.MYTName == "b").MYTValue[0].MYTName),
                float.Parse(json.MYTValue.Find(x => x.MYTName == "a").MYTValue[0].MYTName));

            return color;
        }

        public static EventTrigger.Entry GetEntry(Action action, EventTriggerType type)
        {
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = type;
            entry.callback.AddListener((data) => { action.Invoke(); });
            return entry;
        }

        public static int[] FindChanges(int[] startArray, int[] newArray)
        {
            return newArray.Where(currentNumber => !startArray.Contains(currentNumber)).ToArray();
        }

        public static T Set<T>(T firstValue, T secondValue)
        {
            return firstValue == null ? secondValue : firstValue;
        }

        public static Color CheckColor(Color obj_1, Color obj_2)
        {
            return obj_1 == default ? obj_2 : obj_1;
        }

        public static T LoadJson<T>(string nameJson)
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            return JsonUtility.FromJson<T>((RegisterSingleton._instance.GetSingleton(typeof(LanguageBundleSaver)) as LanguageBundleSaver)?.LanguageAb
                .LoadAsset<UnityEngine.Object>(nameJson).ToString());
            // return JsonUtility.FromJson<T>(Resources
            //     .Load<TextAsset>(PATH_TO_LANGUAGE_JSON + SceneManager.GetActiveScene().name + "/" +
            //                      ChangeLanguageLoginScene.currentLanguage + "/" + nameJson).text);
#else
            // return JsonUtility.FromJson<T>((RegisterSingleton._instance.BundleSaver.Ab
            //     .LoadAsset<UnityEngine.Object>(nameJson).ToString()));

            JsonSerializer jsonSerializer = JsonSerializer.CreateDefault();
            return jsonSerializer.Deserialize<T>(new JsonTextReader(File.OpenText(Application.dataPath + "\\" +
                PATH_TO_LANGUAGE_JSON + "/" +
                (RegisterSingleton._instance.GetSingleton(typeof(LanguageBundleSaver)) as LanguageBundleSaver)
                ?.SceneEnum + "/" +
                SceneManager.GetActiveScene().name + "/" +
                ChangeLanguageLoginScene.currentLanguage + "/" + "\\" + nameJson + ".json")));
#endif
        }

        public static T LoadJsonFromLanguage<T>(string nameJson)
        {
            //todo вынести отдельно
            JsonSerializer jsonSerializer = JsonSerializer.CreateDefault();
            
            // return jsonSerializer.Deserialize<T>(new JsonTextReader(File.OpenText(Application.dataPath + "\\" +
            //     PATH_TO_LANGUAGE_JSON + "/" +
            //     (RegisterSingleton._instance.GetSingleton(typeof(LanguageBundleSaver)) as LanguageBundleSaver)
            //     ?.SceneEnum + "/" +
            //     SceneManager.GetActiveScene().name + "/" +
            //     ChangeLanguageLoginScene.currentLanguage + "/" + "\\" + nameJson + ".json")));
            
            return JsonUtility.FromJson<T>(
                ((RegisterSingleton._instance.GetSingleton(typeof(LanguageBundleSaver)) as LanguageBundleSaver)
                    ?.LanguageAb
                    .LoadAsset<UnityEngine.Object>(nameJson).ToString()));

        }

        public static T LoadJson<T>(string currentLanguage, string nameJson)
        {
            //todo вынести отдельно
            JsonSerializer jsonSerializer = JsonSerializer.CreateDefault();

#if UNITY_WEBGL && !UNITY_EDITOR
 // return jsonSerializer.Deserialize<T>(new JsonTextReader(new StringReader(Resources
 //              .Load<TextAsset>(ChangeLanguageLoginScene.currentLanguage + "\\" + nameJson).text) ));
            return
                JsonUtility.FromJson<T>(Resources
                    .Load<TextAsset>(PATH_TO_LANGUAGE_JSON + ChangeLanguageLoginScene.currentLanguage + "/"  + nameJson).text);
#else
            return jsonSerializer.Deserialize<T>(new JsonTextReader(File.OpenText(Application.dataPath + "\\" +
                PATH_TO_LANGUAGE_JSON +
                currentLanguage + "/" + "\\" + nameJson + ".json")));
            // return 
            //     JsonUtility.FromJson<T>(File.ReadAllText(Application.dataPath + "\\"+ChangeLanguageLoginScene.currentLanguage+"\\" + nameJson + ".json"));
            // JsonUtility.FromJson<Documents>(Resources.Load<TextAsset>(ChangeLanguageLoginScene.currentLanguage+"\\" + nameJson ).text);

#endif
        }

        public static T DeserializeJson<T>(string json)
        {
            return JsonUtility.FromJson<T>(json);
        }
    }
}