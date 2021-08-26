using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjReskin : MonoBehaviour
{
    public GameObject target;
    SpriteRenderer sprend; 
    public List<Texture2D> skinDatas;
    public int skinNumber = 0;
    Sprite[] sprites;
    // Start is called before the first frame update
    void Start()
    {
        sprend = target.GetComponent<SpriteRenderer>();
        skinNumber = SecurityPlayerPrefs.GetInt("skinNum", 1);
        setSkin(skinNumber);
    }
    void LateUpdate()
    {
        string[] temp = sprend.sprite.name.Split('_');
        
        sprend.sprite = sprites[int.Parse(temp[temp.Length - 1])];
    }
    void setSkin(int num){
        skinNumber = num;
        SecurityPlayerPrefs.SetInt("skinNum", skinNumber);
        sprites = Resources.LoadAll<Sprite>("Sprites/" + skinDatas[skinNumber].name);
    }

    public void setUISkin(){
        if(target.tag == "Player"){
            GameObject.FindWithTag("UI_Player").GetComponent<Image>().sprite = sprites[0];
        }
    }
    public void nextSkin(){
        skinNumber++;
        if(skinNumber > skinDatas.Count - 1){
            skinNumber = 0;
        }
        setSkin(skinNumber);
        setUISkin();
    }
    public void prevSkin(){
        skinNumber--;
        if(skinNumber < 0){
            skinNumber = skinDatas.Count - 1;
        }
        setSkin(skinNumber);
        setUISkin();
    }
}
