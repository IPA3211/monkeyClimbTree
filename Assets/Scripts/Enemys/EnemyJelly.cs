using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyJelly : Enemy
{
    public float maxSpeed;
    public float breakingSpeed;
    float velocity;

    protected override void Start()
    {
        base.Start();
        transform.rotation = Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.up, cam.transform.position - transform.position));
    }
    override protected void FixedUpdate(){
        base.FixedUpdate();
        if(isInAction){
            transform.Translate(Vector3.up * velocity * Time.deltaTime);
        }
    }
    override protected void WarnEnded(){
        base.WarnEnded();
        velocity = maxSpeed;
        StartCoroutine("JellyFishMove");
    }
    
    IEnumerator JellyFishMove() {
        if(velocity <= 0){
            velocity = maxSpeed;
        }

        while(velocity > 0){
            velocity -= Time.deltaTime * breakingSpeed;
            yield return new WaitForFixedUpdate();
        }
        
        velocity = 0;
        yield return new WaitForSeconds(0.5f);

        StartCoroutine("JellyFishMove");
    } 
}
