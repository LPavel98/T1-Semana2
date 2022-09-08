using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BalaEnemyController : MonoBehaviour
{
    // Start is called before the first frame update
    public float velocity = 20;
    Rigidbody2D rb;
    float realVelocity;
    
    private GameManagerController gameManager;

   
    public void SetRightDirection(){
        realVelocity = -velocity;
    }
     public void SetLeftDirection(){
        realVelocity = -velocity;

    }
    void Start()
    {
            gameManager = FindObjectOfType<GameManagerController>();
            rb = GetComponent<Rigidbody2D>();   
            Destroy(this.gameObject, 5);

    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(0, realVelocity);
    }

    void OnCollisionEnter2D(Collision2D other) {
            Destroy(this.gameObject); 
            if (other.gameObject.name == "Caballero")
            {
                gameManager.PerderVida();
                //Destroy(other.gameObject);
                //gameManager.GanarPuntos(10);
            }
              
    }
   
}
