using UnityEngine;

public class TerrainObject : MonoBehaviour
{
    public Animator Anim;
    public GameObject Player;

    public virtual void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Anim = GetComponentInChildren<Animator>();
    }

    public virtual void Update()
    {


    }

    public virtual void OnCollisionEnter2D(Collision2D coll)
    {
    }

    public virtual void OnCollisionExit2D(Collision2D coll)
    {

    }

}

