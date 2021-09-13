using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementNode : MonoBehaviour
{
    // Start is called before the first frame update
    public Text title, describe, count, btnText;
    public GameObject coinUI, MagnetUI, BoosterUI, SkinUI, HeartUI, clearUI;
    public Button btn;
    Achievement owner;
    AchievementManager achievementManager;

    void Start(){
        achievementManager = RuntimeGameManager.gameManager.GetComponent<AchievementManager>();
    }
    public void refresh(Achievement achieveInfo){
        owner = achieveInfo;
        title.text = achieveInfo.name;
        describe.text = achieveInfo.describe;
        setRewardUI(achieveInfo.rewardType);
        clearUI.SetActive(achieveInfo.isReceived);
        count.text = achieveInfo.getScore() + " / " + achieveInfo.toClear;
        btnText.text = achieveInfo.clearCoin.ToString();

        if(achieveInfo.isCleared && !achieveInfo.isReceived){
            btn.interactable = true;
        }
        else{
            btn.interactable = false;
        }
    }

    public void setRewardUI(RewardType rewardType){
        switch(rewardType){
            case RewardType.COIN :
                coinUI.SetActive(true);
                MagnetUI.SetActive(false);
                BoosterUI.SetActive(false);
                SkinUI.SetActive(false);
                HeartUI.SetActive(false);
            break;
            case RewardType.MAGNET :
                coinUI.SetActive(false);
                MagnetUI.SetActive(true);
                BoosterUI.SetActive(false);
                SkinUI.SetActive(false);
                HeartUI.SetActive(false);
            break;
            case RewardType.HEART :
                coinUI.SetActive(false);
                MagnetUI.SetActive(false);
                BoosterUI.SetActive(false);
                SkinUI.SetActive(false);
                HeartUI.SetActive(true);
            break;
            case RewardType.SKIN :
                coinUI.SetActive(false);
                MagnetUI.SetActive(false);
                BoosterUI.SetActive(false);
                SkinUI.SetActive(true);
                HeartUI.SetActive(false);
            break;
            case RewardType.BOOSTER :
                coinUI.SetActive(false);
                MagnetUI.SetActive(false);
                BoosterUI.SetActive(true);
                SkinUI.SetActive(false);
                HeartUI.SetActive(false);
            break;
        }
    }

    public void Destroy(){
        Destroy(gameObject);
    }

    public void OnBtnClicked(){
        owner.AchievementCleared();
        achievementManager.refreshDailyAchieve();
        achievementManager.refreshWeeklyAchieve();
        achievementManager.saveActivedAchieve();
    }
}
