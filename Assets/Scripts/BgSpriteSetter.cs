using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpriteSet{
    public Sprite first;
    public List<Sprite> middle;
    public Sprite end;
}

public class BgSpriteSetter : MonoBehaviour
{
    SpriteSet spriteSet;
    public bool isEnd = false;
    SmoothCamera cam;
    BgPositionSetter bgPos;
    int count;
    // Start is called before the first frame update
    void Start()
    {
        bgPos = GetComponent<BgPositionSetter>();
        cam = GetComponent<SmoothCamera>();        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(GameSystem.isRestarted){
            isEnd = false;
        }
        
        if(bgPos.nextSprite == null){
            bgPos.nextSprite = getNextSprite();
        }

        if(isEnd){
            bgPos.nextIsEnd();
        }
    }

    Sprite getNextSprite(){
        if(count == spriteSet.middle.Count){
            count = 0;
        }

        if(count == -1){
            count = spriteSet.middle.Count - 1;
        }

        if(GameSystem.isRestarted){
            return spriteSet.middle[count--];
        }
        else
            return spriteSet.middle[count++];
    }

    public void ChangeSpriteSet(SpriteSet sp){
        Debug.Log("changed");
        spriteSet = sp;

        count = 0;
        bgPos.nextSprite = null;
        bgPos.firstBg.GetComponent<SpriteRenderer>().sprite = spriteSet.first;
        bgPos.endBg.GetComponent<SpriteRenderer>().sprite = spriteSet.end;
        if(spriteSet.middle.Count == 0){
            bgPos.endBg.GetComponent<SpriteRenderer>().sprite = spriteSet.end;
            cam.ending(bgPos.endBg.transform.position.y);
            isEnd = true;
        }
        else 
            bgPos.middleBg2.GetComponent<SpriteRenderer>().sprite = spriteSet.middle[count++];
    }
}
