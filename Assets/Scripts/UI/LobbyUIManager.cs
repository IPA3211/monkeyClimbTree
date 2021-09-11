using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUIManager : MonoBehaviour
{
    public GameObject lobbyUI;

    [Header("UIs")]
    public Text stageText;
    public Text coinText;
    public GameObject rightBtn, leftBtn;
    public GameObject rocket;

    // Start is called before the first frame update
    void Start()
    {
        coinText.text = GameSystem.getCoin().ToString();
        StageTextChange();
    }
    private void FixedUpdate()
    {
        coinText.text = GameSystem.getCoin().ToString();
    }
    private void OnEnable()
    {
        coinText.text = GameSystem.getCoin().ToString();
        StageTextChange();
    }
    public void OnLeftBtnClicked(){
        GameSystem.stageDown();
        StageTextChange();
        
    }
    public void OnRightBtnClicked(){
        GameSystem.stageUp();
        StageTextChange();
    }
    public void OnStartBtnClicked(){
        lobbyUI.SetActive(false);
    }

    void StageTextChange(){
        if(GameSystem.getStage() == GameSystem.maxStage || GameSystem.getStage() == GameSystem.playerClearedStage)
            rightBtn.SetActive(false);
        else
            rightBtn.SetActive(true);
    
        if(GameSystem.getStage() == 0){
            leftBtn.SetActive(false);
            rocket.SetActive(false);
        }
        else{
            leftBtn.SetActive(true);
            rocket.SetActive(true);
        }

        stageText.text = "Stage " + (GameSystem.getStage() + 1); 
        if(GameSystem.getStage() == GameSystem.maxStage){
            stageText.text = "Debug Stage"; 
        }
    }

    public void DebugLevelBtn(){
        GameSystem.playerClearedStage = GameSystem.maxStage;
        SecurityPlayerPrefs.SetInt("playerClearedStage", GameSystem.playerClearedStage);
        StageTextChange();
    }
}
