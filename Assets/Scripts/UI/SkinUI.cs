using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class SkinUI : MonoBehaviour
{
    public GameObject gameManager;
    public Image UIPlayer;
    public Text UIName;
    public Text skinNumText;
    public Text persentText;
    public int skinNum;
    
    List<Skin> sortedSkins;

    ObjReskin skinManager;
    
    // Start is called before the first frame update
    void Start()
    {
        skinManager = gameManager.GetComponent<ObjReskin>();
        sortedSkins = skinManager.skinDatas.ToList();
        sortedSkins.Sort((a, b) => {
            if(a.rareNum < b.rareNum){
                return -1;
            }
            else if(a.rareNum > b.rareNum){
                return 1;
            }
            else
                return a.index.CompareTo(b.index);
        });

        for(int i = 0; i < sortedSkins.Count; i++){
            if(sortedSkins[i].index == skinManager.skinNumber){
                skinNum = i;
                break;
            }
        }
        changeUI(skinManager.setUISkin(sortedSkins[skinNum].index));
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
        changeUI(skinManager.setUISkin(sortedSkins[skinNum].index));
    }
    
    public void OnPrevBtnClicked(){
        skinNum--;
        if(skinNum == -1){
            skinNum = skinManager.getMaxSize() - 1;
        }
        changeUI(skinManager.setUISkin(sortedSkins[skinNum].index));
    }

    public void refreshSkinUI(){
        changeUI(skinManager.setUISkin(sortedSkins[skinNum].index));
    }

    void changeUI((Sprite skin, string text) skin){
        UIPlayer.sprite = skin.skin;
        UIName.text = skin.text;
        if (sortedSkins[skinNum].rareNum == rare.Normal)
        {
            UIName.color = Color.gray;
        }
        else if (sortedSkins[skinNum].rareNum == rare.Rare)
        {
            UIName.color = new Color32(0, 186, 155, 255);
        }
        else if (sortedSkins[skinNum].rareNum == rare.Hard)
        {
            UIName.color = new Color32(255, 0, 108, 255);
        }
        skinNumText.text = (skinNum + 1) + " / " + skinManager.getMaxSize();
        persentText.text = string.Format("{0:0#}", skinManager.getPercent()) + "%";
    }
}
