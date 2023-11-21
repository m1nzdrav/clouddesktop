using UnityEngine;

public class FontSettigns : MonoBehaviour
{
    void OnGUI()
    {
        GUIStyle style = new GUIStyle();
        style.font = (Font)Resources.Load("Amiri-Regular", typeof(Font)); // The font file must be in the "Resources" folder!
        style.fontSize = 50;
        style.normal.textColor = Color.red;
        GUI.Label(new Rect(10, 10, 100, 20), "Alien Attack!!!", style);
    }
}
