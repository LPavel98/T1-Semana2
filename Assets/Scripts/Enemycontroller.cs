using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemycontroller : MonoBehaviour
{
    // Start is called before the first frame update   public float JumpForce = 5;
    public GameObject bullet;
    public float velocity = 10;
    Rigidbody2D rb; 
    SpriteRenderer sr;
    Animator animator;
    const int ANIMATION_QUIETO = 0;
    const int ANIMATION_CORRER = 1;

   
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
       
        
        if (Input.GetKeyUp(KeyCode.P) && sr.flipX == true)
        {
            var bulletPosition = transform.position + new Vector3(-0,-1,0);
            var gb = Instantiate(bullet, bulletPosition, Quaternion.identity) as GameObject;
            var controller = gb.GetComponent<BulletController>();
            controller.SetLeftDirection();
        }
        else if (Input.GetKeyUp(KeyCode.P) && sr.flipX == false)
        {
            var bulletPosition = transform.position + new Vector3(0,-1,0);
            var gb = Instantiate(bullet, bulletPosition, Quaternion.identity) as GameObject;
            var controller = gb.GetComponent<BulletController>();
            controller.SetRightDirection();
        }

        else if (Input.GetKey(KeyCode.RightArrow)){
            rb.velocity = new Vector2(2, rb.velocity.y);  
            sr.flipX = false;
        }
        else if (Input.GetKey(KeyCode.LeftArrow)){
            rb.velocity = new Vector2(-2, rb.velocity.y);
            sr.flipX = true;
        }
        
        else
        {
            
        }
        
        
        
       
            
       
       
      

    }  

    void OnCollisionEnter2D(Collision2D other) {
        
            if (other.gameObject.tag == "Enemy")
            {
                Debug.Log("Estas muerto");
            }   
    }
   

    private void ChangeAnimation(int animation){
        animator.SetInteger("Estado", animation);

    }

}
