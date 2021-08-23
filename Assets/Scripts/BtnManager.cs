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
    public InputField camOffset;
    public InputField camSpeed;
    public InputField timeScale;
    public InputField gravityScale;
    public InputField charScale;
    
    [Header ("In Play UI")]
    public Text scoreText;
    public Text healthText;



    playerController pctrl;
    Rigidbody2D rigi;
    SmoothCamera smoothCamera;
    void Start()
    {
        pctrl = player.GetComponent<playerController>();
        rigi = player.GetComponent<Rigidbody2D>();
        smoothCamera = gameObject.GetComponent<SmoothCamera>();
        
        xPower.text = pctrl.XPower.ToString();
        yPower.text = pctrl.YPower.ToString();
        camOffset.text = smoothCamera.camOffset.ToString();
        camSpeed.text = smoothCamera.camSpeed.ToString();
        timeScale.text = Time.timeScale.ToString();
        gravityScale.text = rigi.gravityScale.ToString();
        charScale.text = player.transform.localScale.x.ToString();
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
        smoothCamera.camOffset = float.Parse(camOffset.text);
        smoothCamera.camSpeed = float.Parse(camSpeed.text);
        Time.timeScale = float.Parse(timeScale.text);
        rigi.gravityScale = float.Parse(gravityScale.text);
        player.transform.localScale = new Vector3(float.Parse(charScale.text), float.Parse(charScale.text), 0);
    }
}
