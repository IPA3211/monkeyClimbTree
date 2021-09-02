using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EagleConfig{
    public float aimmingTime = 2;
    public float rushSpeed = 20;
    public float spawnTime = 1;

}
public class EnemyEagle : Enemy
{
    [Header("Eagle Config")]
    public EagleConfig eagleConfig;
    public GameObject aim;
    AudioManager audioManager;
    GameObject target;
    Vector2 stopPosition;
    Vector2 targetDir;
    bool isOnUpperSide;
    bool isAimfinished = false;
    bool isAttacking = false;
    // Start is called before the first frame update
    override protected void Start()
    {
        base.Start();
        aim.SetActive(false);
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        target = GameObject.FindGameObjectWithTag("Player");

        if(cam.transform.position.y < transform.position.y){
            isOnUpperSide = true;
            gameObject.GetComponent<SpriteRenderer>().flipY = true;
        }
        else
            isOnUpperSide = false;

    }
    // Update is called once per frame
    override protected void FixedUpdate()
    {
        base.FixedUpdate();
        if(isAimfinished){
            
            if(isOnUpperSide)
                gameObject.transform.Translate(-targetDir * Time.deltaTime * eagleConfig.rushSpeed);
            else
                gameObject.transform.Translate(targetDir * Time.deltaTime * eagleConfig.rushSpeed);

            if(!isAttacking)
            {
                audioManager.Play("Eagle");
                isAttacking = true;
            }
        }
    }

    public override string WhatsName()
    {
        return base.WhatsName();
    }

    protected override void WarnEnded()
    {
        StartCoroutine("AimmingCoroutine");
    }

    void Aimming(){
        aim.transform.rotation = Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.up, target.transform.position - transform.position));
    }
    IEnumerator AimmingCoroutine() {
        Vector2 startPos = transform.position + new Vector3(0, 0, 10);
        Vector2 camPos = cam.transform.position + new Vector3(0, 0, 10);
        Vector3 diffPos = startPos - camPos;
        float progress = 0;
        float height = Random.Range(2f, 8f);
        while(progress < 1){
            if(isOnUpperSide)
                transform.position = Vector3.Lerp(startPos, (cam.transform.position + new Vector3(0, 0, 10)) + diffPos + Vector3.down * height, progress);
            else
                transform.position = Vector3.Lerp(startPos, (cam.transform.position + new Vector3(0, 0, 10)) + diffPos + Vector3.up * height, progress);
            
            progress += Time.deltaTime / eagleConfig.spawnTime;
            
            yield return new WaitForFixedUpdate();
        }

        startPos = transform.position;
        camPos = cam.transform.position + new Vector3(0, 0, 10);
        diffPos = startPos - camPos;

        aim.SetActive(true);
        progress = 0;
        while(progress < 1){
            Aimming();
            progress += Time.deltaTime / eagleConfig.aimmingTime;
            transform.position = (cam.transform.position + new Vector3(0, 0, 10)) + diffPos;
            
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
