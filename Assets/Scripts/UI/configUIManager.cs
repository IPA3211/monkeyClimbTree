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
    public InputField xPower;
    public InputField yPower;
    public InputField timeScale;
    public InputField gravityScale;
    public InputField charScale;
    public InputField immuneTime;
    public InputField doubleJumpPower;

        [Space (10f)]
    public InputField enemyP;
    public InputField enviP;
    public InputField snakeSpeed;
    public InputField wallAmount;
    public InputField panzeeX;
    public InputField panzeeY;
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

    playerController pctrl;
    Rigidbody2D rigi;
    SpawnEnemy enemy;
    SpawnEnvironment envi;

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
        
        xPower.text = pctrl.XPower.ToString();
        yPower.text = pctrl.YPower.ToString();
        timeScale.text = GameSystem.getTimeScale().ToString();
        gravityScale.text = rigi.gravityScale.ToString();
        charScale.text = player.transform.localScale.x.ToString();
        immuneTime.text = pctrl.immuneTime.ToString();
        doubleJumpPower.text = pctrl.doubleJumpPower.ToString();

        enemyP.text = enemy.enemyLevel.spawnPeriod.ToString();
        enviP.text = envi.enviLevel.spawnPeriod.ToString();

        snakeSpeed.text = enemy.enemyLevel.snakeSpeed.ToString();
        wallAmount.text = enemy.enemyLevel.wallAmount.ToString();
        panzeeX.text = enemy.enemyLevel.panzeeXPower.ToString();
        panzeeY.text = enemy.enemyLevel.panzeeYPower.ToString();

        minSpawn.text = enemy.enemyLevel.minSpawnAmount.ToString();
        maxSpawn.text = enemy.enemyLevel.maxSpawnAmount.ToString();
        
        wallT.isOn = enemy.enemyLevel.spawnWall;
        snakeT.isOn = enemy.enemyLevel.spawnSnake;
        panzeeT.isOn = enemy.enemyLevel.spawnPanzee;
        eagleT.isOn = enemy.enemyLevel.spawnEagle;
        appleT.isOn = enemy.enemyLevel.spawnApple;
        dolphinT.isOn = enemy.enemyLevel.spawnDolphin;
        branchT.isOn = envi.enviLevel.spawnBranch;
        bushT.isOn = envi.enviLevel.spawnBush;
    }

    void changeConfig(){
        pctrl.XPower = float.Parse(xPower.text);
        pctrl.YPower = float.Parse(yPower.text);
        GameSystem.setTimeScale(float.Parse(timeScale.text));
        rigi.gravityScale = float.Parse(gravityScale.text);
        player.transform.localScale = new Vector3(float.Parse(charScale.text), float.Parse(charScale.text), 0);
        pctrl.immuneTime = float.Parse(immuneTime.text);
        pctrl.doubleJumpPower = float.Parse(doubleJumpPower.text);
        
        enemy.enemyLevel.spawnPeriod = float.Parse(enemyP.text);
        envi.enviLevel.spawnPeriod = float.Parse(enviP.text);

        enemy.enemyLevel.snakeSpeed = float.Parse(snakeSpeed.text);
        enemy.enemyLevel.wallAmount = int.Parse(wallAmount.text);
        enemy.enemyLevel.panzeeXPower = float.Parse(panzeeX.text);
        enemy.enemyLevel.panzeeYPower = float.Parse(panzeeY.text);
        
        enemy.enemyLevel.spawnWall = wallT.isOn;
        enemy.enemyLevel.spawnSnake = snakeT.isOn;
        enemy.enemyLevel.spawnPanzee = panzeeT.isOn;
        enemy.enemyLevel.spawnEagle = eagleT.isOn;
        enemy.enemyLevel.spawnApple = appleT.isOn;
        enemy.enemyLevel.spawnDolphin = dolphinT.isOn;
        envi.enviLevel.spawnBranch = branchT.isOn;
        envi.enviLevel.spawnBush = bushT.isOn;
    }
}
