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
    public GameObject gameoverUI1, gameoverUI2, gameManager;
    public Image background;
    public List<Texts> texts;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(GameSystem.isDead){
            
            if(sumTime < 1){
                background.color = Color.Lerp(Color.black * new Color(1,1,1,0), Color.black * new Color(1,1,1,0.5f), sumTime);
            }
            else if (sumTime > texts.Count + 1){
                return;
            }
            else{
                foreach(Text t in texts[(int)(sumTime - 1)].data){
                    t.color = Color.Lerp(Color.white * new Color(1,1,1,0), Color.white * new Color(1,1,1,1f), sumTime - (int)sumTime);
                }
            }

            sumTime += Time.deltaTime;
        }
    } 

    public void startGameoverUI(){
        gameoverUI1.SetActive(true);
        sumTime = 0;
        background.color = Color.clear;
        foreach(Texts t in texts){
            foreach(Text text in t.data){
                text.color = Color.clear;
            }
        }
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

    public void exitBtnClicked(){
        GameSystem.restart();
        SceneManager.LoadScene("SampleScene");
    }
    public void restartBtnClicked(){
        gameoverUI2.SetActive(false);
        GameSystem.restart();
        gameManager.GetComponent<RuntimeGameManager>().gameStart();
    }
}
