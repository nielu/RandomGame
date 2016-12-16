using UnityEngine;
using System.Collections;

public class spikes: TerrainObject
{
    public float damage;
	// Use this for initialization
	public override void Start () 
    {
        base.Start();
	}
	
	// Update is called once per frame
	public override void Update ()
    {
        base.Start();

	}

    public override void OnCollisionEnter2D(Collision2D coll)
    {
        if (Player.CompareTag(coll.gameObject.tag))
        {
            Player.GetComponent<PlayerScript>().Health -= damage;
        }
        base.OnCollisionEnter2D(coll);
    }
}
