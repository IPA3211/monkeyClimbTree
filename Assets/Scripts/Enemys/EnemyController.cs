using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameSystem.playerHeight - transform.position.y > 20){
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D (Collider2D other){
        Debug.Log("ww");
        if(other.gameObject.tag == "Player"){
            GameSystem.damaged(1);
            Destroy(gameObject);
        }
    }
}
