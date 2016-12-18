using UnityEngine;
using System.Collections;

public class HealthBarController : MonoBehaviour
{
    
    public Texture[] HeartsSprite;
    public int x, y;

    private PlayerScript player;
    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnGUI()
    {
        var pHealth = player.Health;
        var localX = x;
        var spriteToDraw = HeartsSprite[0];

        for (int i = 0; i < 3; i++)
        {
            var h = pHealth - i;
            if (h > .5f)
                spriteToDraw = HeartsSprite[2];
            else if (h > 0f)
                spriteToDraw = HeartsSprite[1];
            else
                spriteToDraw = HeartsSprite[0];


            var r = new Rect(localX, y, spriteToDraw.width / 2, spriteToDraw.height / 2);
            GUI.DrawTexture(r, spriteToDraw);
            localX += spriteToDraw.width / 2;
        }
    }


}
