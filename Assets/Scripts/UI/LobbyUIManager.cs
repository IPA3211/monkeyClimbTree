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
    // Start is called before the first frame update
    void Start()
    {
        
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
        stageText.text = "Stage " + (GameSystem.getLevel() / 3 + 1); 
    }
    public void OnRightBtnClicked(){
        GameSystem.stageUp();
        stageText.text = "Stage " + (GameSystem.getLevel() / 3 + 1); 
    }
    public void OnStartBtnClicked(){
        lobbyUI.SetActive(false);
    }
}
