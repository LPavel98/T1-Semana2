using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerController : MonoBehaviour
{
    // public AudioClip WorldClip;
    private VaqueraController vaqueraController;
    public Text scoreText;
    public Text livesText;
    private int score;
    private int lives;

    // AudioSource audioSource;
    
    // Start is called before the first frame update
    void Start()
    {
        // audioSource = GetComponent<AudioSource>();
        score = 0;
        lives = 3;
        PrintScoreInScreen();
        PrintLivesInScreen();
        // audioSource.PlayOneShot(WorldClip);
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
    public void GanarVidas(int vidas){
        lives += vidas;
        PrintLivesInScreen();
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
        scoreText.text = "Monedas: " + score;
    }

    
    private void PrintLivesInScreen(){
        livesText.text = "Vida: " + lives;
    }
}
