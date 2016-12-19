using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour
{
    // script to move camera
    public float maxWalkSpeed;
    public float jumpSpeed;
    public float Health;
    public Vector3 startingPosition;
    public GameObject player;
    public GameObject cam;
    public LayerMask ground;

    private float actualSpeed;
    private Vector3 cameraStartingPosition;
    private Vector3 cameraDifference;
    private Animator animator;
    private bool isJumping;
    private Rigidbody2D body;

    void Start()
    {
        player = GameObject.Find("Player");
        actualSpeed = maxWalkSpeed;
        isJumping = false;

        cameraStartingPosition = cam.transform.position;
        cameraDifference = cameraStartingPosition - transform.position;

        body = GetComponent<Rigidbody2D>();

        //body.freezeRotation = false;
    }
    void FixedUpdate()
    {

        if (transform.position.y < -1 || Health <= 0)
        {
            GameOver();
            return;
        }

        actualSpeed = 0f;


        var groundHit = GetGround();

        if (groundHit && isJumping == false)
        {

            if (Input.GetKeyDown(KeyCode.Space) || Input.touchCount > 0)
            {
                body.AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);
                isJumping = true;
            }

        }
        else if (groundHit && isJumping)
        {
            isJumping = false;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            actualSpeed = -maxWalkSpeed;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            actualSpeed = maxWalkSpeed;
        }
        else if (Mathf.Abs(Input.acceleration.x) > 0.05f)
        {
            actualSpeed = Input.acceleration.x * maxWalkSpeed * 2f; //full speed at 45* bank?
        }


        transform.rotation = Quaternion.FromToRotation(Vector3.up, GetGroundNormal().normal);

        var move = new Vector2(actualSpeed * Time.deltaTime, body.velocity.y);

        if (Mathf.Abs(body.rotation) > 0.1f && groundHit)
        {

            var v = actualSpeed * Time.deltaTime;

            var vx = v * Mathf.Cos(body.rotation);
            var vy = v * Mathf.Sin(body.rotation);

            move = new Vector2(vx, vy);
        }

        



        body.velocity = move;
        cam.transform.position = transform.position + cameraDifference;


    }


    RaycastHit2D GetGroundNormal()
    {

        return Physics2D.Raycast(transform.position, Vector3.down, 1.35f, ground);
    }


    RaycastHit2D GetGround()
    {
        Debug.DrawRay(transform.position, -transform.up, Color.red, 0.35f);
        return Physics2D.Raycast(transform.position, -transform.up, 0.35f, ground);
    }

    bool GetRightWall()
    {
        return Physics2D.Raycast(transform.position, transform.right, 0.5f, ground) ||
            Physics2D.Raycast(transform.position + new Vector3(0, .3f, 0), transform.right, 0.5f, ground) ||
            Physics2D.Raycast(transform.position + new Vector3(0, -.3f, 0), transform.right, 0.5f, ground);
    }

    bool GetRightSlope()
    {
        return !Physics2D.Raycast(transform.position, transform.right, 0.5f, ground) &&
            (Physics2D.Raycast(transform.position + new Vector3(0, .3f, 0), transform.right, 0.5f, ground) ||
            Physics2D.Raycast(transform.position + new Vector3(0, -.3f, 0), transform.right, 0.5f, ground));
    }

    public void ApplyDamage(float dmg, Collision2D coll)
    {
        Health -= dmg;
        var f = body.position - coll.contacts[0].point;

        body.AddForce(f * 15.0f, ForceMode2D.Impulse);
        
    }

    void GameOver()
    {
        Debug.Log("Failed!");
        GetComponentInChildren<TerrainSpawner>().GameOver();
        transform.position = startingPosition;
        cam.transform.position = cameraStartingPosition;
        actualSpeed = maxWalkSpeed;
        Health = 3f;
    }
}
