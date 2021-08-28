using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyPanzee : Enemy
{
    [Header("Panzee Config")]
    public float XPower;
    public float YPower;
    Rigidbody2D rigied;
    SpriteRenderer spriteRenderer;
    bool isOnRight = true;
    bool isOnWall = true;
    // Start is called before the first frame update
    override protected void FixedUpdate(){
        base.FixedUpdate();
        if(transform.position.x < -4.6){
            Jump(false);
        }
        
        if(transform.position.x > 4.6){
            Jump(true);
        }
    }
    override protected void Start(){
        base.Start();
        if(Random.Range(0f, 1f) > 0.5f){
            isOnRight = true;
        }
        else{
            isOnRight = false;
        }
        rigied = gameObject.GetComponent<Rigidbody2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        Jump(isOnRight);
    }

    override protected void WarnStarted(){
        gameObject.GetComponent<Rigidbody2D>().simulated = false;
        gameObject.GetComponent<Collider2D>().enabled = false;
    }
    
    override protected void WarnEnded(){
        gameObject.GetComponent<Rigidbody2D>().simulated = true;
        gameObject.GetComponent<Collider2D>().enabled = true;
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
        if(other.tag == "Player" && !other.GetComponent<playerController>().isOnWall && !other.GetComponent<playerController>().isImmune)
            Jump(isOnRight, XPower, rigied.velocity.y + 1f);
    }
}
