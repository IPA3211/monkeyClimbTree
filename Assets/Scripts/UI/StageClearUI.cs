using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageClearUI : MonoBehaviour
{
    float sumTime;
    EndingManager endingManager;
    public GameObject StageClearUI1, StageClearUI2, gameManager;
    public Button nextStageBtn;
    public Image background;
    public Image coinSprite;
    public Image endingSprite;
    public List<Texts> texts;
    public Text score;
    public Text coinEarned;
    public Text endingTitle;
    public Text endingDescription;
    Ending curEnding;

    // Start is called before the first frame update
    void Start()
    {
        endingManager = gameManager.GetComponent<EndingManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameSystem.isStageCleared)
        {
            if(GameSystem.getStage() == 4)
            {
                nextStageBtn.gameObject.SetActive(false);
            }
            GameSystem.deadSign = GameSystem.getStage().ToString() + "StageClear";
            Debug.Log("ddd " + GameSystem.deadSign);

            if (sumTime < 1)
            {
                background.color = Color.Lerp(Color.black * new Color(1, 1, 1, 0), Color.black * new Color(1, 1, 1, 0.5f), sumTime);
                coinSprite.color = Color.white * new Color(1, 1, 1, 0);
            }
            else if (sumTime > texts.Count + 1)
            {
                return;
            }
            else
            {
                foreach (Text t in texts[(int)(sumTime - 1)].data)
                {
                    t.color = Color.Lerp(Color.white * new Color(1, 1, 1, 0), Color.white * new Color(1, 1, 1, 1f), sumTime - (int)sumTime);
                    if((int)(sumTime - 1) == 2)
                    {
                        coinSprite.color = Color.Lerp(Color.white * new Color(1, 1, 1, 0), Color.white * new Color(1, 1, 1, 1f), sumTime - (int)sumTime);
                    }
                }
            }

            sumTime += Time.unscaledDeltaTime;
        }
    }

    public void startStageClearUI()
    {
        setEndingUI();
        StageClearUI1.SetActive(true);
        sumTime = 0;
        background.color = Color.clear;
        score.text = GameSystem.getScore().ToString();
        coinEarned.text = GameSystem.getCoinEarned().ToString();
        foreach (Texts t in texts)
        {
            foreach (Text text in t.data)
            {
                text.color = Color.clear;
            }
        }
    }
    public void setEndingUI()
    {
        GameSystem.deadSign = GameSystem.getStage().ToString() + "StageClear";
        curEnding = endingManager.GetEnding();

        if (curEnding.thumbnails.Length == 1)
            endingSprite.sprite = curEnding.thumbnails[0];
        else
        {
            endingSprite.sprite = curEnding.thumbnails[0];
        }
        endingTitle.text = curEnding.endingName;
        endingDescription.text = curEnding.description;
    }

    public void btnClicked()
    {
        if (sumTime < texts.Count + 1)
        {
            sumTime = (int)sumTime + 0.99f;
        }
        else
        {
            StageClearUI1.SetActive(false);
            StageClearUI2.SetActive(true);
        }
    }

    public void exitBtnClicked()
    {
        GameSystem.restart();
        SceneManager.LoadScene("SampleScene");
    }
    public void nextStageBtnClicked()
    {
        StageClearUI2.SetActive(false);
        //GameSystem.restart();
        gameManager.GetComponent<RuntimeGameManager>().upStage();
    }

    public void endingBtnClicked()
    {
        StageClearUI2.SetActive(false);
    }
}
