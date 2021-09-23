using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EtcUIManager : MonoBehaviour
{
    // Start is called before the first frame update
    LobbyUIManager manager;
    public Button onBtn, offBtn;
    public Button hOnBtn, hOffBtn, rightBtn, leftBtn, bothBtn;
    public Text versionText;
    public GameObject etcUi;
    public List<GameObject> childUis;
    public GameObject heartImage;

    void Awake(){
        etcUi.SetActive(true);
        for(int i = 0; i < childUis.Count; i++){
            childUis[i].transform.localScale = Vector3.zero;
        }
    }

    void Start(){
        manager = gameObject.GetComponent<LobbyUIManager>();
        GameSystem.isCanVive = SecurityPlayerPrefs.GetInt("Vive", 1) == 1;
        GameSystem.whichHand = (handPos)SecurityPlayerPrefs.GetInt("Hand", 0);
        versionText.text = "Ver. " + Application.version;
        OnHandSettingChanged();
        OnViveSettingChanged();
    }
    void Update(){
    }

    private void FixedUpdate()
    {
        if (GameSystem.hasHeartDoublePlus)
        {
            heartImage.SetActive(true);
        }            
        else
        {
            heartImage.SetActive(false);
        }
            
    }

    public void HeartDoublePlusBtn()
    {
        GameSystem.hasHeartDoublePlus = !GameSystem.hasHeartDoublePlus;
        if (GameSystem.hasHeartDoublePlus)
        {
            GameSystem.setHealth(4);
        }
        else
        {
            GameSystem.setHealth(3);
        }
    }

    public void OnViveOnBtnClicked(){
        GameSystem.isCanVive = true;
        OnViveSettingChanged();
    }
    public void OnViveOffBtnClicked(){
        GameSystem.isCanVive = false;
        OnViveSettingChanged();
    }
    public void OnHandOnBtnClicked(){
        GameSystem.whichHand = handPos.PLAYER_HAND_RIGHT;
        OnHandSettingChanged();
    }
    public void OnHandOffBtnClicked(){
        GameSystem.whichHand = handPos.PLAYER_HAND_NULL;
        OnHandSettingChanged();
    }
    public void OnHandRightBtnClicked(){
        GameSystem.whichHand = handPos.PLAYER_HAND_RIGHT;
        OnHandSettingChanged();
    }
    public void OnHandLeftBtnClicked(){
        GameSystem.whichHand = handPos.PLAYER_HAND_LEFT;
        OnHandSettingChanged();
    }
    public void OnHandBothBtnClicked(){
        GameSystem.whichHand = handPos.PLAYER_HAND_BOTH;
        OnHandSettingChanged();
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

    private void OnHandSettingChanged(){
        hOnBtn.interactable = true;
        hOffBtn.interactable = true;
        rightBtn.interactable = true;
        leftBtn.interactable = true;
        bothBtn.interactable = true;

        switch(GameSystem.whichHand){
            case handPos.PLAYER_HAND_NULL :
                hOffBtn.interactable = false;
                rightBtn.interactable = false;
                leftBtn.interactable = false;
                bothBtn.interactable = false;
            break;
            case handPos.PLAYER_HAND_RIGHT : 
                hOnBtn.interactable = false;
                rightBtn.interactable = false;
            break;
            case handPos.PLAYER_HAND_LEFT : 
                hOnBtn.interactable = false;
                leftBtn.interactable = false;
            break;
            case handPos.PLAYER_HAND_BOTH : 
                hOnBtn.interactable = false;
                bothBtn.interactable = false;
            break;
        }

        SecurityPlayerPrefs.SetInt("Hand", (int)GameSystem.whichHand);
    }
}
