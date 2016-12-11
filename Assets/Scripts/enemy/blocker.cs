using UnityEngine;
using System.Collections;

public class blocker : enemy
{
    public float JumpTime;

    private float timer = 0;
    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        JumpTime += Random.Range(-0.2f, 0.2f);
    }

    // Update is called once per frame
    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        timer += Time.deltaTime;
        if (timer > JumpTime)
        {
            body.AddForce(Vector2.up * speed, ForceMode2D.Impulse);
            timer = 0;
        }
    }
}
