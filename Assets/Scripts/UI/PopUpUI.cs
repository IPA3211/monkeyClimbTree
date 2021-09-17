using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PopUpUI : MonoBehaviour
{
    System.Action _RAction, _LAction;
    public GameObject UI, background;
    public Text describeText;
    public Button btnR, btnL;
    // Start is called before the first frame update
    public void DestroyThis(){
        background.SetActive(false);
        UI.transform.LeanScale(Vector3.zero, 0.5f).setEaseInBack();
    }
    public void PopUpMsgUI(string describe, System.Action RAction, string btnDescribe = "확인"){
        background.SetActive(true);
        _RAction = RAction;
        _LAction = RAction;
        btnR.gameObject.GetComponentInChildren<Text>().text = btnDescribe;
        UI.transform.LeanScale(Vector3.one, 0.5f).setEaseOutQuad();

        describeText.text = describe;
        btnL.gameObject.SetActive(false);
    }

    public void PopUpSelectMsgUI(string describe, System.Action LAction, System.Action RAction, string btnLDescribe = "네", string btnRDescribe = "아니요"){
        background.SetActive(true);
        _RAction = RAction;
        _LAction = LAction;
        btnR.gameObject.GetComponentInChildren<Text>().text = btnRDescribe;
        btnL.gameObject.GetComponentInChildren<Text>().text = btnLDescribe;

        background.SetActive(true);
        UI.transform.LeanScale(Vector3.one, 0.5f).setEaseOutQuad();

        describeText.text = describe;
    }
    public void onRBtnClicked(){
        _RAction();
    }
    public void onLBtnClicked(){
        _LAction();
    }

}
