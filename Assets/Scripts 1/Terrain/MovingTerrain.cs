using UnityEngine;
using System.Collections;

public class MovingTerrain : Terrain
{

 public Vector3 From, To;
 public Vector3 Speed;

 private bool returning = false;
 private Vector3 localPosition;
 private GameObject player;

 void Start()
 {
  player = GameObject.FindGameObjectWithTag("Player");
 }

 void Update()
 {
  localPosition = transform.localPosition;
  if (returning)
  {
   transform.Translate(-Speed * Time.deltaTime);
   if (Vector3.Distance(localPosition, From) < 0.05f)
    returning = false;
  }
  else
  {
   transform.Translate(Speed * Time.deltaTime);
   if (Vector3.Distance(localPosition, To) < 0.05f)
    returning = true;
  }
 }

 void OnCollisionEnter2D(Collision2D coll)
 {
  if (coll.gameObject.tag == "Player")
  {
   MakeChild();
  }
 }

 void OnCollisionExit2D(Collision2D coll)
 {
  if (coll.gameObject.tag == "Player")
  {
   ReleaseChild();
  }
 }

 private void ReleaseChild()
 {
  player.transform.parent = null;
 }

 private void MakeChild()
 {
  player.transform.parent = transform;
 }

}
