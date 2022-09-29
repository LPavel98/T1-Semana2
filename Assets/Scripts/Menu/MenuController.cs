using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public SpriteRenderer srCharacter;
    public Sprite[] sprites;
    private int next = 1;
    // Start is called before the first frame update
    void Start(){
    }
   public void StartGame(){
    SceneManager.LoadScene(0);
   }
   public void ScoreGame(){
    SceneManager.LoadScene(2);
   }
   public void BacktoMenu(){
    SceneManager.LoadScene(1);
   }
   public void ChangeCharacter(){
    srCharacter.sprite = sprites[next];
    next++;
    if (next  == sprites.Length)
    {
        next = 0;
    }
   }
}
