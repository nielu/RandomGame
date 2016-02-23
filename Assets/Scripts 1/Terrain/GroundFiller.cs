using UnityEngine;
using System.Collections;

public class GroundFiller : MonoBehaviour 
{
    public GameObject DirtBlock;
    public int HowMuch;


    private Vector2 lastPos;
	// Use this for initialization
	void Start () 
    {
        lastPos = transform.position;
        SpawnGround();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (lastPos.x + 0.7f >= transform.position.x)
        {
            SpawnGround();
            lastPos = transform.position;
        }
	}


    void SpawnGround()
    {
        for (int i = 0; i < HowMuch; i++)
        {
            Instantiate(DirtBlock, lastPos + new Vector2(0, -1.4f), Quaternion.EulerAngles(0,0,0));
        }
    }
}
