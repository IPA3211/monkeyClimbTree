using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgPositionSetter : MonoBehaviour
{


    // Start is called before the first frame update
    public GameObject bg1;
    public GameObject bg2;
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

        if(cam.transform.position.y > bg1.transform.position.y + bgOffset){
            bg1.transform.position = bg2.transform.position + new Vector3(0, bgOffset, 0);
            changeBg(bg1);
        }
        else if (cam.transform.position.y > bg2.transform.position.y + bgOffset){
            bg2.transform.position = bg1.transform.position + new Vector3(0, bgOffset, 0);
            changeBg(bg2);
        }
        if(cam.transform.position.y < bg1.transform.position.y - bgOffset){
            bg1.transform.position = bg2.transform.position - new Vector3(0, bgOffset, 0);
            changeBg(bg1);
        }
        else if (cam.transform.position.y < bg2.transform.position.y - bgOffset){
            bg2.transform.position = bg1.transform.position - new Vector3(0, bgOffset, 0);
            changeBg(bg2);
        }
    }

    void changeBg(GameObject bg){
        if(nextSprite != null)
            bg.GetComponent<SpriteRenderer>().sprite = nextSprite;
        
        nextSprite = null;
        
        if(isEnd){
            smoothCamera.limit = bg.transform.position.y;
            GetComponentInChildren<levelUpVine>().getDown(smoothCamera.limit);
        }
    }

    public void nextIsEnd(){
        isEnd = true;
    }

    void Reset(){
        isEnd = false;
    }
}
