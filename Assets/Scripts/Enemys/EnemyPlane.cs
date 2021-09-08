using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlane : Enemy
{
    public float speed;

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if(isInAction){
            transform.transform.Translate(Vector3.up * Time.deltaTime * speed);
        }
    }
}
