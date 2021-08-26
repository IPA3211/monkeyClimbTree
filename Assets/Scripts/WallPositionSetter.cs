using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallPositionSetter : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject wall1;
    public GameObject wall2;
    public GameObject wallOnCam;
    public GameObject cam;
    public float wallOffset;
    void Start()
    {
        wallOnCam = wall1;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(cam.transform.position.y >= wall1.transform.position.y + wallOffset){
            wall1.transform.position = wall2.transform.position + new Vector3(0, wallOffset, 0);
            wallOnCam = wall2;
        }
        else if (cam.transform.position.y >= wall2.transform.position.y + wallOffset){
            wall2.transform.position = wall1.transform.position + new Vector3(0, wallOffset, 0);
            wallOnCam = wall1;
        }

        if (cam.transform.position.y < wall1.transform.position.y - wallOffset){
            wall1.transform.position = wall2.transform.position - new Vector3(0, wallOffset, 0);
            wallOnCam = wall2;
        }
        else if (cam.transform.position.y < wall2.transform.position.y - wallOffset){
            wall2.transform.position = wall1.transform.position - new Vector3(0, wallOffset, 0);
            wallOnCam = wall1;
        }
    }
}
