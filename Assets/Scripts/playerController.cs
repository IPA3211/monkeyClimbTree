using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    // Start is called before the first frame update
    public float XPower;
    public float YPower;
    public float immuneTime;
    Animator anim;
    Rigidbody2D rigied;
    SpriteRenderer spriteRenderer;
    Vector2 saveVelo;
    bool isOnRight = true;
    bool isOnWall = false;
    bool isPaused = false;
    bool isLevelUped = false;
    bool isImmune = false;

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
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
                anim.SetBool("IsOnWall", isOnWall);
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
            spriteRenderer.flipX = true;
            isOnRight = false;
        }
        else{
            rigied.velocity = new Vector2(XPower, YPower);
            spriteRenderer.flipX = false;
            isOnRight = true;
        }
        isOnWall = false;
        anim.SetBool("IsOnWall", isOnWall);
    }
    void OnCollisionEnter2D (Collision2D other){
        if((other.gameObject.tag == "Wall" || other.gameObject.tag == "EnemyWall") && !isOnWall){
            rigied.velocity = new Vector2(0, 0);
            isOnWall = true;
            anim.SetBool("IsOnWall", isOnWall);
        }

        if (other.gameObject.tag == "EnemyWall")
        {
            playerHit();
        }
    }

    void OnCollisionStay2D(Collision2D other){
        if(other.gameObject.tag == "EnemyWall"){
            playerHit();
        }
    }

    public void playerHit(){
        if(!isImmune){
            GameSystem.damaged(1);
            StartCoroutine("playerImmuned");
        }
    }

    IEnumerator playerImmuned() {
        isImmune = true;
        SpriteRenderer rend = gameObject.GetComponent<SpriteRenderer>();
        for(int i = 0; i < immuneTime * 10; i++){
            rend.enabled = !rend.enabled;
            yield return new WaitForSeconds(0.1f);
        }
        rend.enabled = true;
        isImmune = false;
    }
}
