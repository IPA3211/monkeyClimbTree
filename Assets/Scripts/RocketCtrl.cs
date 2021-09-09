using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketCtrl : MonoBehaviour
{
    public float speed;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    void OnEnable(){
        player = GameObject.FindWithTag("Player");
        transform.position = new Vector3(0, -12, 0);
    }
    
    void OnDisable(){
        player.GetComponent<SpriteRenderer>().color = Color.white;
    }


    // Update is called once per frame
    void Update()
    {
        if(!GameSystem.isStarted){
            transform.position = Vector3.Lerp(transform.position, player.transform.position, 0.01f);
            player.GetComponent<SpriteRenderer>().color = Color.clear;
        }
        else{
            transform.Translate(Vector3.up * Time.deltaTime * speed);   
            player.GetComponent<SpriteRenderer>().color = Color.white;
            Destroy(gameObject, 5f);
        }
    }
}
