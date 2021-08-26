using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem
{
    private static bool isPasued = false;
    public static bool isStarted = false;
    public static float playerHeight = 0;
    private static int score = 0;
    public static int health = 3;
    public static int level = 0;
    public static int maxLevel = 15;
    public static bool isLevelUping = false;
    public static bool isLeveluped = false;
    
    public static void setPause(bool setting){
        isPasued = setting;
    }
    public static bool getPause(){
        return isPasued;
    }
    public static void damaged(int amount){
        health -= amount;
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
        setLevel((int)(getLevel() / 3) * 3 + 3);
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
    }
}
