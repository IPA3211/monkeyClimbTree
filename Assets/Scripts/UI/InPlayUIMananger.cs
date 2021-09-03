using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InPlayUIMananger : MonoBehaviour
{
    
    [Header ("In Play UI")]
    public GameObject InPlayUI;
    public Text scoreText;
    public Text coinText;
    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;
    public GameObject potion1;
    public GameObject potion2;
    public GameObject potion3;
    int health;
    int potion;

    void FixedUpdate(){
        if(GameSystem.isStarted){
            InPlayUI.SetActive(true);
        }
        scoreText.text =  GameSystem.getScore().ToString();
        health = GameSystem.getHealth();
        potion = GameSystem.getPotion();

        if (health == 3)
        {
            heart1.SetActive(true);
            heart2.SetActive(true);
            heart3.SetActive(true);
        }
        else if (health == 2)
        {
            heart3.SetActive(false);
        }
        else if (health == 1)
        {
            heart2.SetActive(false);
            heart3.SetActive(false);
        }
        else if (health <= 0)
        {
            heart1.SetActive(false);
            heart2.SetActive(false);
            heart3.SetActive(false);
        }



        if (potion == 0)
        {
            potion1.SetActive(false);
            potion2.SetActive(false);
            potion3.SetActive(false);
        }
        else if (potion == 1)
        {
            potion1.SetActive(true);
        }
        else if (potion == 2)
        {
            potion1.SetActive(true);
            potion2.SetActive(true);
        }
        else if (potion >= 3)
        {
            potion1.SetActive(true);
            potion2.SetActive(true);
            potion3.SetActive(true);
        }

        coinText.text = GameSystem.getCoin().ToString();
    }
}
