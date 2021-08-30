using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Level
{
    public float levelTime = 60;
    public EnemyLevel enemyLevel;
    public EnviLevel enviLevel;
}

[System.Serializable]
public class EnemyLevel{
    [Header ("Config")]
    public float spawnPeriod = 5;
    public int minSpawnAmount = 1;
    public int maxSpawnAmount = 2;
    [Header ("Enemies")]
    public bool spawnWall = true;
    public int wallAmount = 2;

    [Space(10f)]
    public bool spawnSnake = false;
    public float snakeSpeed = 12f;
    
    [Space(10f)]
    public bool spawnPanzee = false;
    public float panzeeXPower = 12f;
    public float panzeeYPower = 15f;

    [Space(10f)]
    public bool spawnApple = false;

    [Space(10f)]
    public bool spawnEagle = false;
}

[System.Serializable]
public class EnviLevel{
    [Header ("Config")]
    public float spawnPeriod = 30;
    [Header ("Enemies")]
    public bool spawnBranch = true;
    public int branchNum = 2;
   
    [Space (10f)]
    public bool spawnBush = false;
    public int bushNum = 10;
}
