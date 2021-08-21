using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMoveByLevel : MonoBehaviour
{
    public GameObject cam;
    public float camSpeed;
    public float moveDistance;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!GameSystem.getPause()){
            cam.transform.position = Vector3.Lerp(cam.transform.position, new Vector3(0, GameSystem.level * moveDistance, -10), Time.deltaTime * camSpeed);
            if(cam.transform.position.y > GameSystem.level * moveDistance - 0.2f){
                cam.transform.position = new Vector3(0, GameSystem.level * moveDistance, -10);
            }
        }
    }
}
