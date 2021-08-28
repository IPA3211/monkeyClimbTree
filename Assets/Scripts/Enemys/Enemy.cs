using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Config")]
    public GameObject warnSprite;
    public float autoDestroyTime;
    virtual protected void WarnStarted(){}
    virtual protected void WarnEnded(){}
    
    protected virtual void Start(){
        StartCoroutine("WarnAttack");
        Destroy(gameObject, autoDestroyTime);
    }

    protected virtual void FixedUpdate()
    {
        if(GameSystem.isDead)
            Destroy(gameObject);
    }
    IEnumerator WarnAttack() {
        WarnStarted();
        SpriteRenderer rend = warnSprite.GetComponent<SpriteRenderer>();
        Color rawColor = rend.material.color;
        float progress = 0;
        for (int i = 0; i < 2; i++)
        {
            progress = 0;
            while (progress < 1)
            {
                rend.color = Color.Lerp(new Color(1, 0, 0, 0), new Color(1, 0, 0, 0.5f), progress);
                progress += 0.1f;
                yield return new WaitForSeconds(0.05f);
            }
            progress = 0;
            while (progress < 1)
            {
                rend.color = Color.Lerp(new Color(1, 0, 0, 0.5f), new Color(1, 0, 0, 0), progress);
                progress += 0.1f;
                yield return new WaitForSeconds(0.05f);
            }
        }
        rend.color = new Color(1, 0, 0, 0);
        WarnEnded();
    }
}
