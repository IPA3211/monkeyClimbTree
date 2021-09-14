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
    [Header("Warns")]
    public GameObject infWarnText;
    public GameObject coin, skin, ending, ranking, achieve;

    GoogleManager netManager = null;

    // Start is called before the first frame update
    void Start()
    {
        coinText.text = GameSystem.getCoin().ToString();
        StageTextChange();

        GameObject temp = GameObject.FindWithTag("Network");
        setWarning();
        if(temp != null){
            netManager = temp.GetComponent<GoogleManager>();
        }
        else
            JsonManager.Load();
    }
    private void FixedUpdate()
    {
        if(GameSystem.hasBooster){
            rocket.SetActive(true);
        }
        coinText.text = GameSystem.getCoin().ToString();
        setWarning();
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
        if(GameSystem.getStage() == GameSystem.maxStage -1){
            GameSystem.resetStartItem();
        }
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
        
        infWarnText.SetActive(false);

        stageText.text = "Stage " + (GameSystem.getStage() + 1); 
        if(GameSystem.getStage() == GameSystem.maxStage - 1){
            infWarnText.SetActive(true);
            stageText.text = "무한 모드"; 
        }
        if(GameSystem.getStage() == GameSystem.maxStage){
            stageText.text = "Debug Stage"; 
        }
    }

    void setWarning(){
        coin.SetActive(false);
        skin.SetActive(false);
        ranking.SetActive(false);
        achieve.SetActive(false);
        ending.SetActive(false);

        if(GameSystem.getCoin() > 100 || GameSystem.hasFreeSkin){
            coin.SetActive(true);
        }

        if(GameSystem.warnAchieve){
            achieve.SetActive(true);
        }
        if(GameSystem.warnEnding){
            ending.SetActive(true);
        }
        if(GameSystem.warnRanking){
            ranking.SetActive(true);
        }
        if(GameSystem.warnSkin){
            skin.SetActive(true);
        }
    }

    public void DebugLevelBtn(){
        GameSystem.playerClearedStage = GameSystem.maxStage;
        SecurityPlayerPrefs.SetInt("playerClearedStage", GameSystem.playerClearedStage);
        StageTextChange();
    }

    public void onRankingBtnClicked(){
        if(netManager != null)
            netManager.ShowinfLeaderboardUI();
    }
}
