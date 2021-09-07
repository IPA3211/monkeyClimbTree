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
    public Toggle magnetTog, boosterTog, shieldTog;
    public int magnetCoin, boosterCoin, shieldCoin;
    bool isUnlockedEffect = false;
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
        if(magnetTog.isOn)
        {
            GameSystem.hasMagnetic = true;
        }
        else
        {
            GameSystem.hasMagnetic = false;
        }
    }

    public void boosterToggled()
    {
        if (boosterTog.isOn)
        {
            GameSystem.hasBooster = true;
        }
        else
        {
            GameSystem.hasBooster = false;
        }
    }

    public void shieldToggled()
    {
        if (shieldTog.isOn)
        {
            GameSystem.hasShield = true;
        }
        else
        {
            GameSystem.hasShield = false;
        }
    }

    public void exitBtnClicked(){
        magnetTog.isOn = false;
        boosterTog.isOn = false;
        shieldTog.isOn = false;
        GameSystem.resetStartItem();
        GameSystem.restart();
        SceneManager.LoadScene("SampleScene");
    }
    public void restartBtnClicked(){
        gameoverUI2.SetActive(false);

        if(GameSystem.hasMagnetic)
        {
            GameSystem.addCoin(-magnetCoin);
        }
        if(GameSystem.hasBooster)
        {
            GameSystem.addCoin(-boosterCoin);
        }
        if(GameSystem.hasShield)
        {
            GameSystem.addCoin(-shieldCoin);
        }

        GameSystem.restart();
        gameManager.GetComponent<RuntimeGameManager>().gameStart();
    }
}
