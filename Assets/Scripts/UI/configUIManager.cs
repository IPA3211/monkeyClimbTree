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
    public Toggle wallT;
    public Toggle snakeT;
    public Toggle panzeeT;
    public Toggle branchT;
    public Toggle bushT;

    playerController pctrl;
    Rigidbody2D rigi;
    SpawnEnemy enemy;
    SpawnEnvironment envi;
    void Start()
    {
        pctrl = player.GetComponent<playerController>();
        rigi = player.GetComponent<Rigidbody2D>();
        enemy = gameManager.GetComponent<SpawnEnemy>();
        envi = gameManager.GetComponent<SpawnEnvironment>();
        
        xPower.text = pctrl.XPower.ToString();
        yPower.text = pctrl.YPower.ToString();
        timeScale.text = Time.timeScale.ToString();
        gravityScale.text = rigi.gravityScale.ToString();
        charScale.text = player.transform.localScale.x.ToString();
        immuneTime.text = pctrl.immuneTime.ToString();
        doubleJumpPower.text = pctrl.doubleJumpPower.ToString();

        enemyP.text = enemy.spawnPeriod.ToString();
        enviP.text = envi.spawnPeriod.ToString();

        snakeSpeed.text = enemy.snakeSpeed.ToString();
        wallAmount.text = enemy.wallAmount.ToString();
        panzeeX.text = enemy.panzeeXPower.ToString();
        panzeeY.text = enemy.panzeeYPower.ToString();
        
        wallT.isOn = enemy.spawnWall;
        snakeT.isOn = enemy.spawnSnake;
        panzeeT.isOn = enemy.spawnPanzee;
        branchT.isOn = envi.spawnBranch;
        bushT.isOn = envi.spawnBush;
    }

    public void ChangePauseSetting(){
        GameSystem.setPause(!GameSystem.getPause());
        Time.timeScale = 0;
        pauseMenu.SetActive(GameSystem.getPause());

        if(!GameSystem.getPause()){
            Time.timeScale = 1;
            changeConfig();
        }
    }

    void changeConfig(){
        pctrl.XPower = float.Parse(xPower.text);
        pctrl.YPower = float.Parse(yPower.text);
        Time.timeScale = float.Parse(timeScale.text);
        rigi.gravityScale = float.Parse(gravityScale.text);
        player.transform.localScale = new Vector3(float.Parse(charScale.text), float.Parse(charScale.text), 0);
        pctrl.immuneTime = float.Parse(immuneTime.text);
        pctrl.doubleJumpPower = float.Parse(doubleJumpPower.text);
        
        enemy.spawnPeriod = float.Parse(enemyP.text);
        envi.spawnPeriod = float.Parse(enviP.text);

        enemy.snakeSpeed = float.Parse(snakeSpeed.text);
        enemy.wallAmount = int.Parse(wallAmount.text);
        enemy.panzeeXPower = float.Parse(panzeeX.text);
        enemy.panzeeYPower = float.Parse(panzeeY.text);
        
        enemy.spawnWall = wallT.isOn;
        enemy.spawnSnake = snakeT.isOn;
        enemy.spawnPanzee = panzeeT.isOn;
        envi.spawnBranch = branchT.isOn;
        envi.spawnBush = bushT.isOn;
    }
}
