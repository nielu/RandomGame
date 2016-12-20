using UnityEngine;
using System.Collections;

public class enemy : MonoBehaviour
{

    public float damage;
    public float health;
    public float speed;
    public int EnemyScore;

    protected Animator animator;
    protected Rigidbody2D body;
    protected GameObject player;
    protected Vector3 startingPosition;
    protected SpriteRenderer spriteRender;
    protected bool goRight;
    protected bool isAlive;
    protected bool onEdge;
    // Use this for initialization
    protected virtual void Start()
    {
        goRight = false;
        isAlive = true;
        onEdge = false;
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        spriteRender = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");
        startingPosition = body.position;
        animator.SetBool("isAlive", isAlive);
    }

    // Update is called once per frame
    protected virtual void FixedUpdate()
    {
        if (isAlive && body.position.x < -1f)
        {
            Die();
        }
    }



    protected void flip()
    {
        goRight = !goRight;
        spriteRender.flipX = goRight;

    }

    protected void Die()
    {
        isAlive = false;
        animator.SetBool("isAlive", isAlive);
        GetComponent<Collider2D>().enabled = false;
    }


    protected RaycastHit2D GetGround(float length = 3f, float xOffset = 0f, float yOffset = 0f)
    {
        Debug.DrawRay(body.position + new Vector2(xOffset, yOffset), -Vector2.up, Color.blue, length);
        return Physics2D.Raycast(body.position + new Vector2(xOffset, yOffset), -Vector2.up, length);
    }

    protected virtual void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag(player.tag))
        {

            Vector2 contactPoint = coll.contacts[0].point;
            Vector2 center = body.transform.position;

            Debug.Log(string.Format("Contact {0} Center {1}", contactPoint, center));
            bool top = contactPoint.y > center.y;
            bool right = contactPoint.x > center.x;
            Debug.Log(string.Format("Top: {0} Right {1}", top, right));
            if (top)        //collision on top of enemy
            {
                Die();
                player.GetComponent<PlayerScript>().Score += EnemyScore;
            }
            else
                player.GetComponent<PlayerScript>().ApplyDamage(damage, coll);
        }
    }

}
