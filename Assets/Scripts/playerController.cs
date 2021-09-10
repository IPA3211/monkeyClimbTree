using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    // Start is called before the first frame update
    public float XPower;
    public float YPower;
    public float doubleJumpPower;
    public float immuneTime;
    Animator anim;
    Rigidbody2D rigied;
    SpriteRenderer spriteRenderer;
    Vector2 saveVelo;

    public GameObject coinParticle;
    public GameObject dustParticle;
    public GameObject potionParticle;
    ParticleSystem dust;

    public bool isOnRight = true;
    public bool isOnWall = false;
    bool isPaused = false;
    public bool isImmune = false;
    public bool isDoubleJumped = true;
    GameObject stuckBush;
    GameObject cam;
    float tempY;

    void Start()
    {
        cam = GameObject.FindWithTag("MainCamera");
        anim = gameObject.GetComponent<Animator>();
        rigied = gameObject.GetComponent<Rigidbody2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        dust = dustParticle.GetComponent<ParticleSystem>();
        rigied.simulated = false;
        Jump(true);
    }

    void Update(){
        //FixedUpdate 에서는 GetMouseButtonDown 인식이 정확하지 않아서 Update 문으로 왔음
        if(GameSystem.isStarted == false){
            return;
        }
        if(Input.GetMouseButtonDown(0) && !isOnWall && !isDoubleJumped && !GameSystem.isDead && Mathf.Abs(gameObject.transform.position.x) < 3.3f){
            //더블점프 사용가능 범위때문에 나누긴 했는데 결국엔 다시 돌아옴 ㅋㅋ
            if(doubleJumpPower > 1){
                if(isOnRight)
                    rigied.velocity = new Vector2(7f, doubleJumpPower);
                
                else
                    rigied.velocity = new Vector2(-7f, doubleJumpPower);
                
                isDoubleJumped = true;
                anim.SetBool("IsDoubleJump", isDoubleJumped);
            }
        }
        
        
    }

    void FixedUpdate()
    {
        if(!GameSystem.isStarted){
            gameObject.transform.position = cam.transform.position + new Vector3(0, -4, 10);
        }

        if(!GameSystem.getPause() && GameSystem.isStarted && !GameSystem.isDead && !GameSystem.isStageCleared)
        {
            if(isPaused){
                //퍼즈 되고서 돌아갈때
                isPaused = !isPaused;
                rigied.simulated = true;
                Jump(true);
            }
            //bush 에 걸렸을 때
            if(stuckBush){
                if(rigied.constraints == RigidbodyConstraints2D.FreezeRotation){
                    saveVelo = rigied.velocity;
                    rigied.constraints = RigidbodyConstraints2D.FreezeAll;
                }
            }
            else{
                if(rigied.constraints == RigidbodyConstraints2D.FreezeAll){
                    rigied.velocity = saveVelo;
                    rigied.constraints = RigidbodyConstraints2D.FreezeRotation;
                }
            }
            //게임 시스템에 플레이어 높이 갱신
            tempY = gameObject.transform.position.y;
            GameSystem.playerHeight = tempY;
            GameSystem.setMaxHeight(tempY);
            if(isOnWall){
                //벽에 부딪혀 있을경우
                MonkeyOnWall();
            }

            if(transform.position.y < cam.transform.position.y - 15f){
                //원숭이가 카메라 아래 5지점에 있을경우
                GameSystem.setHealth(0);
                GameSystem.deadSign = "EnemyWall";
            }
        }
        else{
            if(GameSystem.isDead){
                MonkeyDead();
            }
            else if(!isPaused){
                //퍼즈 될때
                isPaused = true;
                rigied.simulated = false;
            }            
        }
        anim.SetBool("isDead", GameSystem.isDead);
    }

    void MonkeyDead()
    {
        /*
        GameSystem.hasMagnetic = false;
        GameSystem.hasBooster = false;
        GameSystem.hasShield = false;
        */
        if (rigied.constraints == RigidbodyConstraints2D.FreezeAll)
        {
            rigied.velocity = saveVelo;
            rigied.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        GameSystem.setTimeScale(0.75f);
        isOnWall = false;
        isDoubleJumped = false;
        if (isOnRight)
            rigied.velocity = new Vector2(-3f, rigied.velocity.y);
        else
            rigied.velocity = new Vector2(3f, rigied.velocity.y);
    }

    void MonkeyOnWall(){
        if(!GameSystem.isDead)
        {
            StartDust();
            rigied.velocity = rigied.velocity * new Vector2(0, 1);
            if (Input.GetMouseButton(0) && cam.transform.position.y + 8 > transform.position.y)
            {
                //터치 될때
                StopDust();
                Jump(isOnRight);
                Counts.jumpCount++;
                
            }
        }        
    }

    public void Jump(bool isRight, float x, float y){
        //파워를 직접 정해줄 수 있는 Jump함수
        if(rigied.constraints == RigidbodyConstraints2D.FreezeAll)
            return;

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
        //미리 지정된 파워를 사용하는 Jump override 함수
        Jump(isRight, XPower, YPower);
    }

    void StartDust()
    {
        if(!isOnRight)
        {
            dustParticle.transform.localPosition = new Vector3(-0.35f, dustParticle.transform.localPosition.y, dustParticle.transform.localPosition.z);
        }
        else
        {
            dustParticle.transform.localPosition = new Vector3(0.35f, dustParticle.transform.localPosition.y, dustParticle.transform.localPosition.z);
        }
        
        dust.Play();
    }

    void StopDust()
    {
        dust.Stop();
    }

    void OnCollisionEnter2D (Collision2D other){
        if((other.gameObject.tag == "Wall" || other.gameObject.tag == "EnemyWall") && !isOnWall){
            //벽에 부딪힐때
            rigied.velocity = new Vector2(0, 0);
            isOnWall = true;
            isDoubleJumped = false;
            anim.SetBool("IsOnWall", isOnWall);
            anim.SetBool("IsDoubleJump", isDoubleJumped);
        }
    }

    void OnCollisionStay2D(Collision2D other){
        if(other.gameObject.tag == "EnemyWall" || other.gameObject.tag == "Enemy"){
            playerHit();
        }
    }

    void OnTriggerStay2D(Collider2D other){
        if(!GameSystem.isDead && !GameSystem.isStageCleared)
        {
            if (other.gameObject.tag == "EnemyWall")
            {
                GameSystem.deadSign = "EnemyWall";
                Debug.Log("deadSign: " + GameSystem.deadSign);
                playerHit();
            }

            if (other.gameObject.tag == "Enemy")
            {
                GameSystem.deadSign = other.gameObject.GetComponent<Enemy>().WhatsName();
                Debug.Log("deadSign: " + GameSystem.deadSign);
                playerHit();
            }

            if (other.gameObject.tag == "EnemyBounce")
            {
                if (!isOnWall && !isImmune)
                {
                    if (isOnRight != other.GetComponent<EnemyPanzee>().isJumpRight())
                    {
                        Jump(isOnRight, XPower, rigied.velocity.y + 1f);
                        other.GetComponent<EnemyPanzee>().Jump();
                    }
                }
                if(!isImmune)
                {
                    GameSystem.deadSign = other.gameObject.GetComponent<Enemy>().WhatsName();
                    Debug.Log("deadSign: " + GameSystem.deadSign);
                }
                playerHit();
            }

            if (other.gameObject.tag == "Bush")
            {
                stuckBush = other.gameObject;
                stuckBush.GetComponent<EnvironmentBush>().Monkey_Stucked();
            }            
        }

        if (other.gameObject.tag == "Coin")
        {
            AudioManager.instance.Play("Coin");
            Instantiate(coinParticle, other.gameObject.transform.position, other.gameObject.transform.rotation);
            GameSystem.addCoin(1);
            GameSystem.addCoinEarned(1);
            GameSystem.addScore(10);
            Destroy(other.gameObject);
            GameSystem.deadSign = "Coin";
        }

        if (other.gameObject.tag == "Potion")
        {
            AudioManager.instance.Play("Potion");
            Instantiate(potionParticle, other.gameObject.transform.position, other.gameObject.transform.rotation);
            GameSystem.setBanana(GameSystem.getBanana() + 1);
            Destroy(other.gameObject);
            GameSystem.deadSign = "Potion";
        }
    }

    public void playerHit(){
        if(!isImmune && !GameSystem.isDead){            
            if(GameSystem.isCanVive){
                RDG.Vibration.Vibrate((long)500);
            }
            AudioManager.instance.Play("Monkey_Cry");
            GameSystem.damaged(1);
            if(!GameSystem.isDead)
            {
                StartCoroutine("playerImmuned");
            }            
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
