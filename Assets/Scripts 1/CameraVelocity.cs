using UnityEngine;
using System.Collections;

public class CameraVelocity : MonoBehaviour
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
 private bool lastGround;
 private Rigidbody2D body;

 void Start()
 {
  player = GameObject.Find("Player");
  actualSpeed = maxWalkSpeed;
  lastGround = false;

  cameraStartingPosition = cam.transform.position;
  cameraDifference = cameraStartingPosition - transform.position;

  body = GetComponent <Rigidbody2D>();

  //body.freezeRotation = false;
 }
 void Update()
 {

  actualSpeed = 0f;

  if (transform.position.y < -1 || Health < 0)
  {
   GameOver();
   return;
  }


  var groundHit = GetGround();

  if (groundHit && lastGround == false)
  {

   if (Input.GetKeyDown(KeyCode.Space) || Input.touchCount > 0)
   {
    body.AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);
    lastGround = true;
   }

  }
  else if (groundHit && lastGround)
   lastGround = false;



  if (Input.GetKey(KeyCode.LeftArrow))
  {
   actualSpeed = -maxWalkSpeed;
  }
  else if (Input.GetKey(KeyCode.RightArrow))
  {
   actualSpeed = maxWalkSpeed;
  }


  transform.rotation = Quaternion.FromToRotation(Vector3.up, GetGroundNormal().normal);


  var move = new Vector2(actualSpeed * Time.deltaTime, body.velocity.y);

  if (GetRightWall())
  {
   if (Input.GetKey(KeyCode.UpArrow))
   {
    move.y = 15.0f * jumpSpeed * Time.deltaTime;
   }
   //move.x = 0f;
  }




  body.velocity = move;
  cam.transform.position = transform.position + cameraDifference;


 }

 public void onTriggerEnter(Collider other)
 {
  Debug.Log("COLLISION! " + other.ToString());
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



 void GameOver()
 {
  Debug.Log("Failed!");
  GetComponentInChildren<TerrainSpawner>().GameOver();
  transform.position = startingPosition;
  cam.transform.position = cameraStartingPosition;
  actualSpeed = maxWalkSpeed;
  Health = 5f;
 }
}
