using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InPlayUIMananger : MonoBehaviour
{
    
    [Header ("In Play UI")]
    public GameObject InPlayUI;
    public GameObject pauseMenu;
    public Text scoreText;
    public Text coinText;
    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;
    public GameObject heart4;
    public GameObject heart5;
    public GameObject banana1;
    public GameObject banana2;
    public GameObject banana3;
    public Image progressBar;
    int health;
    int banana;


    void FixedUpdate(){
        if(GameSystem.isStarted){
            InPlayUI.SetActive(true);
        }
        scoreText.text =  GameSystem.getScore().ToString();
        health = GameSystem.getHealth();
        banana = GameSystem.getBanana();

        if (health == 5)
        {
            heart1.SetActive(true);
            heart2.SetActive(true);
            heart3.SetActive(true);
            heart4.SetActive(true);
            heart5.SetActive(true);
        }
        else if (health == 4)
        {
            heart1.SetActive(true);
            heart2.SetActive(true);
            heart3.SetActive(true);
            heart4.SetActive(true);
            heart5.SetActive(false);
        }
        else if (health == 3)
        {
            heart1.SetActive(true);
            heart2.SetActive(true);
            heart3.SetActive(true);
            heart4.SetActive(false);
            heart5.SetActive(false);
        }
        else if (health == 2)
        {
            heart1.SetActive(true);
            heart2.SetActive(true);
            heart3.SetActive(false);
            heart4.SetActive(false);
            heart5.SetActive(false);
        }
        else if(health == 1)
        {
            heart1.SetActive(true);
            heart2.SetActive(false);
            heart3.SetActive(false);
            heart4.SetActive(false);
            heart5.SetActive(false);
        }
        else if(health <= 0)
        {
            heart1.SetActive(false);
            heart2.SetActive(false);
            heart3.SetActive(false);
            heart4.SetActive(false);
            heart5.SetActive(false);
        }



        if (banana == 0)
        {
            banana1.SetActive(false);
            banana2.SetActive(false);
            banana3.SetActive(false);
        }
        else if (banana == 1)
        {
            banana1.SetActive(true);
        }
        else if (banana == 2)
        {
            banana1.SetActive(true);
            banana2.SetActive(true);
        }
        else if (banana >= 3)
        {
            banana1.SetActive(true);
            banana2.SetActive(true);
            banana3.SetActive(true);
        }
        
        coinText.text = GameSystem.getCoin().ToString();
        progressBar.fillAmount = GameSystem.playerHeight / GameSystem.stageclearHeight;
    }

    public void OnPauseBtnClicked(){
        GameSystem.setPause(!GameSystem.getPause());
    }
}
