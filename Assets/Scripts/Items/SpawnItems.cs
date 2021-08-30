using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItems : MonoBehaviour
{
    [Header("Config")]
    public float spawnPeriod;

    [Header("Items")]
    public GameObject coin;

    [Space(10f)]
    Transform cam;
    float timeCount;

    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (GameSystem.CanTimeCount())
            timeCount += Time.deltaTime;
        else
            timeCount = 0;


        if (timeCount > spawnPeriod && !GameSystem.isLevelUping && !GameSystem.isLeveluped)
        {
            timeCount = 0;
            SpawnCoin();
        }
    }

    void SpawnCoin()
    {
        Instantiate(coin, new Vector3(Random.Range(-4.5f, 4.5f), Random.Range(-8f,  9f) + cam.position.y, 0), Quaternion.Euler(0, 0, 0));
    }
}
