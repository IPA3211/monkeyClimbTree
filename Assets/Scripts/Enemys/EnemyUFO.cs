using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UFOConfig{
    public float aimmingTime = 2;
    public float rushSpeed = 20;
    public float spawnTime = 1;

}
public class EnemyUFO : Enemy
{
    public GameObject aim;
    public UFOConfig config;
    GameObject target;
    Vector2 targetDir;
    Collider2D col;
    SpriteRenderer rend;
    bool isAimfinished = false;

    protected override void Start()
    {
        base.Start();
        target = GameObject.FindGameObjectWithTag("Player");
        rend = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        col.enabled = false;
        rend.color = new Color(1,1,1,0);
    }
    override protected void FixedUpdate()
    {
        base.FixedUpdate();
        if(isAimfinished){
            gameObject.transform.Translate(targetDir * Time.deltaTime * config.rushSpeed);
        }
    }
    protected override void WarnEnded()
    {
        StartCoroutine("AimmingCoroutine");
    }
    void Aimming(){
        aim.transform.rotation = Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.up, target.transform.position - transform.position));
    }

    IEnumerator AimmingCoroutine() {
        col.enabled = true;
        rend.color = Color.white;
        Vector2 startPos = transform.position + new Vector3(0, 0, 10);
        Vector2 camPos = cam.transform.position + new Vector3(0, 0, 10);
        Vector3 diffPos = startPos - camPos;
        float progress = 0;
        float height = Random.Range(2f, 8f);
        while(progress < 1){
            transform.position = cam.transform.position + new Vector3(0, 0, 10) + diffPos;
            progress += (Time.deltaTime / config.spawnTime);
            
            yield return new WaitForFixedUpdate();
        }

        startPos = transform.position;
        camPos = cam.transform.position + new Vector3(0, 0, 10);
        diffPos = startPos - camPos;

        aim.SetActive(true);
        progress = 0;
        while(progress < 1){
            Aimming();
            transform.position = (cam.transform.position + new Vector3(0, 0, 10)) + diffPos;
            progress += (Time.deltaTime / config.aimmingTime);
            
            yield return new WaitForFixedUpdate();
        }

        targetDir = target.transform.position - transform.position;
        targetDir.Normalize();
        
        progress = 0;
        while(progress < 0.5){
            progress += Time.deltaTime;
            transform.position = (cam.transform.position + new Vector3(0, 0, 10)) + diffPos;
            
            yield return new WaitForFixedUpdate();
        }
        
        aim.SetActive(false);
        isAimfinished = true;
        gameObject.tag = "Enemy";
    }
}
