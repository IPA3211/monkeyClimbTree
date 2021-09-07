using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    public GameObject levelDesignBtn;
    public List<Stage> stages;
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

        if(GameSystem.isRestarted){
            GameSystem.isLevelChanged = true;
            GameSystem.isStageChanged = true;
        }
    }

    void Update()
    {
        if(!stages[GameSystem.getStage()].isDebugStage){
            Level level = stages[GameSystem.getStage()].levels[GameSystem.getLevel()];
            if(GameSystem.isLevelChanged){
                enemySpawner.enemyLevel = level.enemyLevel;
                enviSpawner.enviLevel = level.enviLevel;
                GameSystem.setTimeScale(level.timeScale);

                
                if(GameSystem.isStageChanged){
                    bgSpriteSetter.ChangeSpriteSet(stages[GameSystem.getStage()].spriteSet);
                    GameSystem.stageclearHeight = stages[GameSystem.getStage()].levels[stages[GameSystem.getStage()].levels.Count - 1].LevelChangeHeight;
                    GameSystem.isStageChanged = false;
                }
                bgFilterSetter.StartCoroutine("ChangeColor", level.LevelbgFilterColor);
                
                if(GameSystem.getLevel() != 0)
                    enviSpawner.Spawn();

                levelDesignBtn.SetActive(false);
                GameSystem.isLevelChanged = false;
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
                enemySpawner.enemyLevel = stages[0].levels[0].enemyLevel;
                bgSpriteSetter.ChangeSpriteSet(stages[GameSystem.getStage()].spriteSet);
                Debug.Log("debug Level");
                levelDesignBtn.SetActive(true);
                GameSystem.isLevelChanged = false;
            }
        }
        
        
    }
}
