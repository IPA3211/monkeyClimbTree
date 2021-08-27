using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelUpVine : MonoBehaviour
{
    GameObject cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameSystem.isStarted){
            transform.position = cam.transform.position + new Vector3(0, 25, 10);
            
            return;
        }
        if(GameSystem.isLeveluped){
            //플레이어가 넝쿨에 부딪혀서 타고 올라감
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            transform.Translate(new Vector3(0, 1, 0) * Time.deltaTime * 5);
        }
        else if(GameSystem.isLevelUping){
            //넝쿨 내려옴
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
            if((GameSystem.getLevel() + 1) * 20 < transform.position.y){
                transform.Translate(new Vector3(0, -1, 0) * Time.deltaTime * 5);
            }
        }
        else{
            //기본 상태
            if((GameSystem.getLevel() + 1) * 20 + 5 > transform.position.y){
                transform.Translate(new Vector3(0, 1, 0) * Time.deltaTime * 10);
            }
        }
    }
    void OnTriggerEnter2D (Collider2D other){
        Debug.Log("uu");
        if(other.gameObject.tag == "Player"){
            GameSystem.isLevelUping = false;
            GameSystem.levelUp();
        }
    }
}
