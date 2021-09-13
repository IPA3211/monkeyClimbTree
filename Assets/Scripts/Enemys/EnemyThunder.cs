using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyThunder : Enemy
{
    public float thunderTime;
    SpriteRenderer spriteRenderer;
    Collider2D col;
    protected override void Start()
    {
        base.Start();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        col = gameObject.GetComponent<Collider2D>();
        col.enabled = false;
        spriteRenderer.color = new Color(1,1,1,0);

        AudioManager.instance.Play("ThunderStart");
    }

    protected override void WarnEnded()
    {
        AudioManager.instance.Play("ThunderAgain1");
        base.WarnEnded();
        StartCoroutine("thunderCoru");        
    }

    IEnumerator thunderCoru(){
        AudioManager.instance.Play("ThunderAgain2");

        spriteRenderer.color = new Color(1,1,1,1);
        col.enabled = true;        
        yield return new WaitForSeconds(thunderTime);
        
        Destroy(gameObject);
    }
}
