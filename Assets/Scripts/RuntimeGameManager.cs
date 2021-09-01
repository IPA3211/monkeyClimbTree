using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class RuntimeGameManager : MonoBehaviour
{
    float timeCount;
    public GameObject canvas;
    ReadyUIManager readyUIManager;
    // Start is called before the first frame update
    void Awake(){
        readyUIManager = canvas.GetComponent<ReadyUIManager>();
        GameSystem.setCoin(SecurityPlayerPrefs.GetInt("Coin", 0));
    }

    // Update is called once per frame
    void Update()
    {
        if(GameSystem.CanTimeCount()){
            timeCount += Time.deltaTime;
        }

        if(timeCount > 1){
            GameSystem.addScore(10);
            timeCount = 0;
        }

        if(GameSystem.playDeadUI){
            GameSystem.playDeadUI = false;
            playerDead();
        }
    }

    public void gameStart(){
        if(!gameObject.GetComponent<CamMoveByLevel>().isMoving){
            readyUIManager.countDown();
            canvas.GetComponent<LobbyUIManager>().OnStartBtnClicked();
        }
    }
    public void playerDead(){
        canvas.GetComponent<GameoverUI>().startGameoverUI();
        SecurityPlayerPrefs.SetInt("Coin", GameSystem.getCoin());
    }
}
