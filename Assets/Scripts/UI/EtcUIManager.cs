using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EtcUIManager : MonoBehaviour
{
    // Start is called before the first frame update
    LobbyUIManager manager;
    public Button onBtn, offBtn;

    void Start(){
        manager = gameObject.GetComponent<LobbyUIManager>();
        GameSystem.isCanVive = SecurityPlayerPrefs.GetInt("Vive", 1) == 1;
        OnViveSettingChanged();
        
    }
    void Update(){
        if(GameSystem.isStarted){
            manager.settingUI.SetActive(false);
            manager.endingUI.SetActive(false);
            manager.skinUI.SetActive(false);
            manager.rankingUI.SetActive(false);
        }
    }
    public void OnConfirmBtnClicked(){
        manager.settingUI.SetActive(false);
        manager.endingUI.SetActive(false);
        manager.skinUI.SetActive(false);
        manager.rankingUI.SetActive(false);
    }

    public void OnViveOnBtnClicked(){
        GameSystem.isCanVive = true;
        OnViveSettingChanged();
    }
    public void OnViveOffBtnClicked(){
        GameSystem.isCanVive = false;
        OnViveSettingChanged();
    }
    private void OnViveSettingChanged(){
        if(GameSystem.isCanVive){
            onBtn.interactable = false;
            offBtn.interactable = true;
        }
        else{
            onBtn.interactable = true;
            offBtn.interactable = false;
        }

        SecurityPlayerPrefs.SetInt("Vive", GameSystem.isCanVive ? 1 : 0);
    }

    public void testBtn(){
        if(GameSystem.isCanVive)
            RDG.Vibration.Vibrate(1000);

        Debug.Log(RDG.Vibration.GetApiLevel());
    }

    public void ttbtn(){
        Handheld.Vibrate();
    }
}
