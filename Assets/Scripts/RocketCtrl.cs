using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketCtrl : MonoBehaviour
{
    public float speed;
    public float lifeTime;
    public float camSpeed;
    public float height;
    float sumTime;
    float tempCamSpeed;
    bool hasSoundPlayed = false;
    GameObject player;
    public GameObject magnet_icon;
    SmoothCamera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = RuntimeGameManager.gameManager.GetComponent<SmoothCamera>();
        tempCamSpeed = cam.camSpeed;
    }

    void OnEnable(){
        sumTime = 0;
        player = GameObject.FindWithTag("Player");
        transform.position = new Vector3(0, -12, 0);
    }
    
    void OnDisable(){
        player.GetComponent<SpriteRenderer>().color = Color.white;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if(!GameSystem.isStarted || GameSystem.isDead){
            transform.position = Vector3.Lerp(transform.position, new Vector3(0, -7, 0), 0.2f);
            player.GetComponent<SpriteRenderer>().color = Color.clear;
            if(GameSystem.hasMagnetic)
                magnet_icon.GetComponent<SpriteRenderer>().color = Color.clear;
        }
        else if(GameSystem.hasBooster && !GameSystem.isDead)
        {
            player.GetComponent<SpriteRenderer>().color = Color.white;
            if (GameSystem.hasMagnetic)
                magnet_icon.GetComponent<SpriteRenderer>().color = Color.white;
            player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            cam.camSpeed = camSpeed;
            transform.Translate(Vector3.up * Time.deltaTime * speed);
            player.transform.Translate(Vector3.up * Time.deltaTime * speed);

            if(!hasSoundPlayed)
            {
                hasSoundPlayed = true;
                AudioManager.instance.Play("Rocket");
            }
            if(GameSystem.playerHeight > height){
                GameSystem.hasBooster = false;
                cam.camSpeed = tempCamSpeed;
                player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                player.GetComponent<playerController>().Jump(true);
            }
        }        
        else
        {
            hasSoundPlayed = false;
            if (sumTime > lifeTime){
                gameObject.SetActive(false);
            }
            transform.Translate(Vector3.up * Time.deltaTime * speed);   
            player.GetComponent<SpriteRenderer>().color = Color.white;
            sumTime += Time.deltaTime;
        }

        if (!GameSystem.hasBooster && GameSystem.isDead)
        {
            Debug.Log("why");
            gameObject.SetActive(false);
        }
    }
}
