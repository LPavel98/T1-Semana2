using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MegamanController : MonoBehaviour
{

    private float timeLeft = 0;
    public AudioClip jumpClip;
    public AudioClip bulletClip;
    public AudioClip powerupClip;

    public AudioClip coinClip;
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator animator;

    const int ANIMATION_QUIETO = 0;
    const int ANIMATION_CORRER = 2;
    const int ANIMATION_Saltar = 4;
    const int ANIMATION_Shoot = 9;
    const int ANIMATION_Cargar = 15;

    bool puedeSaltar = true;
    private int saltosHechos;
    public int limiteSaltos = 2;
    public float JumpForce = 20;
    public float velocity = 10;
    private int Saltos;

    public GameObject bullet;


    private GameManagerController gameManager;
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        gameManager = FindObjectOfType<GameManagerController>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Debug.Log("Puede saltar" + puedeSaltar.ToString());
        puedeSaltar = true;

        rb.velocity = new Vector2(0, rb.velocity.y);
        ChangeAnimation(ANIMATION_QUIETO);

        
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.velocity = new Vector2(-velocity, rb.velocity.y);
            sr.flipX = true;
            ChangeAnimation(ANIMATION_CORRER);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.velocity = new Vector2(velocity, rb.velocity.y);
            sr.flipX = false;
            ChangeAnimation(ANIMATION_CORRER);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (saltosHechos < limiteSaltos)
            {

                rb.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);

                saltosHechos++;
                ChangeAnimation(ANIMATION_Saltar);
                audioSource.PlayOneShot(jumpClip);

            }

        }
        if (Input.GetKey(KeyCode.X))
        {
            ChangeAnimation(ANIMATION_Cargar);
            timeLeft += Time.deltaTime;
            Debug.Log("Timer: " + timeLeft);
        }

        if (timeLeft < 1)
        {
            if (Input.GetKeyUp(KeyCode.X) && sr.flipX == false)
            {
                ChangeAnimation(ANIMATION_Shoot);
                var bulletPosition = transform.position + new Vector3(1, 0, 0);
                var gb = Instantiate(bullet, bulletPosition, Quaternion.identity) as GameObject;
                var controller = gb.GetComponent<BulletMegamanController>();
                controller.SetRightDirection();
            }
            if (Input.GetKeyUp(KeyCode.X) && sr.flipX == true)
            {
                ChangeAnimation(ANIMATION_Shoot);
                var bulletPosition = transform.position + new Vector3(-1, 0, 0);
                var gb = Instantiate(bullet, bulletPosition, Quaternion.identity) as GameObject;
                var controller = gb.GetComponent<BulletMegamanController>();
                controller.SetLeftDirection();
                
            }
        }
            else if (timeLeft > 3 && timeLeft < 5)
            {
                if (Input.GetKeyUp(KeyCode.X) && sr.flipX == false)
                {
                    ChangeAnimation(ANIMATION_Shoot);
                    var bulletPosition = transform.position + new Vector3(1, 0, 0);
                    var gb = Instantiate(bullet, bulletPosition, Quaternion.identity) as GameObject;
                    
                    var controller = gb.GetComponent<BulletMegamanController>();
                    controller.SetRightDirection();
                    gb.transform.localScale = new Vector3(10,10,10);
                    controller.SetDanio(2);
                }
                if (Input.GetKeyUp(KeyCode.X) && sr.flipX == true)
                {
                    ChangeAnimation(ANIMATION_Shoot);
                    var bulletPosition = transform.position + new Vector3(-1, 0, 0);
                    var gb = Instantiate(bullet, bulletPosition, Quaternion.identity) as GameObject;
                    
                    var controller = gb.GetComponent<BulletMegamanController>();
                    controller.SetLeftDirection();
                    gb.transform.localScale = new Vector3(10,10,10);
                    controller.SetDanio(2);
                }
            }
            else if (timeLeft > 5)
            {
                if (Input.GetKeyUp(KeyCode.X) && sr.flipX == false)
                {
                    ChangeAnimation(ANIMATION_Shoot);
                    var bulletPosition = transform.position + new Vector3(2, 0, 0);
                    var gb = Instantiate(bullet, bulletPosition, Quaternion.identity) as GameObject;
                    var controller = gb.GetComponent<BulletMegamanController>();
                    gb.transform.localScale = new Vector3(15,15,15);
                    controller.SetRightDirection();
                    controller.SetDanio(3);
                }
                if (Input.GetKeyUp(KeyCode.X) && sr.flipX == true)
                {
                    ChangeAnimation(ANIMATION_Shoot);
                    var bulletPosition = transform.position + new Vector3(-2, 0, 0);
                    var gb = Instantiate(bullet, bulletPosition, Quaternion.identity) as GameObject;
                    var controller = gb.GetComponent<BulletMegamanController>();
                    gb.transform.localScale = new Vector3(15,15,15);
                    controller.SetLeftDirection();
                    controller.SetDanio(3);
                }
            }


        if (Input.GetKeyUp(KeyCode.X))
        {
            timeLeft = 0;
        }




    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Puede saltar");
        puedeSaltar = true;

        if (other.collider.tag == "Tilemap")
        {
            saltosHechos = 0;
        }
    }
    private void ChangeAnimation(int animation)
    {
        animator.SetInteger("Estado", animation);

    }
}
