using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadyUIManager : MonoBehaviour
{
    public GameObject readyUI;
    public Text text;
    // Start is called before the first frame update
    public void countDown(){
        StartCoroutine("countDownCoroutine");
    }

    IEnumerator countDownCoroutine() {
        readyUI.SetActive(true);
        text.text = "READY...";

        while(GameSystem.isRestarted){
            yield return new WaitForFixedUpdate();
        }
        
        yield return new WaitForSecondsRealtime(2f);
        text.text = "START...";
        yield return new WaitForSecondsRealtime(1f);
        readyUI.SetActive(false);
        GameSystem.isStarted = true;
    }
}
