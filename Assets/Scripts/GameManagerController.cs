using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameManagerController : MonoBehaviour
{
    // public AudioClip WorldClip;
    private VaqueraController vaqueraController;
    public Text scoreText;
    public Text livesText;
    public Text bulletText;

    private int score;
    private int lives;

    private int bullet;

    // AudioSource audioSource;
    
    // Start is called before the first frame update
    void Start()
    {
        // audioSource = GetComponent<AudioSource>();
        bullet = 5;
        score = 0;
        lives = 3;
        PrintScoreInScreen();
        PrintLivesInScreen();
        PrintBulletsInScreen();
        LoadGame();
        // audioSource.PlayOneShot(WorldClip);
    }

    // Update is called once per frame
    public int Score(){
        return score;
    }

    public int Lives(){
        return lives;
    }

    public int Bullet(){
        return bullet;
    }

    public void PerderBalas(){
        bullet -= 1;
        PrintBulletsInScreen();
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

    public void SaveGame()
    {
        var filePath=Application.persistentDataPath + "/save.dat";

        FileStream file;

        if (File.Exists(filePath))
            file=File.OpenWrite(filePath);

        else 
            file=File.Create(filePath);

        GameData data =new GameData();
        data.Score=score;

        BinaryFormatter bf =new BinaryFormatter();
        bf.Serialize(file, data);
        file.Close();

    }

    public void LoadGame()
    {
        var filePath=Application.persistentDataPath + "/save.dat";

        FileStream file;

        if (File.Exists(filePath))
            file=File.OpenRead(filePath);

        else {
            Debug.LogError("No se encuentra el archivo");
            
            return;
        
        }

        BinaryFormatter bf =new BinaryFormatter();
        GameData data = (GameData)bf.Deserialize(file);
        file.Close();

        score=data.Score;
        PrintScoreInScreen();

    }

    private void PrintScoreInScreen(){
        scoreText.text = "Monedas: " + score;
    }

    
    private void PrintLivesInScreen(){
        livesText.text = "Vida: " + lives;
    }

    private void PrintBulletsInScreen(){
        bulletText.text = "Balas: " + bullet;
    }
}
