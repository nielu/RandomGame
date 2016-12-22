using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticLevel : MonoBehaviour
{
    public string LevelName;

    private GameObject Player;

    private bool ShowScore = false;
    // Use this for initialization
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {

    }

    void GameFinish()
    {
        ShowScore = true;
    }

    void OnGUI()
    {
        if (ShowScore)
        {
            GUI.Box(new Rect(Screen.width/2 - 100,Screen.height - 50, 200, 100), string.Format("Game over.\nScore: {0}", Player.GetComponent<PlayerScript>().Score));
        }
    }

    public void OnTriggerEnter2D(Collider2D coll)
    {
        if (Player.CompareTag(coll.tag))
        {
            Debug.Log("Finished!");

            Invoke("GameFinish", 0.5f);
        }
    }
}
