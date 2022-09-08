using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerController : MonoBehaviour
{
    private VaqueraController vaqueraController;
    public Text scoreText;
    public Text livesText;
    private int score;
    private int lives;
    
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        lives = 3;
        PrintScoreInScreen();
        PrintLivesInScreen();
    }

    // Update is called once per frame
    public int Score(){
        return score;
    }

    public int Lives(){
        return lives;
    }

    public void GanarPuntos(int puntos){
        score += puntos;
        PrintScoreInScreen();
    }
    public void PerderVida(){
        lives -= 1;
        PrintLivesInScreen();
        if (lives == 2)
        {
            
            livesText.text = "GAME OVER";
            
           
        }
        
    }

    private void PrintScoreInScreen(){
        scoreText.text = "Puntaje: " + score;
    }

    
    private void PrintLivesInScreen(){
        livesText.text = "Vida: " + lives;
    }
}
