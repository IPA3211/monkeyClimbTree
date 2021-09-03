using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Config")]
    public string enemyName;
    public GameObject warnSprite;
    public float autoDestroyTime;
    protected Vector3 diffPos;
    protected GameObject cam;
    protected bool isInAction;
    virtual protected void WarnStarted(){}
    virtual protected void WarnEnded(){}
    
    protected virtual void Start(){
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        diffPos = transform.position - (cam.transform.position - new Vector3(0, 0, 10));
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
        return name;
    }

    IEnumerator WarnAttack() {
        WarnStarted();
        isInAction = false;
        SpriteRenderer rend = warnSprite.GetComponent<SpriteRenderer>();
        float progress = 0;
        for (int i = 0; i < 2; i++)
        {
            progress = 0;
            while (progress < 1)
            {
                rend.color = Color.Lerp(new Color(1, 0, 0, 0), new Color(1, 0, 0, 0.5f), progress);
                progress += Time.deltaTime;
                yield return new WaitForFixedUpdate();
            }
            progress = 0;
            while (progress < 1)
            {
                rend.color = Color.Lerp(new Color(1, 0, 0, 0.5f), new Color(1, 0, 0, 0), progress);
                progress += Time.deltaTime;
                yield return new WaitForFixedUpdate();
            }
        }
        rend.color = new Color(1, 0, 0, 0);
        isInAction = true;
        WarnEnded();
    }

    IEnumerator WarnLineAttack() {
        WarnStarted();
        isInAction = false;
        LineRenderer rend = warnSprite.GetComponent<LineRenderer>();
        float progress = 0;
        for (int i = 0; i < 2; i++)
        {
            progress = 0;
            while (progress < 1)
            {
                rend.endColor = Color.Lerp(new Color(1, 0, 0, 0), new Color(1, 0, 0, 0.5f), progress);
                rend.startColor = Color.Lerp(new Color(1, 0, 0, 0), new Color(1, 0, 0, 0.5f), progress);
                progress += Time.deltaTime;
                yield return new WaitForFixedUpdate();
            }
            progress = 0;
            while (progress < 1)
            {
                rend.endColor = Color.Lerp(new Color(1, 0, 0, 0.5f), new Color(1, 0, 0, 0), progress);
                rend.startColor = Color.Lerp(new Color(1, 0, 0, 0.5f), new Color(1, 0, 0, 0), progress);
                progress += Time.deltaTime;
                yield return new WaitForFixedUpdate();
            }
        }
        rend.startColor = new Color(1, 0, 0, 0);
        rend.endColor = new Color(1, 0, 0, 0);
        isInAction = true;
        WarnEnded();
    }
}
