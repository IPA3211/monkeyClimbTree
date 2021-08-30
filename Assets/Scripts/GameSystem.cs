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
    public static float playerHeight = 0;
    private static int score = 0;
    private static int coin = 0;
    private static int health = 3;
    public static int playerHealth = 3;
    private static int level = 0;
    public static int maxLevel = 15;
    public static bool isLevelUping = false;
    public static bool isLeveluped = false;
    public static bool isLevelChanged = true;
    public static bool isCanVive = true;
    private static float LevelUpTime = 60;
    private static float LastLevelUpTime = 90;
    
    public static void setPause(bool setting){
        isPasued = setting;
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
    public static void addScore(int s){
        score +=s;
    }
    public static int getScore(){
        return score + (int)playerHeight;
    }
    public static int getLevel(){
        return level;
    }
    public static void levelUp(){
        setLevel(getLevel() + 1);
        isLeveluped = true;
    }
    public static void stageUp(){
        if((getLevel() / 3) * 3 + 3 <= maxLevel){
            setLevel((int)(getLevel() / 3) * 3 + 3);
        }
    }
    public static void stageDown(){
        setLevel((int)(getLevel() / 3) * 3 - 3);
        
    }
    public static void setLevel(int newLevel){
        level = newLevel;
        if(level > maxLevel){
            level = maxLevel;
        }
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
        isLeveluped = false;
        playerHeight = 0;
        score = 0;
    }

    public static float getLevelUpTime(){
        if(level % 3 == 2){
            return LastLevelUpTime;
        }
        else{
            return LevelUpTime;
        }
    }
}
