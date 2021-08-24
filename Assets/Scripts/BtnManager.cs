using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnManager : MonoBehaviour
{
    // Start is called before the first frame update
    [Header ("Config")]
    public GameObject player;
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
    public Toggle wallT;
    public Toggle snakeT;
    public Toggle panzeeT;
    public Toggle branchT;
    public Toggle bushT;
    
    [Header ("In Play UI")]
    public Text scoreText;
    public Text healthText;



    playerController pctrl;
    Rigidbody2D rigi;
    SpawnEnemy enemy;
    SpawnEnvironment envi;
    void Start()
    {
        pctrl = player.GetComponent<playerController>();
        rigi = player.GetComponent<Rigidbody2D>();
        enemy = gameObject.GetComponent<SpawnEnemy>();
        envi = gameObject.GetComponent<SpawnEnvironment>();
        
        xPower.text = pctrl.XPower.ToString();
        yPower.text = pctrl.YPower.ToString();
        timeScale.text = Time.timeScale.ToString();
        gravityScale.text = rigi.gravityScale.ToString();
        charScale.text = player.transform.localScale.x.ToString();
        immuneTime.text = pctrl.immuneTime.ToString();
        doubleJumpPower.text = pctrl.doubleJumpPower.ToString();

        enemyP.text = enemy.spawnPeriod.ToString();
        enviP.text = envi.spawnPeriod.ToString();
        
        wallT.isOn = enemy.spawnWall;
        snakeT.isOn = enemy.spawnSnake;
        panzeeT.isOn = enemy.spawnPanzee;
        branchT.isOn = envi.spawnBranch;
        bushT.isOn = envi.spawnBush;
    }

    void FixedUpdate(){
        scoreText.text =  GameSystem.getScore().ToString();
        healthText.text = GameSystem.getHealth().ToString();
    }

    public void ChangePauseSetting(){
        GameSystem.setPause(!GameSystem.getPause());
        pauseMenu.SetActive(GameSystem.getPause());

        if(!GameSystem.getPause()){
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
        
        enemy.spawnWall = wallT.isOn;
        enemy.spawnSnake = snakeT.isOn;
        enemy.spawnPanzee = panzeeT.isOn;
        envi.spawnBranch = branchT.isOn;
        envi.spawnBush = bushT.isOn;
    }
}
