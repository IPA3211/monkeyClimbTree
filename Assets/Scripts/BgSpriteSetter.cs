using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgSpriteSetter : MonoBehaviour
{
    public Sprite first;
    public List<Sprite> middle;
    public Sprite end;
    public bool isEnd = false;
    SmoothCamera cam;
    BgPositionSetter bgPos;
    int count;
    // Start is called before the first frame update
    void Start()
    {
        bgPos = GetComponent<BgPositionSetter>();
        cam = GetComponent<SmoothCamera>();
        count = 0;

        bgPos.bg1.GetComponent<SpriteRenderer>().sprite = first;
        if(middle.Count == 0){
            bgPos.bg2.GetComponent<SpriteRenderer>().sprite = end;
            cam.limit = bgPos.bg2.transform.position.y;
            isEnd = true;
        }
        else 
            bgPos.bg2.GetComponent<SpriteRenderer>().sprite = middle[count++];
    }

    // Update is called once per frame
    void Update()
    {
        if(GameSystem.isRestarted){
            isEnd = false;
        }
        
        if(bgPos.nextSprite == null){
            bgPos.nextSprite = getNextSprite();
        }

        if(isEnd){
            bgPos.nextIsEnd();
            bgPos.nextSprite = getNextSprite();
        }
    }

    Sprite getNextSprite(){
        if(count == middle.Count){
            count = 0;
        }

        if(count == -1){
            count = middle.Count - 1;
        }

        if(isEnd){
            return end;
        }
        else if(GameSystem.isRestarted){
            return middle[count--];
        }
        else
            return middle[count++];
    }
}
