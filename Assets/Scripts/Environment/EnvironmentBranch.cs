using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentBranch : MonoBehaviour
{
    public float LifeTime;
    public float warningTime;
    public float health = 1.5f;
    bool playerHit = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("BranchLifeCycle");
    }

    // Update is called once per frame
    void Update()
    {
        if(playerHit)
            health -= Time.deltaTime;

        if(health < 0){
            Destroy(gameObject);
        }
    }
    void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.tag == "Player"){
            playerHit = true;
        }
    }

    void OnCollisionExit2D(Collision2D other){
        if(other.gameObject.tag == "Player"){
            playerHit = false;
        }
    }
    IEnumerator BranchLifeCycle() {
        
        yield return new WaitForSeconds(LifeTime - warningTime);
        SpriteRenderer rend = gameObject.GetComponent<SpriteRenderer>();
        for(int i = 0; i < warningTime * 10; i++){
            rend.enabled = !rend.enabled;
            yield return new WaitForSeconds(0.1f);
        }
        Destroy(gameObject);
    }
}
