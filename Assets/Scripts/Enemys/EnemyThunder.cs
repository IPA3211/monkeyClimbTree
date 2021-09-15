using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyThunder : Enemy
{
    public float thunderTime;
    public float delayTime;
    Animator anim;
    SpriteRenderer spriteRenderer;
    Collider2D col;
    protected override void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetFloat("Speed", Random.Range(1f, 2f));
        base.Start();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        col = gameObject.GetComponent<Collider2D>();
        col.enabled = false;
        spriteRenderer.color = new Color32(241, 255, 0,0);

        AudioManager.instance.Play("ThunderStart");
    }

    protected override void WarnStarted()
    {
        base.WarnStarted();
        StartCoroutine("delaySound");
    }

    protected override void WarnEnded()
    {
        
        base.WarnEnded();
        StartCoroutine("thunderCoru");        
    }

    IEnumerator delaySound()
    {
        yield return new WaitForSeconds(delayTime);
        AudioManager.instance.Play("ThunderAgain1");
        AudioManager.instance.Play("ThunderAgain2");
    }

    IEnumerator thunderCoru(){
        

        spriteRenderer.color = new Color32(241, 255, 0, 255);
        col.enabled = true;        
        yield return new WaitForSeconds(thunderTime);
        
        Destroy(gameObject);
    }
}
