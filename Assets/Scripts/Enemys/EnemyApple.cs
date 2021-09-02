using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyApple : Enemy
{
    // Start is called before the first frame update
    override protected void FixedUpdate(){
        base.FixedUpdate();
    }
    override protected void Start(){
        base.Start();
    }

    public override string WhatsName()
    {
        return base.WhatsName();
    }

    override protected void WarnStarted(){
        gameObject.GetComponent<Rigidbody2D>().simulated = false;
        gameObject.GetComponent<Collider2D>().enabled = false;
    }
    
    override protected void WarnEnded(){
        gameObject.GetComponent<Rigidbody2D>().simulated = true;
        gameObject.GetComponent<Collider2D>().enabled = true;
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player" && !other.GetComponent<playerController>().isImmune){
            if(gameObject.transform.position.x > 0){
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-5, 10);
            }
            else{
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(5, 10);
            }
            other.GetComponent<playerController>().playerHit();
        }
    }
}
