using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Config")]
    public string enemyName;
    public GameObject warnSprite;
    public GameObject warnIconSprite;
    public float autoDestroyTime;
    protected Vector3 diffPos;
    protected GameObject cam;
    protected bool isInAction;
    
    private void handCheck(){
        switch(GameSystem.whichHand){
            case handPos.PLAYER_HAND_RIGHT:
                if(transform.position.x > 1 && transform.position.y - cam.transform.position.y < -10){
                    warnSprite.transform.localPosition = warnSprite.transform.localPosition + new Vector3(0, 5, 0);
                    warnIconSprite.transform.localPosition = warnIconSprite.transform.localPosition + new Vector3(0, 5, 0);
                }
            break;
            case handPos.PLAYER_HAND_LEFT:
                if(transform.position.x < -1 && transform.position.y - cam.transform.position.y < -10){
                    warnSprite.transform.localPosition = warnSprite.transform.localPosition + new Vector3(0, 5, 0);
                    warnIconSprite.transform.localPosition = warnIconSprite.transform.localPosition + new Vector3(0, 5, 0);
                }
            break;
            case handPos.PLAYER_HAND_BOTH:
                if(transform.position.y - cam.transform.position.y < -10){
                    warnSprite.transform.localPosition = warnSprite.transform.localPosition + new Vector3(0, 5, 0);
                    warnIconSprite.transform.localPosition = warnIconSprite.transform.localPosition + new Vector3(0, 5, 0);
                }
            break;
        }
    }

    virtual protected void WarnStarted(){}
    virtual protected void WarnEnded(){}
    
    protected virtual void Start(){
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        diffPos = transform.position - (cam.transform.position - new Vector3(0, 0, 10));
        handCheck();
        StartCoroutine("WarnAttack");
        Destroy(gameObject, autoDestroyTime);
    }
    protected virtual void LineStart(){
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        diffPos = transform.position - (cam.transform.position - new Vector3(0, 0, 10));
        StartCoroutine("WarnLineAttack");
        Destroy(gameObject, autoDestroyTime);
    }

    protected virtual void FixedUpdate()
    {
        if(!isInAction){
            transform.position = cam.transform.position + diffPos;
        }
        if(GameSystem.isDead)
            Destroy(gameObject);
    }

    public virtual string WhatsName()
    {
        return enemyName;
    }

    IEnumerator WarnAttack() {
        WarnStarted();
        isInAction = false;
        SpriteRenderer rend = warnSprite.GetComponent<SpriteRenderer>();
        SpriteRenderer rend2 = warnIconSprite.GetComponent<SpriteRenderer>();

        float progress = 0;
        for (int i = 0; i < 2; i++)
        {
            progress = 0;
            while (progress < 1)
            {
                rend.color = Color.Lerp(new Color(1, 0, 0, 0), new Color(1, 0, 0, 0.75f), progress);
                rend2.color = Color.Lerp(new Color(1, 1, 1, 0), new Color(1, 1, 1, 0.75f), progress);
                progress += Time.deltaTime;
                yield return new WaitForFixedUpdate();
            }
            progress = 0;
            while (progress < 1)
            {
                rend.color = Color.Lerp(new Color(1, 0, 0, 0.75f), new Color(1, 0, 0, 0), progress);
                rend2.color = Color.Lerp(new Color(1, 1, 1, 0.75f), new Color(1, 1, 1, 0), progress);
                progress += Time.deltaTime;
                yield return new WaitForFixedUpdate();
            }
        }
        rend.color = new Color(1, 0, 0, 0);
        rend2.color = new Color(1, 1, 1, 0);
        isInAction = true;
        WarnEnded();
    }

    IEnumerator WarnLineAttack() {
        WarnStarted();
        isInAction = false;
        LineRenderer rend = warnSprite.GetComponent<LineRenderer>();
        SpriteRenderer rend2 = warnIconSprite.GetComponent<SpriteRenderer>();
        float progress = 0;
        for (int i = 0; i < 2; i++)
        {
            progress = 0;
            while (progress < 1)
            {
                rend.endColor = Color.Lerp(new Color(1, 0, 0, 0), new Color(1, 0, 0, 0.5f), progress);
                rend.startColor = Color.Lerp(new Color(1, 0, 0, 0), new Color(1, 0, 0, 0.5f), progress);
                rend2.color = Color.Lerp(new Color(1, 1, 1, 0), new Color(1, 1, 1, 0.5f), progress);
                progress += Time.deltaTime;
                yield return new WaitForFixedUpdate();
            }
            progress = 0;
            while (progress < 1)
            {
                rend.endColor = Color.Lerp(new Color(1, 0, 0, 0.5f), new Color(1, 0, 0, 0), progress);
                rend.startColor = Color.Lerp(new Color(1, 0, 0, 0.5f), new Color(1, 0, 0, 0), progress);
                rend2.color = Color.Lerp(new Color(1, 1, 1, 0.5f), new Color(1, 1, 1, 0), progress);
                progress += Time.deltaTime;
                yield return new WaitForFixedUpdate();
            }
        }
        rend.startColor = new Color(1, 0, 0, 0);
        rend.endColor = new Color(1, 0, 0, 0);
        rend2.color = new Color(1, 1, 1, 0);
        isInAction = true;
        WarnEnded();
    }
}
