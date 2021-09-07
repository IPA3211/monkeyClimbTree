using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadyUIManager : MonoBehaviour
{
    public GameObject readyUI;
    public Image bg;
    public Text text;
    // Start is called before the first frame update
    public void countDown(int mode){
        StartCoroutine("countDownCoroutine", mode);
    }

    public void countDownStageCleared()
    {
        StartCoroutine("StageCleared");
    }

    IEnumerator countDownCoroutine(int mode) {
        readyUI.SetActive(true);
        text.text = "???????...";

        while(GameSystem.isRestarted){
            yield return new WaitForFixedUpdate();
        }

        if (!GameSystem.isRestarted)
        {
            text.text = "???...";
            yield return new WaitForSecondsRealtime(2f);
        }

        text.text = "????!";
        yield return new WaitForSecondsRealtime(1f);
        readyUI.SetActive(false);

        if(mode == 0)
            GameSystem.isStarted = true;            
        else if(mode == 1){
            GameSystem.setPause(false);
        }
    }

    IEnumerator countDownEndingCoroutine()
    {
        readyUI.SetActive(true);
        text.text = "?? ???? ??...";

        while (GameSystem.isRestarted)
        {
            yield return new WaitForFixedUpdate();
        }

        if (!GameSystem.isRestarted)
        {
            text.text = "???...";
            yield return new WaitForSecondsRealtime(2f);
        }

        text.text = "????!";
        yield return new WaitForSecondsRealtime(1f);
        readyUI.SetActive(false);

        GameSystem.isStarted = true;
    }
    IEnumerator StageCleared()
    {
        readyUI.SetActive(true);
        text.text = "";
        
        float progress = 0;
        while(progress < 1){
            bg.color = Color.Lerp(new Color(0, 0, 0, 0.5f), Color.black, progress);
            progress += Time.unscaledDeltaTime * 2;
            yield return new WaitForFixedUpdate();
        }
        GameSystem.stageUp();
        GameSystem.nextStageStart();
        StartCoroutine("countDownEndingCoroutine");
        
        while (GameSystem.isRestarted)
            yield return new WaitForFixedUpdate();
        
        progress = 0;
        while(progress < 1){
            bg.color = Color.Lerp(Color.black, new Color(0, 0, 0, 0.5f), progress);
            progress += Time.unscaledDeltaTime * 2;
            yield return new WaitForFixedUpdate();
        }
    }
}
