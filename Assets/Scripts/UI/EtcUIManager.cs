using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EtcUIManager : MonoBehaviour
{
    // Start is called before the first frame update
    LobbyUIManager manager;
    public Button onBtn, offBtn;
    public GameObject etcUi;
    public List<GameObject> childUis;

    void Awake(){
        etcUi.SetActive(true);
        for(int i = 0; i < childUis.Count; i++){
            childUis[i].transform.localScale = Vector3.zero;
        }
    }

    void Start(){
        manager = gameObject.GetComponent<LobbyUIManager>();
        GameSystem.isCanVive = SecurityPlayerPrefs.GetInt("Vive", 1) == 1;
        OnViveSettingChanged();
    }
    void Update(){
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
}
