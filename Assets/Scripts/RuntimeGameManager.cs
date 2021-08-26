using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuntimeGameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void levelUp(){
        GameSystem.isLevelUping = true;
    }
    public void gameStart(){
        if(!gameObject.GetComponent<CamMoveByLevel>().isMoving)
            GameSystem.isStarted = true;
    }
}
