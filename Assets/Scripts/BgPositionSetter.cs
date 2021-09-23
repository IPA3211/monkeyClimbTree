using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgPositionSetter : MonoBehaviour
{


    // Start is called before the first frame update
    public GameObject firstBg, endBg;
    public GameObject middleBg1, middleBg2;
    public GameObject cam;
    public Sprite nextSprite;
    public float bgOffset;
    bool isEnd = false;
    SmoothCamera smoothCamera;
    void Start()
    {
        smoothCamera = GetComponent<SmoothCamera>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(GameSystem.isRestarted){
            Reset();
        }

        if(cam.transform.position.y > middleBg1.transform.position.y + bgOffset){
            middleBg1.transform.position = middleBg2.transform.position + new Vector3(0, bgOffset, 0);
            changeBg(middleBg1, true);
        }
        else if (cam.transform.position.y > middleBg2.transform.position.y + bgOffset){
            middleBg2.transform.position = middleBg1.transform.position + new Vector3(0, bgOffset, 0);
            changeBg(middleBg2, true);
        }
        
        if(cam.transform.position.y < middleBg1.transform.position.y - bgOffset){
            middleBg1.transform.position = middleBg2.transform.position - new Vector3(0, bgOffset, 0);
            changeBg(middleBg1, false);
        }
        else if (cam.transform.position.y < middleBg2.transform.position.y - bgOffset){
            middleBg2.transform.position = middleBg1.transform.position - new Vector3(0, bgOffset, 0);
            changeBg(middleBg2, false);
        }
    }

    void changeBg(GameObject bg, bool canBeEnd){
        if(nextSprite != null)
            bg.GetComponent<SpriteRenderer>().sprite = nextSprite;
        
        nextSprite = null;
        
        if(isEnd && canBeEnd){
            smoothCamera.ending(bg.transform.position.y);
            endBg.transform.position = bg.transform.position;
            endBg.SetActive(true);
            GetComponentInChildren<levelUpVine>().getDown(smoothCamera.limit);
        }
    }

    public void nextIsEnd(){
        isEnd = true;
    }

    void Reset(){
        isEnd = false;
        endBg.SetActive(false);
        GetComponentInChildren<levelUpVine>().resetPos();
    }
}
