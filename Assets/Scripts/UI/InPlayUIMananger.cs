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

    void FixedUpdate(){
        if(GameSystem.isStarted){
            InPlayUI.SetActive(true);
        }
        scoreText.text =  GameSystem.getScore().ToString();
        healthText.text = GameSystem.getHealth().ToString();
    }
}
