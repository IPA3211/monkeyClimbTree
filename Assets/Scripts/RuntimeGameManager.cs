using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.SceneManagement;

public class RuntimeGameManager : MonoBehaviour
{
    float timeCount;
    public GameObject canvas;
    EndingManager endingManger;
    ReadyUIManager readyUIManager;
    GoogleManager netManager = null;
    // Start is called before the first frame update
    void Awake(){
        endingManger = GetComponent<EndingManager>();
        readyUIManager = canvas.GetComponent<ReadyUIManager>();
        GameObject temp = GameObject.FindWithTag("Network");
        
        if(temp != null){
            netManager = temp.GetComponent<GoogleManager>();
        }
        else
            JsonManager.Load();
        
        if(netManager != null && netManager.loadingFailed){
            JsonManager.Load();
            // 네트워크에서 불러오는데에 실패했을 때
            if(netManager.CheckLogin()){
                // 네트워크에 연결된 상태라면
                if(SecurityPlayerPrefs.GetString("UserID", "").Equals(Social.localUser.id) || SecurityPlayerPrefs.GetString("UserID", "").Equals(""))
                    // 현재 네트워크에 연결된 ID 와 로컬에 있는 ID 가 같거나 UserID 가 적혀있지 않다면
                    netManager.SaveCloud();
                else{
                    // 로컬에 적혀있는 ID 가 현재 연결된 ID 와 다르다면
                    SecurityPlayerPrefs.DeleteAll();
                    SecurityPlayerPrefs.SetString("UserID", Social.localUser.id);
                }
            }
        }
        
        GameSystem.setCoin(SecurityPlayerPrefs.GetInt("Coin", 0));
        GameSystem.playerClearedStage = SecurityPlayerPrefs.GetInt("playerClearedStage", 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(GameSystem.CanTimeCount()){
            timeCount += Time.deltaTime;
        }

        if(timeCount > 1){
            //GameSystem.addScore(10);
            timeCount = 0;
        }

        if(GameSystem.playDeadUI){
            GameSystem.playDeadUI = false;
            playerDead();
        }

        if(GameSystem.playClearUI){
            GameSystem.playClearUI = false;
            stageClear();
        }
    }

    public void gameStart(){
        if(!gameObject.GetComponent<CamMoveByLevel>().isMoving){
            readyUIManager.countDown(0);
            canvas.GetComponent<LobbyUIManager>().OnStartBtnClicked();
        }
    }

    public void upStage()
    {
        readyUIManager.countDownStageCleared();
    }

    public void stageClear(){
        if(GameSystem.playerClearedStage < GameSystem.getStage() + 1)
            GameSystem.playerClearedStage = GameSystem.getStage() + 1;

        canvas.GetComponent<StageClearUI>().startStageClearUI();
        SecurityPlayerPrefs.SetInt("Coin", GameSystem.getCoin());
        SecurityPlayerPrefs.SetInt("playerClearedStage", GameSystem.playerClearedStage);

        
        endingsSave();
        if(netManager != null)
            netManager.SaveCloud();
    }

    public void playerDead(){
        GameSystem.resetStartItem();
        canvas.GetComponent<GameoverUI>().startGameoverUI();
        SecurityPlayerPrefs.SetInt("Coin", GameSystem.getCoin());
        endingsSave();
        if(netManager != null)
            netManager.SaveCloud();
    }

    public void endingsSave()
    {
        List<Ending> endings = endingManger.endings;
        for (int i = 0; i<endings.Count; i++)
        {
            if (endings[i].CheckUnlock())
                SecurityPlayerPrefs.SetInt("Ending" + i.ToString(), 1);
            else
                SecurityPlayerPrefs.SetInt("Ending" + i.ToString(), 0);
        }
    }

    public void debugDelData(){
        SecurityPlayerPrefs.DeleteAll();
        if(netManager != null)
            netManager.DeleteCloud();

        JsonManager.DeleteAll();

        GameSystem.resetStartItem();
        GameSystem.restart();
        SceneManager.LoadScene("SampleScene");
    }
}
