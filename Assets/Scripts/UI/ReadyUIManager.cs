using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadyUIManager : MonoBehaviour
{
    public GameObject readyUI;
    public Text text;
    // Start is called before the first frame update
    public void countDown(int mode){
        StartCoroutine("countDownCoroutine", mode);
    }

    IEnumerator countDownCoroutine(int mode) {
        readyUI.SetActive(true);
        text.text = "READY...";

        while(GameSystem.isRestarted){
            yield return new WaitForFixedUpdate();
        }
        
        yield return new WaitForSecondsRealtime(2f);
        text.text = "START!";
        yield return new WaitForSecondsRealtime(1f);
        readyUI.SetActive(false);

        if(mode == 0)
            GameSystem.isStarted = true;
        else if(mode == 1){
            GameSystem.setPause(false);
        }
    }
}
