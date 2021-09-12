using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SatelliteConfig{
    public float aimmingTime = 2;
    public float spawnTime = 1;
    public float shootTime = 0.3f;
    public int shootAmount = 3;

}
public class EnemySatellite : Enemy
{
    public GameObject aim;
    public GameObject shoot;
    public SatelliteConfig config;
    GameObject target;
    Collider2D col;
    SpriteRenderer rend;
    
    protected override void Start(){
        base.Start();
        target = GameObject.FindGameObjectWithTag("Player");
    }
    protected override void WarnEnded()
    {
        base.WarnEnded();
        StartCoroutine("AimmingCoroutine");
    }

    void Aimming(float diffPos){
        transform.position = Vector3.Lerp(new Vector3(transform.position.x, cam.transform.position.y + diffPos, 0),
                                                        new Vector3(target.transform.position.x, cam.transform.position.y + diffPos, 0), 0.1f);

    }

    IEnumerator AimmingCoroutine() {
        Vector2 startPos = transform.position + new Vector3(0, 0, 10);
        Vector2 camPos = cam.transform.position + new Vector3(0, 0, 10);
        Vector3 diffPos = startPos - camPos;
        float progress = 0;
        float height = 3f;
        while(progress < 1){
            transform.position = Vector3.Lerp(startPos, (cam.transform.position + new Vector3(0, 0, 10)) + diffPos + Vector3.down * height, progress);
            
            progress += Time.deltaTime / config.spawnTime;
            
            yield return new WaitForFixedUpdate();
        }

        startPos = transform.position;
        camPos = cam.transform.position + new Vector3(0, 0, 10);
        diffPos = startPos - camPos;

        int temp = 0;
        while(config.shootAmount > temp){
            aim.SetActive(true);
            progress = 0;
            //움직이는 거
            while(progress < 1){
                progress += Time.deltaTime / config.aimmingTime;
                Aimming(diffPos.y);
                yield return new WaitForFixedUpdate();
            }
            aim.SetActive(false);
            
            startPos = transform.position;
            camPos = cam.transform.position + new Vector3(0, 0, 10);
            diffPos = startPos - camPos;            
            progress = 0;

            AudioManager.instance.Play("Satellite");
            // 쏘기 직전에 멈추는거
            while (progress < 0.33f){
                progress += Time.deltaTime;
                transform.position = (cam.transform.position + new Vector3(0, 0, 10)) + diffPos;
                yield return new WaitForFixedUpdate();
            }
                        
            progress = 0;
            GetComponent<Collider2D>().enabled = true;
            shoot.SetActive(true);

            // 쏘는 중인거
            while(progress < config.shootTime){
                progress += Time.deltaTime;
                transform.position = (cam.transform.position + new Vector3(0, 0, 10)) + diffPos;
                
                yield return new WaitForFixedUpdate();
            }
            shoot.SetActive(false);
            GetComponent<Collider2D>().enabled = false;

            temp++;
        }

        // 위로 올라가는 거
        while(progress < 1){
            transform.position = Vector3.Lerp(startPos, (cam.transform.position + new Vector3(0, 0, 10)) + diffPos + Vector3.up * height, progress);
            
            progress += Time.deltaTime / config.spawnTime;
            
            yield return new WaitForFixedUpdate();
        }

        Destroy(gameObject);
    }
}
