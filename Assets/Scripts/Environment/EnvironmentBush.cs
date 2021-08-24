using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentBush : MonoBehaviour
{
    public float lifeTime;
    public int touchHealth;
    public float health;
    bool playerHit = false;
    Rigidbody2D playerRigid;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("BushLife");
    }

    void OnDestroy() {
        if(playerHit){
            playerRigid.simulated = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(playerHit){
            health -= Time.deltaTime;
            if(Input.GetMouseButtonDown(0)){
                touchHealth--;
            }
        }

        if(touchHealth == 0 || health < 0){
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Player"){
            playerRigid = other.gameObject.GetComponent<Rigidbody2D>();
            playerRigid.simulated = false;
            playerHit = true;
        }
    }

    void OnCollisionExit2D(Collision2D other){
        if(other.gameObject.tag == "Player"){
            playerHit = false;
        }
    }

    IEnumerator BushLife() {
        
        float progress = 0;

        while (progress <= 1)
        {
            gameObject.transform.localScale = Vector3.Lerp(new Vector3(0, 0, 1), new Vector3(1, 1, 1), progress);
            progress += 0.01f;
            yield return new WaitForSeconds(0.005f);
        }
        
        
        yield return new WaitForSeconds(lifeTime);

        progress = 0;
        while (progress <= 1)
        {
            gameObject.transform.localScale = Vector3.Lerp(new Vector3(1, 1, 1), new Vector3(0, 0, 1), progress);
            progress += 0.01f;
            yield return new WaitForSeconds(0.005f);
        }
        
        Destroy(gameObject);
    }
}
