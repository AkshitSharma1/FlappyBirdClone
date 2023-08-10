using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ICanFly : MonoBehaviour
{
    //Yes I WILL FLY
    Rigidbody2D myBODY;
    Animator ANIM;
    [SerializeField] float val;
    bool isDead = false;
    [SerializeField]  AudioClip flap;
    AudioSource ad;
    private void Start()
    {

        ad = gameObject.GetComponent<AudioSource>();
        myBODY = GetComponent<Rigidbody2D>();
        ANIM = GetComponent<Animator>();
        isDead = false;
        Debug.Log("Player has been initialized");
    }
    [SerializeField]public bool jumpAllowed = false;
    public void Update()
    {
     
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            if (jumpAllowed==true)
            {
                myBODY.velocity = Vector2.up * val;
                Debug.Log("Making player jump");
                ad.PlayOneShot(flap);
              //  Debug.Log("KEY IS DOWN");
              
                ANIM.SetTrigger("CanFly");
            }
        }

    }
    void Die()

    {
        if(isDead==true) { return; }
        isDead = true;
        Debug.Log("Player has died");
        GameObject.FindObjectOfType<GC>().GameOver();
        jumpAllowed = false;
        Destroy(gameObject,1f);
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(isDead==true) { return; }
        Debug.Log("Called die from oncollisionenter2d");
        Die();
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject.FindObjectOfType<GC>().SpawnPillar();
        if (isDead==true) { return; }
        Debug.Log("called score from ontriggerenter2d");
        Score();
    }
    public void Score( )
    {
        GameObject.FindObjectOfType<GC>().IncreaseScore();
       //Inc score
    }
}
