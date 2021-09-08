using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemySnake : Enemy
{
    [Header("Snake Config")]
    public float speed;
    
    // Start is called before the first frame update
    override protected void FixedUpdate(){
        base.FixedUpdate();
        if(isInAction){
            gameObject.transform.Translate(Vector3.up * Time.deltaTime * speed);
        }
    }
    override protected void Start(){
        base.Start();
    }

    public override string WhatsName()
    {
        return base.WhatsName();
    }

    override protected void WarnStarted(){
        gameObject.GetComponent<Collider2D>().enabled = false;
    }
    
    override protected void WarnEnded(){
        gameObject.GetComponent<Collider2D>().enabled = true;
        AudioManager.instance.Play("Snake");
    }
}
