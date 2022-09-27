using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GokuController : MonoBehaviour
{
    

    public GameObject bullet;

    Rigidbody2D rb; 
    SpriteRenderer sr;
    public float velocity = 10;
    private float defaultGravity;
    
    private Vector2 direction;
    private bool tieneNube = false;
    Animator animator;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>(); 
        animator = GetComponent<Animator>();
        defaultGravity = rb.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        direction = new Vector2(x, y);
        Run();
        rb.velocity = new Vector2(rb.velocity.x, 0);

        if (Input.GetKeyUp(KeyCode.G) )
        {
            
               animator.SetInteger("Estado", 4);
                var bulletPosition = transform.position + new Vector3(2,0,0);
                var gb = Instantiate(bullet, bulletPosition, Quaternion.identity) as GameObject;
                var controller = gb.GetComponent<GokuBulletController>();
                controller.SetRightDirection();

           
            
        }
        else if (Input.GetKey(KeyCode.UpArrow) && tieneNube )
        {
            rb.velocity = new Vector2(rb.velocity.x, velocity);
        }
        else if (Input.GetKey(KeyCode.DownArrow) && tieneNube)
        {
            rb.velocity = new Vector2(rb.velocity.x, -velocity);
        }
        //bullet
        
    }

    private void Run(){
        
        rb.velocity = new Vector2 (direction.x * velocity, rb.velocity.y);
        sr.flipX = direction.x < 0;
    }
    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.name == "nube"){
            Destroy(other.gameObject);
            rb.gravityScale = 0;
            tieneNube = true;
            animator.SetInteger("Estado", 1);
        } 
        if (other.gameObject.tag == "otraScena"){
            
            SceneManager.LoadScene(GameManagerController.tercerEscena);
        } 
    }
    void OnCollisionEnter2D (Collision2D other){
        if (other.gameObject.tag == "Tilemap"){
            rb.gravityScale = defaultGravity;
            tieneNube = false;
            animator.SetInteger("Estado", 0);
        } 
    }


}
