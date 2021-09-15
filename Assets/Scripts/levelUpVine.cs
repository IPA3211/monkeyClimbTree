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
    void OnTriggerEnter2D (Collider2D other){
        if(other.gameObject.tag == "Player"){
            playerController pc = other.gameObject.GetComponent<playerController>();
            pc.anim.SetBool("IsDoubleJump", false);
            pc.anim.SetBool("IsOnWall", true);
            
            GameSystem.isStageCleared = true;
            StartCoroutine("getUp", gameObject);
            StartCoroutine("getUp", other.gameObject);
        }
    }

    public void getDown(float y){
        GameSystem.isLevelUping = true;
        GetComponent<Collider2D>().enabled = true;
        transform.position = new Vector3(0, y + 20, 0);
    }

    IEnumerator getUp(GameObject obj) {
        float progress = 0;
        Vector3 firstPos = obj.transform.position;
        Vector3 diffPos = firstPos - cam.transform.position + new Vector3(0, 0, 10);
        while(progress < 1){
            obj.transform.position = Vector3.Lerp(firstPos, (cam.transform.position + new Vector3(0, 0, 10)) + diffPos + Vector3.up * 10, progress);
            progress += (Time.deltaTime / 2);
            yield return new WaitForFixedUpdate();
        }
        //GameSystem.setHealth(0);
        GameSystem.isStageCleared = true;
        GameSystem.playClearUI = true;
        GetComponent<Collider2D>().enabled = false;
    }
}
