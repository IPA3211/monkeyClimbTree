using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageClearUI : MonoBehaviour
{
    float sumTime;
    public GameObject StageClearUI1, StageClearUI2, gameManager;
    public Button nextStageBtn;
    public Image background;
    public Image coinSprite;
    public List<Texts> texts;
    public Text score;
    public Text coinEarned;

    // Start is called before the first frame update
    void Start()
    {
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
