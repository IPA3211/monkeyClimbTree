using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public enum rare
{
    Normal, 
    Rare, 
    Hard
}

[System.Serializable]
public class Skin{
    [HideInInspector]
    public int index;
    public Texture2D texture;
    public string skinName;
    public rare rareNum;
    public bool isUnlocked;

    Skin(Texture2D t, string n){
        texture = t;
        skinName = n;
        isUnlocked = false;
    }
}

public class ObjReskin : MonoBehaviour
{
    public GameObject target;
    SpriteRenderer sprend; 
    public List<Skin> skinDatas;
    public int skinNumber = 0;
    public Sprite lockSprite;
    Sprite[] sprites;
    // Start is called before the first frame update
    void Start()
    {
        LoadSkinData();
        sprend = target.GetComponent<SpriteRenderer>();
        skinNumber = SecurityPlayerPrefs.GetInt("skinNum", 0);
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
        sprites = Resources.LoadAll<Sprite>("Sprites/Skin/" + skinDatas[skinNumber].texture.name);
    }

    public (Sprite, string) setUISkin(int num){
        if(skinDatas[num].isUnlocked){
            setSkin(num);
            return (sprites[0], skinDatas[skinNumber].skinName);
        }
        else{
            return (lockSprite, "???");
        }
    }

    public float getPercent(){
        int sum = 0;
        for(int i = 0; i < getMaxSize(); i++){
            if(skinDatas[i].isUnlocked){
                sum++;
            }
        }

        return (float)sum / (float)getMaxSize() * 100;
    }

    public int getMaxSize(){
        return skinDatas.Count;
    }

    public void SaveSkinData(){
        bool[] bools = new bool[skinDatas.Count];
        for(int i = 0; i < skinDatas.Count; i++){
            bools[i] = skinDatas[i].isUnlocked;
        }
        BitArray ba3 = new BitArray(bools);
        SecurityPlayerPrefs.SetString("SkinData", ToBitString(ba3));
    }

    public void LoadSkinData(){
        string skinDataString;
        skinDataString = SecurityPlayerPrefs.GetString("SkinData", "1");
        int i;
        for(i = 0; i < skinDataString.Length; i++){
            if(i >= skinDatas.Count){
                return;
            }
            if(skinDataString[i] == '1'){
                skinDatas[i].isUnlocked = true;
            }
            else{
                skinDatas[i].isUnlocked = false;
            }
            skinDatas[i].index = i;
        }

        for(; i < skinDatas.Count; i++){
            skinDatas[i].isUnlocked = false;
            skinDatas[i].index = i;
        }
    }

    public string ToBitString(BitArray bits)
    {
        var sb = new StringBuilder();

        for (int i = 0; i < bits.Count; i++)
        {
            char c = bits[i] ? '1' : '0';
            sb.Append(c);
        }

        return sb.ToString();
    }
}
