using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentBush : MonoBehaviour
{
    public float lifeTime;
    public int touchHealth;
    public float health;
    bool playerHit = false;
    Vector3 playerVelocity;
    bool isOnWall;
    float playerGravity;
    Animator anim;
    Rigidbody2D playerRigid;
    playerController pctrl;
    GameObject cam;
    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        cam = GameObject.FindWithTag("MainCamera");
        StartCoroutine("BushLife");
    }
    void Update()
    {
        if(playerHit){
            health -= Time.deltaTime;
            if(Input.GetMouseButtonDown(0)){
                touchHealth--;
                if (touchHealth == 4 || touchHealth == 2 || touchHealth == 0)
                    AudioManager.instance.Play("Bush1");
                else if (touchHealth == 3 || touchHealth == 1)
                    AudioManager.instance.Play("Bush2");
            }
        }

        if(touchHealth == 0 || health < 0){
            Destroy(gameObject);
        }

        if(GameSystem.isDead)
            Destroy(gameObject);
            
        if(cam.transform.position.y - 11 > gameObject.transform.position.y)
            Destroy(gameObject);
    }
    void OnDestroy() {
        
    }

    public void Monkey_Stucked()
    {
        anim.SetBool("IsStucked", true);
    }
    
    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Player"){
            
            playerHit = true;
        }
    }

    IEnumerator BushLife() {
        
        float progress = 0;

        while (progress <= 1)
        {
            gameObject.transform.localScale = Vector3.Lerp(new Vector3(0, 0, 1), new Vector3(1, 1, 1), progress);
            progress += 0.01f;
            yield return new WaitForSeconds(0.01f);
        }
        
        
        yield return new WaitForSeconds(lifeTime);

        progress = 0;
        while (progress <= 1)
        {
            gameObject.transform.localScale = Vector3.Lerp(new Vector3(1, 1, 1), new Vector3(0, 0, 1), progress);
            progress += 0.01f;
            yield return new WaitForSeconds(0.01f);
        }
        
        Destroy(gameObject);
    }
}
