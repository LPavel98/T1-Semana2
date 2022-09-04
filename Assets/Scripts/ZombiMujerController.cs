using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiMujerController : MonoBehaviour
{
    // Start is called before the first frame update
    public float JumpForce = 20;

    public float velocity = 10;
    public int Saltos;
    Rigidbody2D rb; 
    SpriteRenderer sr;
    Animator animator;
    const int ANIMATION_QUIETO = 0;
    const int ANIMATION_CAMINAR = 1;
    const int ANIMATION_ATACAR = 3;
    const int ANIMATION_DEAD = 6;
    // bool puedeSaltar = true;
    // private int saltosHechos;
    // public int limiteSaltos = 2;

    private Vector3 lastCheckpointPosition;
   
    void Start()
    {
        Debug.Log("Iniciamos script de player");
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("Puede saltar"+puedeSaltar.ToString());
        //  puedeSaltar = true;

                
        //CAMINAR
        if (Input.GetKey(KeyCode.RightArrow)){
            rb.velocity = new Vector2(velocity, rb.velocity.y);  
            sr.flipX = false;
            ChangeAnimation(ANIMATION_CAMINAR);
        }
        else if (Input.GetKey(KeyCode.LeftArrow)){
            rb.velocity = new Vector2(-velocity, rb.velocity.y);
            sr.flipX = true;
            ChangeAnimation(ANIMATION_CAMINAR);
        }

        //DEAD
       else if(Input.GetKeyUp(KeyCode.Q))
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            ChangeAnimation(ANIMATION_DEAD);
        }

        //ATACAR
        else if (Input.GetKey(KeyCode.Z)){
            
            ChangeAnimation(ANIMATION_ATACAR);
        }

        //QUIETO
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            ChangeAnimation(ANIMATION_QUIETO);
        }
    }

    
    void OnCollisionEnter2D(Collision2D other) {
        // Debug.Log("Puede saltar");
        // puedeSaltar = true;
            if (other.gameObject.tag == "Enemy")
            {
                Debug.Log("Estas muerto");
            } 
            if (other.gameObject.name == "DarkHole")
            {
                if (lastCheckpointPosition != null)
                {
                    transform.position = lastCheckpointPosition;
                }
            }

        //     if(other.collider.tag=="Tilemap"){
        //     saltosHechos = 0;  
        // }

            
    }
    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("trigger");
        lastCheckpointPosition = transform.position;
    }
   
    private void ChangeAnimation(int animation){
        animator.SetInteger("Estado", animation);

    }

}
