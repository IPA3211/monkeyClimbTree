using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCoin : MonoBehaviour
{
    public float LifeTime;
    public float warningTime;

    void Start()
    {
        StartCoroutine("BranchLifeCycle");
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
