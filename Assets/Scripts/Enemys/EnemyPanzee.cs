using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPanzee : MonoBehaviour
{
    public float XPower;
    public float YPower;
    Rigidbody2D rigied;
    SpriteRenderer spriteRenderer;
    bool isOnRight = true;
    bool isOnWall = true;
    // Start is called before the first frame update
    void Start()
    {
        if(Random.Range(0f, 1f) > 0.5f){
            isOnRight = true;
        }
        else{
            isOnRight = false;
        }
        rigied = gameObject.GetComponent<Rigidbody2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        StartCoroutine("WarnAttack");
        Jump(isOnRight);
        Destroy(gameObject, 10);
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x < -4.6){
            Jump(false);
        }
        
        if(transform.position.x > 4.6){
            Jump(true);
        }
    }
    void Jump(bool isRight, float x, float y){
        if(isRight){
            rigied.velocity = new Vector2(-x, y);
            spriteRenderer.flipX = true;
            isOnRight = false;
        }
        else{
            rigied.velocity = new Vector2(x, y);
            spriteRenderer.flipX = false;
            isOnRight = true;
        }
    }

    void Jump(bool isRight){
        Jump(isRight, XPower, YPower);
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player" && !other.GetComponent<playerController>().isOnWall)
            Jump(isOnRight, XPower, rigied.velocity.y + 1f);
    }

    IEnumerator WarnAttack() {
        gameObject.GetComponent<Rigidbody2D>().simulated = false;
        gameObject.GetComponent<Collider2D>().enabled = false;

        yield return new WaitForSeconds(2f);

        gameObject.GetComponent<Rigidbody2D>().simulated = true;
        gameObject.GetComponent<Collider2D>().enabled = true;
    }
}
