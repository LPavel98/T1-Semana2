using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiAttackController : MonoBehaviour
{
    // Start is called before the first frame update

    public float velocity;
  
    Rigidbody2D rb; 
    SpriteRenderer sr;
    Animator animator;
    const int ANIMATION_QUIETO = 0;
    const int ANIMATION_CAMINAR = 1;
    const int ANIMATION_ATACAR = 3;
    const int ANIMATION_DEAD = 6;
    public float vidaEnemigo = 3;

    // bool puedeSaltar = true;
    // private int saltosHechos;
    // public int limiteSaltos = 2;
    private GameManagerController gameManager;
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
            rb.velocity = new Vector2(velocity, rb.velocity.y);
            //sr.flipX = true;
            ChangeAnimation(ANIMATION_CAMINAR);
        // Debug.Log("Puede saltar"+puedeSaltar.ToString());
        //  puedeSaltar = true;

                
        //CAMINAR
        // if (Input.GetKey(KeyCode.RightArrow)){
        //     rb.velocity = new Vector2(velocity, rb.velocity.y);  
        //     sr.flipX = false;
        //     ChangeAnimation(ANIMATION_CAMINAR);
        // }
        // else if (Input.GetKey(KeyCode.LeftArrow)){
        //     rb.velocity = new Vector2(-velocity, rb.velocity.y);
        //     sr.flipX = true;
        //     ChangeAnimation(ANIMATION_CAMINAR);
        // }

        //DEAD
    //    else if(Input.GetKeyUp(KeyCode.Q))
    //     {
    //         rb.velocity = new Vector2(0, rb.velocity.y);
    //         ChangeAnimation(ANIMATION_DEAD);
    //     }

        //ATACAR
        
           // rb.velocity = new Vector2(0, rb.velocity.y);
            //ChangeAnimation(ANIMATION_QUIETO);
        
        
        
        

        // else if (true)
        // {
        //     rb.velocity = new Vector2(-1, rb.velocity.y);
        //     sr.flipX = true;
        //     ChangeAnimation(ANIMATION_CAMINAR); 
        // }
        // else
        // {
           
        // }
            
        
            
            //ChangeAnimation(ANIMATION_ATACAR);
        

        //QUIETO
        //else
        //{
            // rb.velocity = new Vector2(0, rb.velocity.y);
            // ChangeAnimation(ANIMATION_QUIETO);
        //}
        if(vidaEnemigo<=0){
            Destroy(this.gameObject);
        }
    }

    
    void OnCollisionEnter2D(Collision2D other) {
        // Debug.Log("Puede saltar");
        // puedeSaltar = true;
            // if (other.gameObject.tag == "Enemy")
            // {
            //     Debug.Log("Estas muerto");
            // } 
            // if (other.gameObject.name == "Vaquera")
            // {
            //     gameManager.PerderVida();
                
            // }

        //     if(other.collider.tag=="Tilemap"){
        //     saltosHechos = 0;  
        // }

            
    }
    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("trigger");
        lastCheckpointPosition = transform.position;
        if (other.gameObject.tag == "limiteDerecho")
        {
            velocity *= -1;
            sr.flipX = true;
        }
        if (other.gameObject.tag == "limiteIzquierdo")
        {
            velocity *= -1;
            sr.flipX = false;
        }
    }
   
    private void ChangeAnimation(int animation){
        animator.SetInteger("Estado", animation);

    }
    public void Damage(int a){
        vidaEnemigo -= a;
        //Debug.Log("Vida Enemigo: "+vidaEnemigo);
    }

}

