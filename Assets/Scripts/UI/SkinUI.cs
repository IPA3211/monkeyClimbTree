using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinUI : MonoBehaviour
{
    public GameObject gameManager;
    public Image UIPlayer;
    public Text UIName;
    public Text skinNumText;
    public Text persentText;
    public int skinNum;

    ObjReskin skinManager;
    
    // Start is called before the first frame update
    void Start()
    {
        skinManager = gameManager.GetComponent<ObjReskin>();
        skinNum = skinManager.skinNumber;
        changeUI(skinManager.setUISkin(skinNum));
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnNextBtnClicked(){
        skinNum++;
        if(skinNum == skinManager.getMaxSize()){
            skinNum = 0;
        }
        changeUI(skinManager.setUISkin(skinNum));
    }
    
    public void OnPrevBtnClicked(){
        skinNum--;
        if(skinNum == -1){
            skinNum = skinManager.getMaxSize() - 1;
        }
        changeUI(skinManager.setUISkin(skinNum));
    }

    public void refreshSkinUI(){
        changeUI(skinManager.setUISkin(skinNum));
    }

    void changeUI((Sprite skin, string text) skin){
        UIPlayer.sprite = skin.skin;
        UIName.text = skin.text;
        if (skinManager.skinDatas[skinNum].rareNum == rare.Normal)
        {
            UIName.color = Color.gray;
        }
        else if (skinManager.skinDatas[skinNum].rareNum == rare.Rare)
        {
            UIName.color = new Color32(0, 186, 155, 255);
        }
        else if (skinManager.skinDatas[skinNum].rareNum == rare.Hard)
        {
            UIName.color = new Color32(255, 0, 108, 255);
        }
        skinNumText.text = (skinNum + 1) + " / " + skinManager.getMaxSize();
        persentText.text = string.Format("{0:0#}", skinManager.getPercent()) + "%";
    }
}
