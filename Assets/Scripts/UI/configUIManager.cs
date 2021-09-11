using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class configUIManager : MonoBehaviour
{
    // Start is called before the first frame update
    [Header ("Config")]
    public GameObject player;
    public GameObject gameManager;
    public GameObject pauseMenu;
    [Space (10f)]
    public InputField timeScale;
    
    [Space (10f)]
    public Dropdown mapDropdown;

    [Space (10f)]
    public InputField enemyP;
    public InputField enviP;
    public InputField wallAmount;
    public InputField minSpawn;
    public InputField maxSpawn;
    public Toggle wallT;
    public Toggle snakeT;
    public Toggle panzeeT;
    public Toggle eagleT;
    public Toggle appleT;
    public Toggle branchT;
    public Toggle bushT;
    public Toggle dolphinT;
    public Toggle jellyT;
    public Toggle planeT;
    public Toggle ThunderT;
    public Toggle UFOT;
    public Toggle satelliteT;

    playerController pctrl;
    Rigidbody2D rigi;
    SpawnEnemy enemy;
    SpawnEnvironment envi;
    LevelSpawner levelSpawner;

    public void ChangePauseSetting(){
        GameSystem.setPause(!GameSystem.getPause());
        pauseMenu.SetActive(GameSystem.getPause());

        if(!GameSystem.getPause()){
            changeConfig();
        }

        if(GameSystem.getPause()){
            refresh();
        }
    }

    void refresh(){
        pctrl = player.GetComponent<playerController>();
        rigi = player.GetComponent<Rigidbody2D>();
        enemy = gameManager.GetComponent<SpawnEnemy>();
        envi = gameManager.GetComponent<SpawnEnvironment>();
        levelSpawner = gameManager.GetComponent<LevelSpawner>();
        timeScale.text = GameSystem.getTimeScale().ToString();

        mapDropdown.ClearOptions();
        for(int i = 0; i < levelSpawner.stages.Count; i++){
            for(int j = 0; j < levelSpawner.stages[i].levels.Count; j++){
                Dropdown.OptionData optionData = new Dropdown.OptionData();
                optionData.text = (i+1) + " - " + (j+1);
                mapDropdown.options.Add(optionData);
            }
        }
        
        mapDropdown.onValueChanged.AddListener(delegate {onDropdownChanged();});

        enemyP.text = enemy.enemyLevel.spawnPeriod.ToString();
        enviP.text = envi.enviLevel.spawnPeriod.ToString();

        wallAmount.text = enemy.enemyLevel.wallAmount.ToString();

        minSpawn.text = enemy.enemyLevel.minSpawnAmount.ToString();
        maxSpawn.text = enemy.enemyLevel.maxSpawnAmount.ToString();
        
        wallT.isOn = enemy.enemyLevel.spawnWall;
        snakeT.isOn = enemy.enemyLevel.spawnSnake;
        panzeeT.isOn = enemy.enemyLevel.spawnPanzee;
        eagleT.isOn = enemy.enemyLevel.spawnEagle;
        appleT.isOn = enemy.enemyLevel.spawnApple;
        dolphinT.isOn = enemy.enemyLevel.spawnDolphin;
        jellyT.isOn = enemy.enemyLevel.spawnJellyfish;
        planeT.isOn = enemy.enemyLevel.spawnPlane;
        ThunderT.isOn = enemy.enemyLevel.spawnThunder;
        UFOT.isOn = enemy.enemyLevel.spawnUFO;
        satelliteT.isOn = enemy.enemyLevel.spawnSatellite;


        branchT.isOn = envi.enviLevel.spawnBranch;
        bushT.isOn = envi.enviLevel.spawnBush;
    }

    public void onDropdownChanged(){
        int checker = mapDropdown.value;
        int a = 0, b = 0;
        for(a = 0; a < levelSpawner.stages.Count; a++){
            for(b = 0; b < levelSpawner.stages[a].levels.Count; b++){
                if(checker == 0){
                    enemy.enemyLevel = levelSpawner.stages[a].levels[b].enemyLevel;
                    envi.enviLevel = levelSpawner.stages[a].levels[b].enviLevel;

                    timeScale.text = levelSpawner.stages[a].levels[b].timeScale.ToString();

                    enemyP.text = enemy.enemyLevel.spawnPeriod.ToString();
                    enviP.text = envi.enviLevel.spawnPeriod.ToString();

                    wallAmount.text = enemy.enemyLevel.wallAmount.ToString();

                    minSpawn.text = enemy.enemyLevel.minSpawnAmount.ToString();
                    maxSpawn.text = enemy.enemyLevel.maxSpawnAmount.ToString();
                    
                    wallT.isOn = enemy.enemyLevel.spawnWall;
                    snakeT.isOn = enemy.enemyLevel.spawnSnake;
                    panzeeT.isOn = enemy.enemyLevel.spawnPanzee;
                    eagleT.isOn = enemy.enemyLevel.spawnEagle;
                    appleT.isOn = enemy.enemyLevel.spawnApple;
                    dolphinT.isOn = enemy.enemyLevel.spawnDolphin;
                    jellyT.isOn = enemy.enemyLevel.spawnJellyfish;
                    planeT.isOn = enemy.enemyLevel.spawnPlane;
                    ThunderT.isOn = enemy.enemyLevel.spawnThunder;
                    UFOT.isOn = enemy.enemyLevel.spawnUFO;
                    satelliteT.isOn = enemy.enemyLevel.spawnSatellite;


                    branchT.isOn = envi.enviLevel.spawnBranch;
                    bushT.isOn = envi.enviLevel.spawnBush;
                    return;
                }
                checker--;
            }
        }
    }

    void changeConfig(){
        GameSystem.setTimeScale(float.Parse(timeScale.text));
        
        enemy.enemyLevel.spawnPeriod = float.Parse(enemyP.text);
        envi.enviLevel.spawnPeriod = float.Parse(enviP.text);

        enemy.enemyLevel.wallAmount = int.Parse(wallAmount.text);

        enemy.enemyLevel.minSpawnAmount = int.Parse(minSpawn.text);
        enemy.enemyLevel.minSpawnAmount = int.Parse(maxSpawn.text);
        
        enemy.enemyLevel.spawnWall = wallT.isOn;
        enemy.enemyLevel.spawnSnake = snakeT.isOn;
        enemy.enemyLevel.spawnPanzee = panzeeT.isOn;
        enemy.enemyLevel.spawnEagle = eagleT.isOn;
        enemy.enemyLevel.spawnApple = appleT.isOn;
        enemy.enemyLevel.spawnDolphin = dolphinT.isOn;
        enemy.enemyLevel.spawnJellyfish = jellyT.isOn;
        enemy.enemyLevel.spawnPlane = planeT.isOn;
        enemy.enemyLevel.spawnThunder = ThunderT.isOn;
        enemy.enemyLevel.spawnUFO = UFOT.isOn;
        enemy.enemyLevel.spawnSatellite = satelliteT.isOn;

        envi.enviLevel.spawnBranch = branchT.isOn;
        envi.enviLevel.spawnBush = bushT.isOn;
    }
}
