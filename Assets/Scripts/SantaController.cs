using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SantaController : MonoBehaviour
{

    private float timeLeft = 0;
    public float JumpForce = 20;

    public float velocity = 0;
    private float defaultVelocity = 10;
    public int Saltos;
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator animator;
    const int ANIMATION_QUIETO = 0;
    const int ANIMATION_CAMINAR = 1;
    const int ANIMATION_CORRER = 2;
    const int ANIMATION_Saltar = 4;
    const int ANIMATION_SLIDE = 5;
    const int ANIMATION_DEAD = 6;
    bool puedeSaltar = true;

    private int saltosHechos;
    public int limiteSaltos = 2;

    private Vector3 lastCheckpointPosition;

    public GameObject bullet;


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
        //Debug.Log("Puede saltar" + puedeSaltar.ToString());
        puedeSaltar = true;
        
        Walk();
        
    }
    

    private void Walk()
    {
        rb.velocity = new Vector2(velocity, rb.velocity.y);
        if (velocity < 0)
            sr.flipX = true;
        if (velocity > 0)
            sr.flipX = false;
    }


    public void WalkToRight()
    {
        velocity = defaultVelocity;
        // rb.velocity = new Vector2(velocity, rb.velocity.y);
        // sr.flipX = false;
        // ChangeAnimation(ANIMATION_CAMINAR);
    }
    public void WalkToLeft()
    {
        velocity = -defaultVelocity;
        // rb.velocity = new Vector2(-velocity, rb.velocity.y);
        // sr.flipX = true;
        // ChangeAnimation(ANIMATION_CAMINAR);
    }
    // public void StopWalkLeft()
    // {
    //     velocity = 0;
    //     // rb.velocity = new Vector2(0, rb.velocity.y);
    //     // sr.flipX = true;
    //     // ChangeAnimation(ANIMATION_CAMINAR);
    // }

    public void StopWalk()
    {
        velocity = 0;
    }

    // public void StopWalkRight()
    // {
    //     rb.velocity = new Vector2(0, rb.velocity.y);
    //     sr.flipX = false;
    //     ChangeAnimation(ANIMATION_CAMINAR);
    // }
    public void BestJump()
    {
        if (saltosHechos < limiteSaltos)
        {
            rb.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);

            saltosHechos++;
            //ChangeAnimation(ANIMATION_Saltar);
        }

    }
      
    public void Disparar()
    {
        //ChangeAnimation(ANIMATION_Shoot);
        if (sr.flipX == false)
        {
            
        var bulletPosition = transform.position + new Vector3(1, 0, 0);
        var gb = Instantiate(bullet, bulletPosition, Quaternion.identity) as GameObject;
        var controller = gb.GetComponent<BulletMegamanController>();
        controller.SetRightDirection();
        }
        if (sr.flipX == true)
        {
           
        var bulletPosition = transform.position + new Vector3(-1, 0, 0);
        var gb = Instantiate(bullet, bulletPosition, Quaternion.identity) as GameObject;
        var controller = gb.GetComponent<BulletMegamanController>();
        controller.SetLeftDirection();
        }
        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
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

        if (other.collider.tag == "Tilemap")
        {
            saltosHechos = 0;
        }


    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("trigger");
        lastCheckpointPosition = transform.position;
    }

    private void ChangeAnimation(int animation)
    {
        animator.SetInteger("Estado", animation);

    }



}
