using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBoardController : MonoBehaviour
{
    public int X, Y;
    public int FontSize;
    public Font GUIFont;
    private PlayerScript player;
    private GUIStyle fontStyle;
    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerScript>();
        fontStyle = new GUIStyle { font = GUIFont, fontSize = FontSize };
    }

    // Update is called once per frame
    void Update()
    {

    }


    void OnGUI()
    {
        GUI.Label(new Rect(X, Y, 200, 50), player.Score.ToString("D6"), fontStyle);
    }
}
