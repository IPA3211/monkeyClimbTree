using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyUIManager : MonoBehaviour
{
    public GameObject lobbyUI;
    [Header("Background")]
    public GameObject etcUIBackground;
    [Header("UIs")]
    public GameObject skinUI;
    public GameObject endingUI, rankingUI, settingUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameSystem.isStarted){
            lobbyUI.SetActive(false);
        }
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
        
    }
    public void OnRightBtnClicked(){
        
    }
}
