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
    public Text scoreText2;
    public Text scoreText3;

    private int score;
    private int score2;
    private int score3;
    private int lives;

    private int bullet;

    // AudioSource audioSource;
    
    // Start is called before the first frame update
    void Start()
    {
        // audioSource = GetComponent<AudioSource>();
        bullet = 5;
        score = 0;
        score2 = 0;
        score3 = 0;
        
        lives = 3;
        PrintScoreInScreen();
        PrintScore2InScreen();
        PrintScore3InScreen();
        PrintLivesInScreen();
        PrintBulletsInScreen();
        
        LoadGame();
        // audioSource.PlayOneShot(WorldClip);
    }

    // Update is called once per frame
    public int Score(){
        return score;
    }
    public int Score2(){
        return score2;
    }
    public int Score3(){
        return score3;
    }

    public int Lives(){
        return lives;
    }

    public int Bullet(){
        return bullet;
    }

    public void PerderBalas(int bal){
        bullet += bal;
        PrintBulletsInScreen();
    }


    public void GanarPuntos(int puntos){
        score += puntos;
        PrintScoreInScreen();
    }
    public void GanarPuntos2(int puntos2){
        score2 += puntos2;
        PrintScore2InScreen();
    }
    public void GanarPuntos3 (int puntos3){
        score3 += puntos3;
        PrintScore3InScreen();
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
        data.Score2=score2;
        data.Score3=score3;
        data.Bullet = bullet;

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
        score2=data.Score2;
        score3=data.Score3;
        bullet=data.Bullet;
        PrintScoreInScreen();
        PrintScore2InScreen();
        PrintScore3InScreen();
        PrintBulletsInScreen();

    }

    private void PrintScoreInScreen(){
        scoreText.text = "Monedas: " + score;
    }
    private void PrintScore2InScreen(){
        scoreText2.text = "Monedas2: " + score2;
    }
    private void PrintScore3InScreen(){
        scoreText3.text = "Monedas3: " + score3;
    }

    
    private void PrintLivesInScreen(){
        livesText.text = "Vida: " + lives;
    }

    private void PrintBulletsInScreen(){
        bulletText.text = "Enemigos: " + bullet;
    }
}
