using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int Value;

    private GameObject Player;
    private Rigidbody2D Body;
    private bool NotUsed = true;
    // Use this for initialization
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Body = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void DestroyMe()
    {
        DestroyImmediate(this.gameObject.transform.parent.gameObject);
    }

    public void OnTriggerEnter2D(Collider2D coll)
    {
        if(Player.CompareTag(coll.tag) && NotUsed)
        {
            Player.GetComponent<PlayerScript>().Score += Value;
            Body.gravityScale = 1.0f;
            Body.AddForce(Vector2.up * 5.0f, ForceMode2D.Impulse);
            Invoke("DestroyMe", 2.0f);
            NotUsed = false;
        }
    }
}
