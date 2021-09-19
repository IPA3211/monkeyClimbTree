using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.SceneManagement;

public class RuntimeGameManager : MonoBehaviour
{
    public static GameObject gameManager;
    float timeCount;
    public GameObject canvas;
    public GameObject rocket;
    EndingManager endingManger;
    ReadyUIManager readyUIManager;
    GoogleManager netManager = null;
    // Start is called before the first frame update
    void Awake(){
        SecurityPlayerPrefs.SetString("Version", Application.version);
        gameManager = gameObject;
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
        GameSystem.timeToAd = SecurityPlayerPrefs.GetInt("timeToAd", 3);
    }

    // Update is called once per frame
    void Update()
    {
        if(GameSystem.isRestarted){
            if(GameSystem.hasBooster)
                rocket.SetActive(true);
        }

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
        if(GameSystem.getStage() == 0){
            if(GameSystem.getScore() >= 200)
                Counts.jungleScore++;
        }
        if(GameSystem.playerClearedStage < GameSystem.getStage() + 1)
            GameSystem.playerClearedStage = GameSystem.getStage() + 1;

        GameSystem.resetStartItem();

        checkedAD(() => {
            canvas.GetComponent<StageClearUI>().startStageClearUI();
            SecurityPlayerPrefs.SetInt("timeToAd", GameSystem.timeToAd);
            SecurityPlayerPrefs.SetInt("Coin", GameSystem.getCoin());
            SecurityPlayerPrefs.SetInt("playerClearedStage", GameSystem.playerClearedStage);
            GetComponent<AchievementManager>().refreshAchieve();

            endingsSave();
            if(netManager != null)
                netManager.SaveCloud();
        });

    }

    public void playerDead(){
        if(GameSystem.getStage() == 0){
            if(GameSystem.getScore() >= 200)
                Counts.jungleScore++;
        }

        if(GameSystem.getStage() == GameSystem.maxStage - 1){
            if(netManager != null){
                netManager.addInfLeaderboard(GameSystem.getScore());
            }
        }
        
        GetComponent<AchievementManager>().refreshAchieve();
        GameSystem.resetStartItem();

        checkedAD(() => {
            canvas.GetComponent<GameoverUI>().startGameoverUI();
            SecurityPlayerPrefs.SetInt("timeToAd", GameSystem.timeToAd);
            SecurityPlayerPrefs.SetInt("Coin", GameSystem.getCoin());
            GetComponent<AchievementManager>().refreshAchieve();
            
            endingsSave();
            if(netManager != null)
                netManager.SaveCloud();
        });
    }

    public void checkedAD(System.Action callBack){
        int coin = 10;
        GameSystem.timeToAd--;
        if(GameSystem.timeToAd <= 0){
            GameSystem.timeToAd = Random.Range(4, 7);
            canvas.GetComponent<PopUpUIManager>().setPopUpSelectMsgUI("저희 게임을 친구들에게 공유하고 " + coin + "코인의 보상을 받아보세요!", 
            () => {
                canvas.GetComponent<PopUpUIManager>().clear();
                callBack();
            }, 
            () => {
                shareLink("몽키키와 함께 전설의 바나나를 찾으러 떠나지 않을래?");
                canvas.GetComponent<PopUpUIManager>().setPopUpCoinUI(coin, () => {
                    canvas.GetComponent<PopUpUIManager>().compo.btnR.GetComponentInChildren<ParticleSystem>().Play();
                    canvas.GetComponent<PopUpUIManager>().clear();
                    GameSystem.addCoin(coin);
                    callBack();
                });
            });
        }
        else
            callBack();
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

        canvas.GetComponent<PopUpUIManager>().setPopUpSelectMsgUI("정말 리셋 하시겠습니까? 스킨, 코인, 진행상황이 모두 삭제됩니다.", 
            () => {
                canvas.GetComponent<PopUpUIManager>().clear();
            }, 
            () => {
                SecurityPlayerPrefs.DeleteAll();
                if(netManager != null)
                    netManager.DeleteCloud();

                JsonManager.DeleteAll();

                GameSystem.resetStartItem();
                GameSystem.restart();
                SceneManager.LoadScene("SampleScene");
                canvas.GetComponent<PopUpUIManager>().clear();
            });
    }

    public void shareLink(string msg){
        string subject = msg;
	    string body = "https://play.google.com/store/apps/details?id=com.MonkeyClimbtheTree.BananaofLegend";

#if UNITY_ANDROID && !UNITY_EDITOR
		AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
		AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
		intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
		intentObject.Call<AndroidJavaObject>("setType", "text/plain");
		intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), subject);
		intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), body);
		AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
		AndroidJavaObject jChooser = intentClass.CallStatic<AndroidJavaObject>("createChooser", intentObject, "Share Via");
		currentActivity.Call("startActivity", jChooser);
#endif
        Counts.watchADCount++;
    }
}
