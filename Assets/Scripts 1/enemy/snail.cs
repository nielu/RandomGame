using UnityEngine;
using System.Collections;

public class snail : enemy
{
    private Vector3 pos1, pos2;
	// Use this for initialization
	protected override void Start ()
    {
        base.Start();
        pos1 = startingPosition; pos1.x -= 1.5f;
        pos2 = startingPosition; pos2.x += 1.5f;
	}
	
	// Update is called once per frame
	protected override void Update () 
    {
        var move = new Vector3();
        if (goRight)
        {
            if (body.position.x > pos2.x)
            {
                move.x = -speed;
                flip();
            }
            else
                move.x = speed;
        }
        else
        {
            if (body.position.x < pos1.x)
            {
                move.x = speed;
                flip();
            }
            else
                move.x = -speed;
        }

        move.x *= Time.deltaTime;
        body.transform.Translate(move);

        base.Update();
	}


    
}
