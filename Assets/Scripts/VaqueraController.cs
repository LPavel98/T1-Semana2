using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class VaqueraController : MonoBehaviour
{
    // Start is called before the first frame updatepublic float JumpForce = 20;

    private GameManagerController gameManager;
    public GameObject bullet;
     public float JumpForce = 20;

    public float velocity = 10;
    public int Saltos;
    Rigidbody2D rb; 
    SpriteRenderer sr;
    Animator animator;

    const int ANIMATION_QUIETO = 0;
   
    const int ANIMATION_CORRER = 2;
    const int ANIMATION_ATACAR = 3;
    const int ANIMATION_Saltar = 4;
    const int ANIMATION_SLIDE = 5;
    const int ANIMATION_DEAD = 6;
    const int ANIMATION_HURT = 7;
    const int ANIMATION_SHOOT = 9;
    bool puedeSaltar = true;

    private int saltosHechos;
    public int limiteSaltos = 2;

    private Vector3 lastCheckpointPosition;
   

    void Start()
    {
        Debug.Log("Iniciamos script de player");
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

        rb.velocity = new Vector2(5, rb.velocity.y);
        sr.flipX = false;
        ChangeAnimation(ANIMATION_CORRER);
         //bullet
         if (Input.GetKeyUp(KeyCode.G) && sr.flipX == true)
        {
            
                
                var bulletPosition = transform.position + new Vector3(-2,0,0);
                var gb = Instantiate(bullet, bulletPosition, Quaternion.identity) as GameObject;
                var controller = gb.GetComponent<BulletController>();
                controller.SetLeftDirection();
        
           
            
        }
        
        else if (Input.GetKeyUp(KeyCode.G) && sr.flipX == false)
        {
            
                var bulletPosition = transform.position + new Vector3(1,0,0);
                var gb = Instantiate(bullet, bulletPosition, Quaternion.identity) as GameObject;
                var controller = gb.GetComponent<BulletController>();
                controller.SetRightDirection();  
 
           
        }

        //CORRER
        else if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.X)){
            rb.velocity = new Vector2(-10, rb.velocity.y);
            sr.flipX = true;
            ChangeAnimation(ANIMATION_CORRER);
        }

        else if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.X)){
            rb.velocity = new Vector2(10, rb.velocity.y);
            sr.flipX = false;
            ChangeAnimation(ANIMATION_CORRER);
        }
        //SLIDE
        else if (Input.GetKey(KeyCode.C) && Input.GetKey(KeyCode.LeftArrow)){
            rb.velocity = new Vector2(-20, rb.velocity.y);
            sr.flipX = true;
            ChangeAnimation(ANIMATION_SLIDE);
        }

        else if (Input.GetKey(KeyCode.C) && Input.GetKey(KeyCode.RightArrow)){
            rb.velocity = new Vector2(20, rb.velocity.y);
            sr.flipX = false;
            ChangeAnimation(ANIMATION_SLIDE);
        }


        //SALTAR
        else if(Input.GetKeyUp(KeyCode.Space)){
            if(saltosHechos<limiteSaltos){

                rb.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
                
                saltosHechos++;
                ChangeAnimation(ANIMATION_Saltar);
                
            }
            
        }

        //ATACAR
        else if (Input.GetKey(KeyCode.Z)){
            
            ChangeAnimation(ANIMATION_ATACAR);
        }

        //shoot
        else if (Input.GetKey(KeyCode.G)){
            
            ChangeAnimation(ANIMATION_SHOOT);
        }

        //DEAD
       else if(Input.GetKeyUp(KeyCode.Q))
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            ChangeAnimation(ANIMATION_DEAD);
        }

        else if (gameManager.livesText.text == "GAME OVER")
                {
                    rb.velocity = new Vector2(0, rb.velocity.y);
                    ChangeAnimation(ANIMATION_DEAD);
                    
                }

        //QUIETO
        else
        {
            // rb.velocity = new Vector2(0, rb.velocity.y);
            // ChangeAnimation(ANIMATION_QUIETO);
        }
    }
   
    
    void OnCollisionEnter2D(Collision2D other) {
        //Debug.Log("Puede saltar");
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

            // if (other.gameObject.name == "ZombiHombre")
            // {
            //     //Destroy(other.gameObject);
            //     gameManager.PerderVida();
            // }

            if(other.collider.tag=="Tilemap"){
            saltosHechos = 0;  
        }

            
    }
    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("trigger");
        lastCheckpointPosition = transform.position;
        if (other.gameObject.name == "nube"){
            Destroy(other.gameObject);
            rb.gravityScale = 0;
            //tieneNube = true;
            animator.SetInteger("Estado", 1);
            SceneManager.LoadScene(GameManagerController.primeraEscena);
        } 
    }
   
    private void ChangeAnimation(int animation){
        animator.SetInteger("Estado", animation);

    }

}