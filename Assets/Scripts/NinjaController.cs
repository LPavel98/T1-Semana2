using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NinjaController : MonoBehaviour
{
    public AudioClip jumpClip;
    public AudioClip bulletClip;
    public AudioClip powerupClip;
    
    public AudioClip coinClip;



    public bool ClimbingAllowed{get; set;}
    Rigidbody2D rb; 
    SpriteRenderer sr;
    bool colisionoConCheckpoint2;
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
    private int balas = 0;

    bool puedeSaltar = true;
    private int saltosHechos;
    public int limiteSaltos = 2;
    public float JumpForce = 20;
    public float velocity = 10;
    private int Saltos;

    private Vector3 lastCheckpointPosition;
    private GameManagerController gameManager;
    AudioSource audioSource;

    //escalera
    private float dirX, dirY;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        gameManager = FindObjectOfType<GameManagerController>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //colisionoConCheckpoint2=false;
        //subir escalera
        Debug.Log("Puede saltar"+puedeSaltar.ToString());
         puedeSaltar = true;

         dirX = Input.GetAxisRaw("Horizontal") * velocity;
         ChangeAnimation(ANIMATION_climb);
         if (ClimbingAllowed)
         {
            dirY = Input.GetAxisRaw("Vertical") * velocity;
            ChangeAnimation(ANIMATION_climb);
         } 

        //CORRER
        if (Input.GetKeyUp(KeyCode.G) && sr.flipX == true && balas < 5)
        {
            
                
                var bulletPosition = transform.position + new Vector3(-3,0,0);
                var gb = Instantiate(bullet, bulletPosition, Quaternion.identity) as GameObject;
                var controller = gb.GetComponent<NinjaBullet>();
                controller.SetLeftDirection();
                ChangeAnimation(ANIMATION_throw);
                audioSource.PlayOneShot(bulletClip);
                 //gameManager.PerderBalas();
                 balas++;
 
        }
        
        if (Input.GetKeyUp(KeyCode.G) && sr.flipX == false && balas < 5)
        {
                
                var bulletPosition = transform.position + new Vector3(2,0,0);
                var gb = Instantiate(bullet, bulletPosition, Quaternion.identity) as GameObject;
                var controller = gb.GetComponent<NinjaBullet>();
                controller.SetRightDirection();  
                ChangeAnimation(ANIMATION_throw);
                audioSource.PlayOneShot(bulletClip);
                // gameManager.PerderBalas();
                 balas++;
           
        }

        if (Input.GetKey(KeyCode.UpArrow) && ClimbingAllowed || Input.GetKey(KeyCode.DownArrow)&& ClimbingAllowed)
        {
               ChangeAnimation(ANIMATION_climb);
        }

        //SLIDE
        else if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.RightArrow)){
            rb.velocity = new Vector2(8, rb.velocity.y);
            sr.flipX = false;
            ChangeAnimation(ANIMATION_SLIDE);
        }
        else if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftArrow)){
            rb.velocity = new Vector2(-8, rb.velocity.y);
            sr.flipX = true;
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

        else if(Input.GetKey(KeyCode.Z)){
            
                
                rb.velocity = new Vector2(0, rb.velocity.y);
                
                ChangeAnimation(ANIMATION_glide);
            
        }

        //SALTAR
        else if(Input.GetKeyUp(KeyCode.Space)){
            if(saltosHechos<limiteSaltos){

                rb.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
                
                saltosHechos++;
                ChangeAnimation(ANIMATION_Saltar);
                audioSource.PlayOneShot(jumpClip);
                
            }
            
        }

        else if (gameManager.livesText.text == "GAME OVER")
                {
                    rb.velocity = new Vector2(0, rb.velocity.y);
                    ChangeAnimation(ANIMATION_DEAD);
                    
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
            if (other.gameObject.tag == "Enemy")
            {
                gameManager.PerderVida();
            }

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

            
            
            if (other.gameObject.tag == "Hongo")
            {
                Destroy(other.gameObject);
                
                gameManager.GanarVidas(1);
                transform.localScale = new Vector3(0.5F, 0.5F, 1);
                audioSource.PlayOneShot(powerupClip);
            }

           




            // if (other.gameObject.tag == "Enemy")
            // {
            //     gameManager.PerderVida();
            //     transform.localScale = new Vector3(0.3F, 0.3F, 1);
            //    //Destroy(other.gameObject);
            //     //gameManager.GanarPuntos(10);
            // }

            
    }
    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("trigger");
        if (other.gameObject.tag == "Checkpoint" && colisionoConCheckpoint2 == false)
        {
            lastCheckpointPosition = transform.position;
        gameManager.SaveGame();
        }
        if (other.gameObject.tag == "Checkpoint2" )
        {
            lastCheckpointPosition = transform.position;
             gameManager.SaveGame();
            colisionoConCheckpoint2 = true;
        }

         if (other.gameObject.name == "nube"){
            Destroy(other.gameObject);
            rb.gravityScale = 0;
            animator.SetInteger("Estado", 1);
            SceneManager.LoadScene(GameManagerController.segundaEscena);
        } 
        if (other.gameObject.tag == "Moneda")
            {
                Destroy(other.gameObject);
                gameManager.GanarPuntos(10);
                // gameManager.SaveGame();
                audioSource.PlayOneShot(coinClip);
            }
            if (other.gameObject.tag == "Moneda2")
            {
                Destroy(other.gameObject);
                gameManager.GanarPuntos2(20);
                // gameManager.SaveGame();
                audioSource.PlayOneShot(coinClip);
            }
            if (other.gameObject.tag == "Moneda3")
            {
                Destroy(other.gameObject);
                gameManager.GanarPuntos3(30);
                // gameManager.SaveGame();
                audioSource.PlayOneShot(coinClip);
            }

        
    }
   
    private void ChangeAnimation(int animation){
        animator.SetInteger("Estado", animation);

    }

    private void FixedUpdate() {
        if (ClimbingAllowed)
        {
            rb.isKinematic = true;
            rb.velocity = new Vector2(dirX, dirY);
            
        }
        else
        {
            rb.isKinematic = false;
            rb.velocity = new Vector2(dirX, rb.velocity.y);
            
            //rb.velocity = new Vector2(velocity, rb.velocity.y);
        }
    }

}
