using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameLevelSpawner : MonoBehaviour
{
    public GameObject levelDesignBtn;
    public List<Level> levels;
    float timeCount = 0;
    SpawnEnemy enemySpawner;
    SpawnEnvironment enviSpawner;

    void Start()
    {
        enemySpawner = GetComponent<SpawnEnemy>();
        enviSpawner = GetComponent<SpawnEnvironment>();

        GameSystem.maxLevel = levels.Count;
    }

    void Update()
    {
        if(GameSystem.isLevelChanged){
            if(levels.Count > GameSystem.getLevel()){
                enemySpawner.enemyLevel = levels[GameSystem.getLevel()].enemyLevel;
                enviSpawner.enviLevel = levels[GameSystem.getLevel()].enviLevel;
                GameSystem.isLevelChanged = false;
                levelDesignBtn.SetActive(false);
            }
            else{
                Debug.Log("debug Level");
                levelDesignBtn.SetActive(true);
                GameSystem.isLevelChanged = false;
            }
        }

        if(GameSystem.isStarted && !GameSystem.isDead && !GameSystem.isLevelUping && !GameSystem.isLeveluped)
            timeCount += Time.deltaTime;
        else
            timeCount = 0;

        if(levels.Count > GameSystem.getLevel()){
            if(timeCount > levels[GameSystem.getLevel()].levelTime){
                    GameSystem.isLevelUping = true;
            }
        }
    }
}
