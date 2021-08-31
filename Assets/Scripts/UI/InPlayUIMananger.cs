using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InPlayUIMananger : MonoBehaviour
{
    
    [Header ("In Play UI")]
    public GameObject InPlayUI;
    public Text scoreText;
    public Text healthText;
    public Text coinText;
    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;
    int health;

    void FixedUpdate(){
        if(GameSystem.isStarted){
            InPlayUI.SetActive(true);
        }
        scoreText.text =  GameSystem.getScore().ToString();
        healthText.text = GameSystem.getHealth().ToString();
        health = GameSystem.getHealth();
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
        coinText.text = GameSystem.getCoin().ToString();
    }
}
