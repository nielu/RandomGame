using UnityEngine;
using System.Collections;

public class slime : enemy
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();



        if (goRight && !GetGround(0.5f, 0.75f, 0f))
        {
            goRight = false;
            return;
        }
        if (!goRight && !GetGround(0.5f, -0.75f, 0f))
        {
            goRight = true;
            return;
        }

        if (Vector3.Distance(body.position, player.transform.position) < 15f)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }

        if (goRight && player.transform.position.x - transform.position.x < 0)
            flip();
        else if (!goRight && transform.position.x - player.transform.position.x < 0)
            flip();

    }

    protected override void OnCollisionEnter2D(Collision2D coll)
    {

        base.OnCollisionEnter2D(coll);
    }

    

}
