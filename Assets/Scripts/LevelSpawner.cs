using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    public GameObject levelDesignBtn;
    public List<Stage> stages;
    float timeCount = 0;
    SpawnEnemy enemySpawner;
    SpawnEnvironment enviSpawner;
    BgFilterSetter bgFilterSetter;
    BgSpriteSetter bgSpriteSetter;
    void Start()
    {
        enemySpawner = GetComponent<SpawnEnemy>();
        enviSpawner = GetComponent<SpawnEnvironment>();
        bgFilterSetter = GetComponent<BgFilterSetter>();
        bgSpriteSetter = GetComponent<BgSpriteSetter>();

        GameSystem.maxStage = stages.Count - 1;
    }

    void Update()
    {
        if(!stages[GameSystem.getStage()].isDebugStage){
            Level level = stages[GameSystem.getStage()].levels[GameSystem.getLevel()];
            if(GameSystem.isLevelChanged){
                enemySpawner.enemyLevel = level.enemyLevel;
                enviSpawner.enviLevel = level.enviLevel;
                bgFilterSetter.StartCoroutine("ChangeColor", level.LevelbgFilterColor);

                GameSystem.isLevelChanged = false;

                levelDesignBtn.SetActive(false);
            }

            if(stages[GameSystem.getStage()].levels.Count - 1 > GameSystem.getLevel()){
                if(GameSystem.playerHeight > level.LevelChangeHeight){
                    Debug.Log("levelUp");
                    GameSystem.levelUp();
                }
            }
            else {
                if(GameSystem.playerHeight > level.LevelChangeHeight){
                    if(!bgSpriteSetter.isEnd){
                        Debug.Log("stageUp");
                        bgSpriteSetter.isEnd = true;
                    }
                }
            }
        }

        else{
            if(GameSystem.isLevelChanged){
                Debug.Log("debug Level");
                levelDesignBtn.SetActive(true);
                GameSystem.isLevelChanged = false;
            }
        }
        
        
    }
}
