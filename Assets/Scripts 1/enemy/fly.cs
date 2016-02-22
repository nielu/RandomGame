using UnityEngine;
using System.Collections;

public class fly : enemy
{

    
	// Use this for initialization
	protected override void Start () 
    {
        base.Start();
        startingPosition.y += 1f;
        body.transform.Translate(startingPosition);
	}
	
	// Update is called once per frame
	protected override void Update () 
    {
        base.Update();
        var ground = GetGround();
        var height = Vector2.Distance(body.position, ground.point);
        if (isAlive && body.velocity.y < 0.1f && height < 1.0f)
            body.AddForce(new Vector2(0f, 15.0f * Time.deltaTime) , ForceMode2D.Impulse);

        //rotate to look at the player
        //body.transform.rotation = Quaternion.Slerp(body.transform.rotation,
        //Quaternion.LookRotation(player.transform.position - body.transform.position), 3.0f * Time.deltaTime);


        //move towards the player
        //body.transform.position += body.transform.forward * speed * Time.deltaTime;



	}
}
