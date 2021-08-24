using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    // Start is called before the first frame update
    public float XPower;
    public float YPower;
    public float doubleJumpPower = 5;
    public float immuneTime;
    Animator anim;
    Rigidbody2D rigied;
    SpriteRenderer spriteRenderer;
    Vector2 saveVelo;
    bool isStarted = false;
    bool isOnRight = true;
    public bool isOnWall = false;
    bool isPaused = false;
    bool isLevelUped = false;
    bool isImmune = false;
    public bool isDoubleJumped = true;
    GameObject cam;

    void Start()
    {
        cam = GameObject.FindWithTag("MainCamera");
        anim = gameObject.GetComponent<Animator>();
        rigied = gameObject.GetComponent<Rigidbody2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        rigied.simulated = false;
    }

    void Update(){
        if(isStarted == false){
            if(Input.GetMouseButtonDown(0)){
                isStarted = true;
                rigied.simulated = true;
                Jump(true);
            }
        }
        if(Input.GetMouseButtonDown(0) && !isOnWall && !isDoubleJumped && Mathf.Abs(gameObject.transform.position.x) < 3){
            if(doubleJumpPower > 1){
                rigied.velocity = new Vector2(rigied.velocity.x, doubleJumpPower);
                isDoubleJumped = true;
            }
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(!GameSystem.getPause()){
            if(isPaused){
                //퍼즈 되고서 돌아갈때
                isPaused = !isPaused;
                rigied.simulated = true;
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

            if(Input.GetMouseButton(0) && isOnWall && cam.transform.position.y + 8> transform.position.y){
                //터치 될때
                Jump(isOnRight);
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

    void Jump(bool isRight, float x, float y){
        if(isRight){
            rigied.velocity = new Vector2(-x, y);
            isOnRight = false;
        }
        else{
            rigied.velocity = new Vector2(x, y);
            isOnRight = true;
        }
        isOnWall = false;
        spriteRenderer.flipX = isRight;
        anim.SetBool("IsOnWall", isOnWall);
    }

    void Jump(bool isRight){
        Jump(isRight, XPower, YPower);
    }

    void OnCollisionEnter2D (Collision2D other){
        if((other.gameObject.tag == "Wall" || other.gameObject.tag == "EnemyWall") && !isOnWall){
            Debug.Log("wow");
            rigied.velocity = new Vector2(0, 0);
            isOnWall = true;
            isDoubleJumped = false;
            anim.SetBool("IsOnWall", isOnWall);
        }

        if (other.gameObject.tag == "EnemyWall")
        {
            playerHit();
        }
    }

    void OnCollisionStay2D(Collision2D other){
        if(other.gameObject.tag == "EnemyWall" || other.gameObject.tag == "Enemy"){
            playerHit();
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Enemy"){
            playerHit();
        }

        if(other.gameObject.tag == "EnemyBounce"){
            playerHit();
            if(!isOnWall){
                Jump(isOnRight, XPower, rigied.velocity.y + 1f);
            }
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
