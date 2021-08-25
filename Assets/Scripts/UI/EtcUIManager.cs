using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EtcUIManager : MonoBehaviour
{
    // Start is called before the first frame update
    LobbyUIManager manager;

    void Start(){
        manager = gameObject.GetComponent<LobbyUIManager>();
    }
    void Update(){
        if(GameSystem.isStarted){
            manager.etcUIBackground.SetActive(false);
            manager.settingUI.SetActive(false);
            manager.endingUI.SetActive(false);
            manager.skinUI.SetActive(false);
            manager.rankingUI.SetActive(false);
        }
    }
    public void OnHomeBtnClicked(){
        manager.etcUIBackground.SetActive(false);
        manager.settingUI.SetActive(false);
        manager.endingUI.SetActive(false);
        manager.skinUI.SetActive(false);
        manager.rankingUI.SetActive(false);
    }
}
