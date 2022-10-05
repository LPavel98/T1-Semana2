using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMegamanController : MonoBehaviour
{
    // Start is called before the first frame update
    public float velocity;
  
    Rigidbody2D rb; 
    SpriteRenderer sr;
    Animator animator;
  
    const int ANIMATION_CAMINAR = 1;
     public float vidaEnemigo = 2;

    private GameManagerController gameManager;
    private NinjaButtonController ninjaButtonController ;
    private Vector3 lastCheckpointPosition;
   
    void Start()
    {
        gameManager = FindObjectOfType<GameManagerController>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        ninjaButtonController = FindObjectOfType<NinjaButtonController>();
    }

    // Update is called once per frame
    void Update()
    {
            rb.velocity = new Vector2(velocity, rb.velocity.y);
            ChangeAnimation(ANIMATION_CAMINAR);
       
        if(vidaEnemigo<=0){
            Destroy(this.gameObject);
            ninjaButtonController.zombiesMuertos ++;
        }
    }

    
    void OnCollisionEnter2D(Collision2D other) {


    }

    private void OnTriggerEnter2D(Collider2D other) {
    
    }
   
    private void ChangeAnimation(int animation){
        animator.SetInteger("Estado", animation);

    }
    public void Damage(int a){
        vidaEnemigo -= a;
        //Debug.Log("Vida Enemigo: "+vidaEnemigo);
    }

}

