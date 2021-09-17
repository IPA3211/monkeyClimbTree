using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpUIManager : MonoBehaviour
{
    public GameObject popUpUI;
    public GameObject popUpCanvas;
    public PopUpUI compo;
    // Start is called before the first frame update

    void Update(){
    }
    void Start(){
        compo = popUpUI.GetComponent<PopUpUI>();
        popUpUI.SetActive(true);
        compo.background.SetActive(false);
        compo.UI.transform.localScale = Vector3.zero;
    }
    public void setPopUpSelectMsgUI(string describe, System.Action LAction, System.Action RAction, string btnLDescribe = "네", string btnRDescribe = "아니요"){
        compo.PopUpSelectMsgUI(describe, LAction, RAction, btnLDescribe, btnRDescribe);
    }
    public void setPopUpMsgUI(string describe, System.Action Raction, string btnDescribe = "확인"){
        compo.PopUpMsgUI(describe, Raction, btnDescribe);
    }
    public void clear(){
        compo.DestroyThis();
    }
}
