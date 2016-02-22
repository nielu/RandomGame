using UnityEngine;
using System.Collections;

public class enemy : MonoBehaviour
{

    public float damage;
    public float health;
    public float speed;

    protected Animator animator;
    protected Rigidbody2D body;
    protected GameObject player;
    protected Vector3 startingPosition;
    protected bool goRight = false;
    protected bool isAlive = true;
	// Use this for initialization
	protected virtual void Start ()
    {
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        startingPosition = body.position; 
        animator.SetBool("isAlive", isAlive);
	}
	
	// Update is called once per frame
	protected virtual void Update ()
    {
        if (isAlive && body.position.x < -1f )
        {
            isAlive = false;
            animator.SetBool("isAlive", isAlive);
        }

	}



    protected void flip()
    {
        goRight = !goRight;
        var scale = transform.localScale;
        scale.x = scale.x * -1f;
        transform.localScale = scale;

    }


    protected RaycastHit2D GetGround()
    {
        return Physics2D.Raycast(body.position, -Vector2.up, 3f);
    }
}
