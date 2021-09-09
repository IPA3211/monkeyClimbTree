using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlane : Enemy
{
    public float speed;
    public GameObject warnIcon;

    // Start is called before the first frame update
    override protected void Start()
    {
        base.Start();

        if (gameObject.transform.position.x < 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
            warnIcon.GetComponent<SpriteRenderer>().flipX = true;
        }

    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if(isInAction){
            transform.transform.Translate(Vector3.up * Time.deltaTime * speed);
        }
    }
}
