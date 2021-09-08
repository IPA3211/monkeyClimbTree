using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Texts{
    public Text[] data;
}

public class GameoverUI : MonoBehaviour
{
    float sumTime;
    EndingManager endingManager;
    public GameObject gameoverUI1, gameoverUI2, gameManager;
    public Image background;
    public Image coinSprite;
    public Image endingSprite;
    public List<Texts> texts;
    public Text score;
    public Text coinEarned;
    public Text endingTitle;
    public Text endingDescription;
    public GameObject endingEffect;
    public Toggle magnetTog, boosterTog, shieldTog, earnTog;
    public int magnetCoin, boosterCoin, shieldCoin;
    bool isUnlockedEffect = false;
    bool isUsingToggle = true;
    public int usedCoin = 0;
    Ending curEnding;

    // Start is called before the first frame update
    void Start()
    {
        endingManager = gameManager.GetComponent<EndingManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameSystem.isDead){
            
            if(sumTime < 1){
                background.color = Color.Lerp(Color.black * new Color(1,1,1,0), Color.black * new Color(1,1,1,0.5f), sumTime);
                coinSprite.color = Color.white * new Color(1, 1, 1, 0);
            }
            else if (sumTime > texts.Count + 1){
                return;
            }
            else{
                foreach(Text t in texts[(int)(sumTime - 1)].data){
                    t.color = Color.Lerp(Color.white * new Color(1,1,1,0), Color.white * new Color(1,1,1,1f), sumTime - (int)sumTime);
                    if ((int)(sumTime - 1) == 2)
                    {
                        coinSprite.color = Color.Lerp(Color.white * new Color(1, 1, 1, 0), Color.white * new Color(1, 1, 1, 1f), sumTime - (int)sumTime);
                    }
                }
            }

            sumTime += Time.unscaledDeltaTime;

            endingEffect.SetActive(isUnlockedEffect);
        }

    }

    public void startGameoverUI(){
        setEndingUI();

        gameoverUI1.SetActive(true);
        sumTime = 0;
        background.color = Color.clear;
        score.text = GameSystem.getScore().ToString();
        coinEarned.text = GameSystem.getCoinEarned().ToString();
        foreach (Texts t in texts){
            foreach(Text text in t.data){
                text.color = Color.clear;
            }
        }
    }

    public void setEndingUI()
    {
        isUnlockedEffect = endingManager.UnlockEnding(GameSystem.deadSign);
        curEnding = endingManager.GetEnding();

        if (curEnding.thumbnails.Length == 1)
            endingSprite.sprite = curEnding.thumbnails[0];
        else
        {
            // 추후에 엔딩짤이 여러개일 경우
            endingSprite.sprite = curEnding.thumbnails[0];
        }
        endingTitle.text = curEnding.endingName;
        endingDescription.text = curEnding.description;
    }

    public void btnClicked(){
        if(sumTime < texts.Count + 1){
            sumTime = (int)sumTime + 0.99f;
        }
        else{
            gameoverUI1.SetActive(false);
            gameoverUI2.SetActive(true);
        }
    }

    public void magnetToggled()
    {
        if (GameSystem.getCoin() < magnetCoin)
            magnetTog.interactable = false;
        else
            magnetTog.interactable = true;

        if (isUsingToggle)
        {
            if (magnetTog.isOn && GameSystem.getCoin() >= magnetCoin)
            {
                usedCoin += magnetCoin;
                GameSystem.addCoin(-magnetCoin);
                GameSystem.hasMagnetic = true;                
            }
            else if (!magnetTog.isOn)
            {
                usedCoin -= magnetCoin;
                GameSystem.addCoin(magnetCoin);
                GameSystem.hasMagnetic = false;
            }
        }        
    }

    public void boosterToggled()
    {
        if (GameSystem.getCoin() < boosterCoin)
            boosterTog.interactable = false;
        else
            boosterTog.interactable = true;

        if (isUsingToggle)
        {
            if (boosterTog.isOn && GameSystem.getCoin() >= boosterCoin)
            {
                usedCoin += boosterCoin;
                GameSystem.addCoin(-boosterCoin);
                GameSystem.hasBooster = true;                
            }
            else if (!boosterTog.isOn)
            {
                usedCoin -= boosterCoin;
                GameSystem.addCoin(boosterCoin);
                GameSystem.hasBooster = false;
                Debug.Log("canceled!");
            }
        }        
    }

    public void shieldToggled()
    {
        if (GameSystem.getCoin() < shieldCoin)
            shieldTog.interactable = false;
        else
            shieldTog.interactable = true;

        if (isUsingToggle)
        {
            if (shieldTog.isOn && GameSystem.getCoin() >= shieldCoin)
            {
                usedCoin += shieldCoin;
                GameSystem.addCoin(-shieldCoin);
                GameSystem.hasShield = true;                
            }
            else if (!shieldTog.isOn)
            {
                usedCoin -= shieldCoin;
                GameSystem.addCoin(shieldCoin);
                GameSystem.hasShield = false;
            }
        }        
    }

    public void earnCoin()
    {
        if(earnTog.isOn)
            earnTog.isOn = false;
        GameSystem.addCoin(5);
    }

    public void exitBtnClicked(){
        isUsingToggle = false;
        magnetTog.isOn = false;
        boosterTog.isOn = false;
        shieldTog.isOn = false;
        isUsingToggle = true;
        GameSystem.addCoin(usedCoin);
        usedCoin = 0;
        GameSystem.resetStartItem();
        GameSystem.restart();
        SecurityPlayerPrefs.SetInt("Coin", GameSystem.getCoin());
        SceneManager.LoadScene("SampleScene");
    }
    public void restartBtnClicked(){
        gameoverUI2.SetActive(false);
        isUsingToggle = false;
        magnetTog.isOn = false;
        boosterTog.isOn = false;
        shieldTog.isOn = false;
        isUsingToggle = true;
        usedCoin = 0;

        GameSystem.restart();
        SecurityPlayerPrefs.SetInt("Coin", GameSystem.getCoin());
        gameManager.GetComponent<RuntimeGameManager>().gameStart();
    }
}
