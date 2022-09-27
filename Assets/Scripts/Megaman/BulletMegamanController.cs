using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMegamanController : MonoBehaviour
{
private GameManagerController gameManager;
    public float velocity = 20;
    Rigidbody2D rb;
    float realVelocity;
    SpriteRenderer sr;
    public int danio = 1;
    
   
    public void SetRightDirection(){
        realVelocity = velocity;
        
    }
     public void SetLeftDirection(){
        
        realVelocity = -velocity;

        
    }

    public void SetDanio(int d){
        danio = d;
    }

    void Start()
    {
            gameManager = FindObjectOfType<GameManagerController>();
            rb = GetComponent<Rigidbody2D>(); 
            sr = GetComponent<SpriteRenderer>();    
            Destroy(this.gameObject, 5);

    }

    // Update is called once per frame
    void Update()
    {   
        
        rb.velocity = new Vector2(realVelocity, 0);

        if (realVelocity == -25)
        {
            sr.flipX = true;
        }
        
        
    }

    void OnCollisionEnter2D(Collision2D other) {
            Destroy(this.gameObject); 
            if (other.gameObject.tag == "Enemy")
            {
                other.gameObject.GetComponent<EnemyMegamanController>().Damage(danio);
            }
              
    }
   
}
