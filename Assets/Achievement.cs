using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counts{
    public static int jumpCount = 0;
    public static int panzeeCount = 0;

    public static void clear(){
        jumpCount = 0;
        panzeeCount = 0;
    }
}

public enum QuestType
{
    JUMP_QUEST,
    PANZEE_QUEST,

}

[System.Serializable]
public class Achievement{
    public string name;
    public string describe;
    public int toClear;
    public int clearCoin = 50;
    int score = 0;
    public bool isCleared = false;
    public bool isReceived = false;
    public QuestType questType;
    
    [HideInInspector]
    public AchievementNode uiNode = null;

    public void setScore(int newScore){
        score = newScore;
        if(toClear <= score){
            isCleared = true;
            score = toClear;
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
            
            case QuestType.PANZEE_QUEST :
                addScore(Counts.panzeeCount);
                break;
        }
    }

    public void AchievementCleared(){
        GameSystem.addCoin(clearCoin);
        isReceived = true;
        uiNode.refresh(this);
        SecurityPlayerPrefs.SetInt("Coin", GameSystem.getCoin());
    }

    public void refresh(){
        countScore();
        uiNode.refresh(this);
    }
}