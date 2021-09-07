using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem
{
    public static bool isRestarted = false;
    private static bool isPasued = false;
    public static bool isStarted = false;
    public static bool isDead = false;
    public static bool playDeadUI = false;
    public static bool playClearUI = false;

    public static bool hasMagnetic = false;
    public static bool hasBooster = false;
    public static bool hasShield = false;

    public static float playerHeight = 0;
    private static float timeScale = 1;
    private static float maxHeight = 0;
    public static float stageclearHeight;
    private static int score = 0;
    private static int coin = 0;
    private static int coinEarned = 0;
    private static int health = 3;
    private static int potion = 0;
    public static int playerHealth = 3;
    private static int level = 0;
    private static int stage = 0;
    public static int maxStage = 15;
    public static bool isLevelUping = false;
    public static bool isStageCleared = false;
    public static bool isLevelChanged = true;
    public static bool isStageChanged = true;
    public static bool isCanVive = true;
    
    public static void resetStartItem()
    {
        hasMagnetic = false;
        hasBooster = false;
        hasShield = false;
    }
    public static void setTimeScale(float newScale){
        timeScale = newScale;
        Time.timeScale = timeScale;
    }
    public static float getTimeScale(){
        return timeScale;
    }
    public static void setPause(bool setting){
        isPasued = setting;
        if(isPasued){
            Time.timeScale = 0;
        }
        else{
            Time.timeScale = timeScale;
        }
    }
    public static bool getPause(){
        return isPasued;
    }
    public static void setCoin(int cnt)
    {
        if(cnt < 0)
        {
            coin = 0;
        }
        else if(cnt > 99999)
        {
            coin = 99999;
        }
        else
        {
            coin = cnt;
        }
        
    }
    public static int getCoin()
    {
        return coin;
    }
    public static void addCoin(int amount)
    {
        setCoin(getCoin() + amount);
    }
    public static void setCoinEarned(int cnt)
    {
        if (cnt < 0)
        {
            coinEarned = 0;
        }
        else if (cnt > 99999)
        {
            coinEarned = 99999;
        }
        else
        {
            coinEarned = cnt;
        }

    }    
    public static int getCoinEarned()
    {
        return coinEarned;
    }
    public static void addCoinEarned(int amount)
    {
        setCoinEarned(getCoinEarned() + amount);
    }



    public static void damaged(int amount){
        setHealth(getHealth() - amount);
    }
    public static void setHealth(int newHealth){
        health = newHealth;
        if(health <= 0){
            health = 0;
            isDead = true;
            playDeadUI = true;
        }
        else{
            isDead = false;
            playDeadUI = false;
        }
    }
    public static int getHealth(){
        return health;
    }

    public static void setPotion(int newPotion)
    {
        potion = newPotion;
    }

    public static int getPotion()
    {
        return potion;
    }

    public static void addScore(int s){
        score += s;
    }
    public static int getScore(){
        return score + (int)maxHeight;
    }
    public static void setMaxHeight(float height)
    {
        if (height > maxHeight)
            maxHeight = height;
    }
    public static int getLevel(){
        return level;
    }
    public static void levelUp(){
        setLevel(getLevel() + 1);
    }
    public static void setStage(int newStage){
        stage = newStage;
        if(stage > maxStage){
            stage = maxStage;
        }
        else if(stage < 0){
            stage = 0;
        }
        isStageChanged = true;
        setLevel(0);
    }
    public static void stageUp(){
        setStage(stage + 1);
        AudioManager.instance.ChangeBGM();
    }
    public static void stageDown(){
        setStage(stage - 1);
        AudioManager.instance.ChangeBGM();
    }

    public static int getStage(){
        return stage;
    }
    public static void setLevel(int newLevel){
        level = newLevel;
        if (level <= 0){
            level = 0;
        }
        isLevelChanged = true;
    }

    public static void restart(){
        setHealth(playerHealth);
        isRestarted = true;
        isStarted = false;
        isPasued = false;
        isLevelUping = false;
        isStageCleared = false;
        setStage(0);
        setLevel(0);
        maxHeight = 0;
        playerHeight = 0;        
        coinEarned = 0;
        score = 0;
        potion = 0;
    }

    public static void nextStageStart(){
        isRestarted = true;
        isStarted = false;
        isPasued = false;
        isLevelUping = false;
        isStageCleared = false;
        setLevel(0);
        score += (int)maxHeight;
        maxHeight = 0;
        playerHeight = 0;
        coinEarned = 0;
    }

    public static bool CanTimeCount(){
        return isStarted && !isDead && !isLevelUping && !isStageCleared && !isRestarted;
    }
}
