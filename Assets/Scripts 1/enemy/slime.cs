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

  if (Vector3.Distance(body.position, player.transform.position) < 15f)
  {
   transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
  }

  if (goRight && player.transform.position.x - transform.position.x < 0)
   flip();
  else if (!goRight && transform.position.x - player.transform.position.x < 0)
   flip();

 }

 protected override void OnRaycastHit2D(Collision coll)
 {
  base.OnRaycastHit2D(coll);
 }

}
