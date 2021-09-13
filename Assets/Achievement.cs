using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counts{
    public static int jumpCount = 0;
    public static int jungleScore = 0;
    public static int feverCount = 0;
    public static int buyItemCount = 0;
    public static int D_DailyMissionCount = 0;
    public static int W_DailyMissionCount = 0;
    public static int skinGambleCount = 0;
    public static int unfortuneCount = 0;
    public static int watchADCount = 0;
    public static int logInCount = 0;

    public static void clear(){
        jumpCount = 0;
        jungleScore = 0;
        feverCount = 0;
        buyItemCount = 0;
        D_DailyMissionCount = 0;
        W_DailyMissionCount = 0;
        skinGambleCount = 0;
        unfortuneCount = 0;
        watchADCount = 0;
        logInCount = 0;
    }
}

public enum QuestType
{
    JUMP_QUEST,
    JUNGLE_QUEST,
    FEVER_QUEST,
    BUY_QUEST,
    D_MISSION_QUEST,
    W_MISSION_QUEST,
    GAMBLE_QUEST,
    UNFORTUNE_QUEST,
    AD_QUEST,
    LOGIN_QUEST

}

public enum RewardType{
    COIN, MAGNET, SKIN, BOOSTER, HEART

}

[System.Serializable]
public class Achievement{
    public string name;
    public string describe;
    public int toClear;
    int score = 0;
    public bool isCleared = false;
    public bool isReceived = false;
    public QuestType questType;
    public RewardType rewardType;
    public int clearCoin = 50;
    
    [HideInInspector]
    public AchievementNode uiNode = null;
    [HideInInspector]
    public bool isDaily = false;

    public void setScore(int newScore){
        score = newScore;
        if(toClear <= score){
            if(isCleared == false){
                isCleared = true;
                score = toClear;
            }
        }
    }
    public void addScore(int newScore){
        setScore(score + newScore);
    }

    public int getScore(){
        return score;
    }

    public void countScore(){
        switch(questType){
            case QuestType.JUMP_QUEST:
                addScore(Counts.jumpCount);
                break;
            
            case QuestType.JUNGLE_QUEST :
                addScore(Counts.jungleScore);
                break;

            case QuestType.FEVER_QUEST :
                addScore(Counts.feverCount);       
            break;

            case QuestType.BUY_QUEST :
                addScore(Counts.buyItemCount);
            break;

            case QuestType.D_MISSION_QUEST :
                addScore(Counts.D_DailyMissionCount);
                Counts.D_DailyMissionCount = 0;
            break;
            case QuestType.W_MISSION_QUEST :
                addScore(Counts.W_DailyMissionCount);
                Counts.W_DailyMissionCount = 0;
            break;

            case QuestType.GAMBLE_QUEST :
                addScore(Counts.skinGambleCount);
            break;

            case QuestType.UNFORTUNE_QUEST :
                addScore(Counts.unfortuneCount);
            break;

            case QuestType.AD_QUEST :
                addScore(Counts.watchADCount);
            break;

            case QuestType.LOGIN_QUEST :
                Debug.LogWarning("wowwowowo" + Counts.logInCount);
                addScore(Counts.logInCount);
            break;

        }
    }

    public void giveReward(){
        if(isDaily){
            Counts.D_DailyMissionCount++;
            Counts.W_DailyMissionCount++;
        }

        switch(rewardType){
            case RewardType.COIN :
                GameSystem.addCoin(clearCoin);
            break;
            case RewardType.MAGNET :
                GameSystem.hasMagnetic = true;
            break;
            case RewardType.HEART :
                GameSystem.hasHeartPlus = true;
            break;
            case RewardType.SKIN :
                GameSystem.hasFreeSkin = true;
            break;
            case RewardType.BOOSTER :
                GameSystem.hasBooster = true;
            break;
        }
    }

    public void AchievementCleared(){
        giveReward();
        isReceived = true;
        uiNode.refresh(this);
        SecurityPlayerPrefs.SetInt("Coin", GameSystem.getCoin());
    }

    public void refresh(){
        countScore();
        if(isReceived == false && isCleared == true){
            GameSystem.warnAchieve = true;
        }
        uiNode.refresh(this);
    }
}