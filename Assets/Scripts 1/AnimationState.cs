using UnityEngine;
using System.Collections;

public class AnimationState : MonoBehaviour
{

 private Animator anim;
 private Rigidbody2D body;

 private bool facingRight = true;
 private string paramName = "state";
 private float delta = 0.1f;

 enum state
 {
  stand = 0,
  walk,
  jump,
  fall,
  climb

 };
 // Use this for initialization
 void Start()
 {
  anim = GetComponent<Animator>();
  body = GetComponent<Rigidbody2D>();
 }

 // Update is called once per frame
 void Update()
 {
  var vX = body.velocity.x;
  var vY = body.velocity.y;

  var newState = state.stand;

  var isUpright = Mathf.Abs(body.rotation) < delta;

  if ((vX < -delta && facingRight == true) || (vX > delta && facingRight == false))
   flip();




  if (vY < -delta && isUpright)
   newState = state.fall;
  else if (vY > delta && isUpright)
   newState = state.jump;
  else if (vX > delta || vX < -delta)
   newState = state.walk;

  anim.SetInteger(paramName, (int)newState);

 }

 void flip()
 {
  facingRight = !facingRight;
  var scale = transform.localScale;
  scale.x = scale.x * -1f;
  transform.localScale = scale;
 }
}
