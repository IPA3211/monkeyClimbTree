using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEagle : Enemy
{
    [Header("Eagle Config")]
    public float aimmingTime;
    public float rushSpeed;
    public float spawnTime = 1;
    public GameObject aim;
    GameObject cam;
    GameObject target;
    Vector2 stopPosition;
    Vector2 targetDir;
    bool isOnUpperSide;
    bool isAimfinished = false;
    // Start is called before the first frame update
    override protected void Start()
    {
        base.Start();
        aim.SetActive(false);
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        target = GameObject.FindGameObjectWithTag("Player");

        if(cam.transform.position.y < transform.position.y)
            isOnUpperSide = true;
        else
            isOnUpperSide = false;

    }
    // Update is called once per frame
    override protected void FixedUpdate()
    {
        if(isAimfinished){
            if(isOnUpperSide)
                gameObject.transform.Translate(-targetDir * Time.deltaTime * rushSpeed);
            else
                gameObject.transform.Translate(targetDir * Time.deltaTime * rushSpeed);
        }
    }

    protected override void WarnEnded()
    {
        base.WarnEnded();
        StartCoroutine("AimmingCoroutine");
    }

    void Aimming(){
        aim.transform.rotation = Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.up, target.transform.position - transform.position));
    }
    IEnumerator AimmingCoroutine() {
        Vector2 startPos = transform.position;
        float progress = 0;
        while(progress < 1){
            if(isOnUpperSide)
                transform.position = Vector3.Lerp(startPos, startPos + Vector2.down * 5, progress);
            else
                transform.position = Vector3.Lerp(startPos, startPos + Vector2.up * 5, progress);
            
            progress += Time.deltaTime / spawnTime;
            
            yield return new WaitForFixedUpdate();
        }

        aim.SetActive(true);
        progress = 0;
        while(progress < 1){
            Aimming();
            progress += Time.deltaTime / aimmingTime;
            
            yield return new WaitForFixedUpdate();
        }
        targetDir = target.transform.position - transform.position;
        targetDir.Normalize();
        
        yield return new WaitForSeconds(0.5f);
        
        aim.SetActive(false);
        isAimfinished = true;
    }
}
