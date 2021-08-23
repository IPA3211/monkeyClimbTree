using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelUpVine : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(GameSystem.isLeveluped){
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            transform.Translate(new Vector3(0, 1, 0) * Time.deltaTime * 5);
        }

        else if(GameSystem.isLevelUping){
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
            if((GameSystem.level + 1) * 20 < transform.position.y){
                transform.Translate(new Vector3(0, -1, 0) * Time.deltaTime * 5);
            }
        }
        else{
            if((GameSystem.level + 1) * 20 + 5 > transform.position.y){
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
