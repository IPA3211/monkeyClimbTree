using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCoin : MonoBehaviour
{
    public float LifeTime;
    public float warningTime;
    GameObject player;
    bool magnetic = false;
    float speed = 12f;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine("BranchLifeCycle");
    }

    private void Update()
    {
        if(magnetic)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag.Equals("Magnet") && GameSystem.hasMagnetic)
        {
            magnetic = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Magnet") && GameSystem.hasMagnetic)
        {
            magnetic = false;
        }
    }



    IEnumerator BranchLifeCycle()
    {
        yield return new WaitForSeconds(LifeTime - warningTime);
        SpriteRenderer rend = gameObject.GetComponent<SpriteRenderer>();
        for (int i = 0; i < warningTime * 10; i++)
        {
            rend.enabled = !rend.enabled;
            yield return new WaitForSeconds(0.1f);
        }
        Destroy(gameObject);
    }
}
