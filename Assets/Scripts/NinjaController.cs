using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaController : MonoBehaviour
{
    Rigidbody2D rb; 
    SpriteRenderer sr;
    
    Animator animator;
    public GameObject bullet;
    const int ANIMATION_QUIETO = 0;
    const int ANIMATION_CORRER = 2;
    //const int ANIMATION_ATACAR = 3;
    const int ANIMATION_Saltar = 4;
    const int ANIMATION_SLIDE = 5;
    const int ANIMATION_DEAD = 6;
    const int ANIMATION_climb = 10;
    const int ANIMATION_glide = 11;
    //const int ANIMATION_jumpattack = 12;
    const int ANIMATION_jumpthrow = 13;
    const int ANIMATION_throw = 14;


    bool puedeSaltar = true;
    private int saltosHechos;
    public int limiteSaltos = 2;
    public float JumpForce = 20;
    public float velocity = 10;
    private int Saltos;

    private Vector3 lastCheckpointPosition;
    private GameManagerController gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManagerController>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Puede saltar"+puedeSaltar.ToString());
         puedeSaltar = true;

        //CORRER
        if (Input.GetKeyUp(KeyCode.G) && sr.flipX == true)
        {
            
                
                var bulletPosition = transform.position + new Vector3(-3,0,0);
                var gb = Instantiate(bullet, bulletPosition, Quaternion.identity) as GameObject;
                var controller = gb.GetComponent<NinjaBullet>();
                controller.SetLeftDirection();
                ChangeAnimation(ANIMATION_throw);
 
        }
        
        else if (Input.GetKeyUp(KeyCode.G) && sr.flipX == false)
        {
                
                var bulletPosition = transform.position + new Vector3(2,0,0);
                var gb = Instantiate(bullet, bulletPosition, Quaternion.identity) as GameObject;
                var controller = gb.GetComponent<NinjaBullet>();
                controller.SetRightDirection();  
                ChangeAnimation(ANIMATION_throw);
           
        }

        //SLIDE
        else if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftArrow)){
            rb.velocity = new Vector2(-7, rb.velocity.y);
            sr.flipX = true;
            ChangeAnimation(ANIMATION_SLIDE);
        }

        else if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.RightArrow)){
            rb.velocity = new Vector2(7, rb.velocity.y);
            sr.flipX = false;
            ChangeAnimation(ANIMATION_SLIDE);
        }
        else if (Input.GetKey(KeyCode.LeftArrow)){
            rb.velocity = new Vector2(-velocity, rb.velocity.y);
            sr.flipX = true;
            ChangeAnimation(ANIMATION_CORRER);
        }

        else if (Input.GetKey(KeyCode.RightArrow)){
            rb.velocity = new Vector2(velocity, rb.velocity.y);
            sr.flipX = false;
            ChangeAnimation(ANIMATION_CORRER);
        }

        // else if (Input.GetKey(KeyCode.T)){
        //     rb.velocity = new Vector2(velocity, rb.velocity.y);
        //     sr.flipX = false;
        //     ChangeAnimation(ANIMATION_climb);
        // }
        // else if (Input.GetKey(KeyCode.R)){
        //     rb.velocity = new Vector2(velocity, rb.velocity.y);
        //     sr.flipX = false;
        //     ChangeAnimation(ANIMATION_glide);
        // }
        
        else if(Input.GetKeyUp(KeyCode.S)){
            
                var bulletPosition = transform.position + new Vector3(2,0,0);
                var gb = Instantiate(bullet, bulletPosition, Quaternion.identity) as GameObject;
                var controller = gb.GetComponent<NinjaBullet>();
                controller.SetRightDirection(); 
                rb.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
                
                ChangeAnimation(ANIMATION_jumpthrow);
            
        }

        //SALTAR
        else if(Input.GetKeyUp(KeyCode.Space)){
            if(saltosHechos<limiteSaltos){

                rb.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
                
                saltosHechos++;
                ChangeAnimation(ANIMATION_Saltar);
                
            }
            
        }

        

        //DEAD
    //    else if (gameManager.livesText.text == "GAME OVER")
    //             {
    //                 rb.velocity = new Vector2(0, rb.velocity.y);
    //                 ChangeAnimation(ANIMATION_DEAD);
                    
    //             }

        
        

        //QUIETO
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            ChangeAnimation(ANIMATION_QUIETO);
        }
    }

    
    void OnCollisionEnter2D(Collision2D other) {
        Debug.Log("Puede saltar");
        puedeSaltar = true;
        // if (other.gameObject.tag == "Enemy")
        //     {
        //         gameManager.PerderVida();
        //     } 
           
            if (other.gameObject.name == "DarkHole")
            {
                if (lastCheckpointPosition != null)
                {
                    transform.position = lastCheckpointPosition;
                }
            }

            if(other.collider.tag=="Tilemap"){
            saltosHechos = 0;  
            }

            
    }
    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("trigger");
        lastCheckpointPosition = transform.position;

        if (other.gameObject.tag == "Enemy")
            {
                Destroy(other.gameObject);
                gameManager.GanarPuntos(10);
            }
    }
   
    private void ChangeAnimation(int animation){
        animator.SetInteger("Estado", animation);

    }

}
