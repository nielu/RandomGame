using UnityEngine;
using System.Collections;

public class spring : TerrainObject
{
    public float Force;

    private bool isUp = false;
    private float timeout = 0f;
    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Start();
        if (isUp)
        {
            timeout -= Time.deltaTime;
            if (timeout < 0)
            {
                timeout = 0f;
                isUp = false;
                Anim.SetBool("isUp", isUp);

            }
        }
    }

    public override void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag(Player.tag) && isUp == false)
        {
            isUp = true;
            Anim.SetBool("isUp", isUp);

            timeout = 0.75f;

            var body = Player.GetComponent<Rigidbody2D>();
            body.velocity = Vector2.zero;
            body.AddForce(Vector2.up * Force, ForceMode2D.Impulse);
        }

        base.OnCollisionEnter2D(coll);
    }
}
