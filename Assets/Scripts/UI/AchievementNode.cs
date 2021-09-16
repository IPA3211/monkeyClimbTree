using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementNode : MonoBehaviour
{
    // Start is called before the first frame update
    public Text title, describe, count, btnText;
    public GameObject coinUI, itemUI, clearUI;
    public ParticleSystem particle;
    Image itemUIImage;
    public Sprite magnet, booster, heart, skin;
    public Button btn;
    Achievement owner;
    AchievementManager achievementManager;

    void OnEnable(){
        achievementManager = RuntimeGameManager.gameManager.GetComponent<AchievementManager>();
    }
    public void refresh(Achievement achieveInfo){
        itemUIImage = itemUI.GetComponent<Image>();
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
        var tt = particle.textureSheetAnimation;
        switch(rewardType){
            case RewardType.COIN :
                coinUI.SetActive(true);
                itemUI.SetActive(false);
            break;
            case RewardType.MAGNET :
                coinUI.SetActive(false);
                itemUI.SetActive(true);
                itemUIImage.sprite = magnet;
                tt.SetSprite(0, magnet);
            break;
            case RewardType.HEART :
                coinUI.SetActive(false);
                itemUI.SetActive(true);
                itemUIImage.sprite = heart;
                tt.SetSprite(0, heart);
            break;
            case RewardType.SKIN :
                coinUI.SetActive(false);
                itemUI.SetActive(true);
                itemUIImage.sprite = skin;
                tt.SetSprite(0, skin);
            break;
            case RewardType.BOOSTER :
                coinUI.SetActive(false);
                itemUI.SetActive(true);
                itemUIImage.sprite = booster;
                tt.SetSprite(0, booster);
            break;
        }
    }

    public void Destroy(){
        Destroy(gameObject);
    }

    public void OnBtnClicked(){
        AudioManager.instance.Play("AchieveClear");
        owner.AchievementCleared();
        achievementManager.refreshDailyAchieve();
        achievementManager.refreshWeeklyAchieve();
        achievementManager.saveActivedAchieve();
        particle.Play();
    }
}
