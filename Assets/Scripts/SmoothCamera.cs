using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCamera : MonoBehaviour
{
    public GameObject player;
    public GameObject cam;
    public float camSpeed;
    public float camOffset;
    [HideInInspector]
    public float maxYPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!GameSystem.getPause()){
            if(maxYPos < player.transform.position.y){
                maxYPos = player.transform.position.y;
            }
            if(maxYPos > cam.transform.position.y + camOffset){
                cam.transform.position = Vector3.Lerp(cam.transform.position, new Vector3(0, maxYPos - camOffset, -10), Time.deltaTime * camSpeed);
            }
        }
    }
}
