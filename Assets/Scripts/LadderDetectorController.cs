using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderDetectorController : MonoBehaviour
{
    [SerializeField]
    private NinjaController ninjaController;
    
    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.GetComponent<LadderController>())
        {
            ninjaController.ClimbingAllowed = true;
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.GetComponent<LadderController>())
        {
             ninjaController.ClimbingAllowed = false;
        }
    }
}
