using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float JumpForce = 5;

    public float velocity = 10;
    Rigidbody2D rb; 
    SpriteRenderer sr;
    Animator animator;
    const int ANIMATION_QUIETO = 0;
    const int ANIMATION_CORRER = 2;

    const int ANIMATION_CAMINAR = 1;
    const int ANIMATION_ATACAR = 3;
    const int ANIMATION_Saltar = 4;
    bool puedeSaltar = true;

    private Vector3 lastCheckpointPosition;
    // Start is called before the first frame update
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
        Debug.Log("Puede saltar"+puedeSaltar.ToString());
        if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.X)){
            rb.velocity = new Vector2(-20, rb.velocity.y);
            sr.flipX = true;
            ChangeAnimation(ANIMATION_CORRER);
        }

        else if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.X)){
            rb.velocity = new Vector2(20, rb.velocity.y);
            sr.flipX = false;
            ChangeAnimation(ANIMATION_CORRER);
        }

        else if (Input.GetKey(KeyCode.RightArrow)){
            rb.velocity = new Vector2(velocity, rb.velocity.y);  
            sr.flipX = false;
            ChangeAnimation(ANIMATION_CAMINAR);
        }
        else if (Input.GetKey(KeyCode.LeftArrow)){
            rb.velocity = new Vector2(-velocity, rb.velocity.y);
            sr.flipX = true;
            ChangeAnimation(ANIMATION_CAMINAR);
        }

        else if (Input.GetKey(KeyCode.Z)){
            // rb.velocity = new Vector2(-velocity, rb.velocity.y);
            // sr.flipX = true;
            ChangeAnimation(ANIMATION_ATACAR);
        }

        

        else if (Input.GetKeyUp(KeyCode.Space) && puedeSaltar)
        {
            rb.AddForce(new Vector2(0,JumpForce), ForceMode2D.Impulse);
            puedeSaltar = false;
            ChangeAnimation(ANIMATION_Saltar);
            
        }

        
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            ChangeAnimation(ANIMATION_QUIETO);
        }
    }

    
    void OnCollisionEnter2D(Collision2D other) {
        Debug.Log("Puede saltar");
        puedeSaltar = true;
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
      
            
    }
    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("trigger");
        lastCheckpointPosition = transform.position;
    }
   

    private void ChangeAnimation(int animation){
        animator.SetInteger("Estado", animation);

    }
}
