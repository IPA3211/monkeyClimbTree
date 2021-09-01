using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUIManager : MonoBehaviour
{
    public GameObject lobbyUI;
    [Header("Background")]
    public GameObject etcUIBackground;
    [Header("UIs")]
    public GameObject skinUI;
    public GameObject endingUI, rankingUI, settingUI;
    public Text stageText;
    public Text coinText;

    // Start is called before the first frame update
    void Start()
    {
        coinText.text = GameSystem.getCoin().ToString();
    }
    private void OnEnable()
    {
        coinText.text = GameSystem.getCoin().ToString();
    }

    public void OnSkinBtnClicked(){
        etcUIBackground.SetActive(true);
        skinUI.SetActive(true);
    }
    public void OnEndingBtnClicked(){
        etcUIBackground.SetActive(true);
        endingUI.SetActive(true);
    }
    public void OnRankingBtnClicked(){
        etcUIBackground.SetActive(true);
        rankingUI.SetActive(true);
    }
    public void OnSettingBtnClicked(){
        etcUIBackground.SetActive(true);
        settingUI.SetActive(true);
    }
    public void OnCoinBtnClicked(){

    }
    public void OnLeftBtnClicked(){
        GameSystem.stageDown();
        stageText.text = "Stage " + (GameSystem.getStage() + 1); 
        
    }
    public void OnRightBtnClicked(){
        GameSystem.stageUp();
        stageText.text = "Stage " + (GameSystem.getStage() + 1); 
        if(GameSystem.getStage() == GameSystem.maxStage){
            stageText.text = "Debug Stage"; 
        }
    }
    public void OnStartBtnClicked(){
        lobbyUI.SetActive(false);
    }
}
