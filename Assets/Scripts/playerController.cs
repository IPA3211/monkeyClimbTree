using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    // Start is called before the first frame update
    public float XPower;
    public float YPower;
    Rigidbody2D rigied;
    SpriteRenderer spriteRenderer;
    Vector2 saveVelo;
    bool isOnRight = true;
    bool isOnWall = false;
    bool isPaused = false;
    bool isLevelUped = false;

    void Start()
    {
        rigied = gameObject.GetComponent<Rigidbody2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        Jump(true);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!GameSystem.getPause()){
            if(isPaused){
                //퍼즈 되고서 돌아갈때
                isPaused = !isPaused;
                rigied.simulated = true;
                GameSystem.isLevelUping = true;
            }

            if(GameSystem.isLeveluped){
                rigied.simulated = false;
                transform.Translate(new Vector3(0, 1, 0) * Time.deltaTime * 5);
                if(GameSystem.playerHeight + 10 < gameObject.transform.position.y){
                    rigied.simulated = true;
                    GameSystem.isLeveluped = false;
                }
                return;
            }
            
            GameSystem.playerHeight = gameObject.transform.position.y;
            
            if(isOnWall){
                //벽에 부딪힐떄
                rigied.velocity = rigied.velocity * new Vector2(0,1);
            }

            if(Input.GetMouseButton(0) && isOnWall){
                //터치 될때
                if(isOnRight){
                    Jump(true);
                }
                else{
                    Jump(false);
                }
                isOnWall = false;
            }


        }
        else{
            //퍼즈 될때
            if(!isPaused){
                isPaused = true;
                rigied.simulated = false;
            }
        }
    }

    void Jump(bool isRight){
        if(isRight){
            rigied.velocity = new Vector2(-XPower, YPower);
            spriteRenderer.flipX = false;
            isOnRight = false;
        }
        else{
            rigied.velocity = new Vector2(XPower, YPower);
            spriteRenderer.flipX = true;
            isOnRight = true;
        }
        isOnWall = false;
    }
    void OnCollisionEnter2D (Collision2D other){
        if(other.gameObject.tag == "Wall" && !isOnWall){
            rigied.velocity = new Vector2(0, 0);
            isOnWall = true;
            Debug.Log("wow");
        }
    }
}
