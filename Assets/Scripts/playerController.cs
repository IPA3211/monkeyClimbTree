using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    // Start is called before the first frame update
    public float XPower;
    public float YPower;
    Rigidbody2D rigied;
    Vector2 saveVelo;
    bool isOnRight = true;
    bool isOnWall = false;
    bool isPaused = false;

    void Start()
    {
        rigied = gameObject.GetComponent<Rigidbody2D>();
        rigied.AddForce(new Vector2(XPower, YPower), ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GameSystem.playerHeight = gameObject.transform.position.y;
        if(!GameSystem.getPause()){
            if(isPaused){
                isPaused = !isPaused;
                rigied.simulated = true;
            }

            if(isOnWall){
                rigied.velocity = rigied.velocity * new Vector2(0,1);
            }
            if(Input.GetMouseButton(0) && isOnWall){
                if(isOnRight){
                    rigied.velocity = new Vector2(-XPower, YPower);
                    isOnRight = false;
                }
                else{
                    rigied.velocity = new Vector2(XPower, YPower);
                    isOnRight = true;
                }
                isOnWall = false;
            }
        }
        else{
            if(!isPaused){
                isPaused = true;
                rigied.simulated = false;
            }
        }
    }

    void OnCollisionEnter2D (Collision2D other){
        if(other.gameObject.tag == "Wall" && !isOnWall){
            rigied.velocity = new Vector2(0, 0);
            isOnWall = true;
            Debug.Log("wow");
        }
    }

}
