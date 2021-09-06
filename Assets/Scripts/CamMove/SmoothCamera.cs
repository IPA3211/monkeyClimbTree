using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCamera : MonoBehaviour
{
    public GameObject player;
    public GameObject cam;
    public float camSpeed;
    public float camOffset;
    public float limit;
    public float maxYPos;
    bool fixCam;
    bool isEnd;
    // Start is called before the first frame update
    void Start()
    {
        maxYPos = camOffset;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!GameSystem.getPause()){
            if(maxYPos < player.transform.position.y){
                maxYPos = player.transform.position.y;
            }

            if(maxYPos > limit){
                maxYPos = limit;
            }

            if(GameSystem.isRestarted){
                Reset();

                if(Mathf.Abs (0 - cam.transform.position.y) < 0.1)
                    GameSystem.isRestarted = false;
            }

            if(isEnd)
                cam.transform.position = Vector3.Lerp(cam.transform.position, new Vector3(0, maxYPos, -10), Time.deltaTime * camSpeed);
            else
                cam.transform.position = Vector3.Lerp(cam.transform.position, new Vector3(0, maxYPos - camOffset, -10), Time.deltaTime * camSpeed);

            if(Mathf.Abs (limit - cam.transform.position.y) < 0.5){
                if(fixCam == false){
                    fixCam = true;
                    cam.transform.position = new Vector3(0, limit, -10);
                }
            }
        }
    }

    public void ending(float pos){
        limit = pos;
        isEnd = true;
    }

    void Reset(){
        maxYPos = camOffset;
        limit = 10000;
        fixCam = false;
        isEnd = false;
    }
}
