using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementNode : MonoBehaviour
{
    // Start is called before the first frame update
    public Text title, describe, count, btnText;
    public Button btn;
    Achievement owner;
    AchievementManager achievementManager;

    void Enabled(){
        achievementManager = RuntimeGameManager.gameManager.GetComponent<AchievementManager>();
    }
    public void refresh(Achievement achieveInfo){
        owner = achieveInfo;
        title.text = achieveInfo.name;
        describe.text = achieveInfo.describe;
        count.text = achieveInfo.getScore() + " / " + achieveInfo.toClear;
        btnText.text = achieveInfo.clearCoin.ToString();

        if(achieveInfo.isCleared && !achieveInfo.isReceived){
            btn.interactable = true;
        }
        else{
            btn.interactable = false;
        }
    }

    public void OnBtnClicked(){
        owner.AchievementCleared();
        achievementManager.saveActivedAchieve();
    }
}
