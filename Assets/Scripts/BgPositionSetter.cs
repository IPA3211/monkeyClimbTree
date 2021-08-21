using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgPositionSetter : MonoBehaviour
{


    // Start is called before the first frame update
    public GameObject bg1;
    public GameObject bg2;
    public GameObject cam;
    public float bgOffset;
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(cam.transform.position.y > bg1.transform.position.y + bgOffset){
            bg1.transform.position = bg2.transform.position + new Vector3(0, bgOffset, 0);
        }
        else if (cam.transform.position.y > bg2.transform.position.y + bgOffset){
            bg2.transform.position = bg1.transform.position + new Vector3(0, bgOffset, 0);
        }
    }
}
