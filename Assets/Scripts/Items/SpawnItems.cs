using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItems : MonoBehaviour
{
    [Header("Config")]
    public float coinSpawnPeriod;
    public float potionSpawnPeriod;
    public float feverTimePeriod;

    [Header("Items")]
    public GameObject coin;
    public GameObject potion;

    [Space(10f)]
    Transform cam;
    float coinTimeCount;
    float potionTimeCount;
    float feverTimeCount;
    bool isFever = false;
    bool fevering = false;

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
        {
            coinTimeCount += Time.deltaTime;
            potionTimeCount += Time.deltaTime;
            if(isFever)
                feverTimeCount += Time.deltaTime;
        }            
        else
        {
            coinTimeCount = 0;
            potionTimeCount = 0;
            feverTimeCount = 0;
        }            


        if (coinTimeCount > coinSpawnPeriod && !GameSystem.isLevelUping && !GameSystem.isStageCleared && !isFever)
        {
            coinTimeCount = 0;
            SpawnCoin();
        }

        if (potionTimeCount > potionSpawnPeriod && !GameSystem.isLevelUping && !GameSystem.isStageCleared && !isFever)
        {
            potionTimeCount = 0;
            SpawnPotion();
        }

        if (GameSystem.getBanana() >= 3 && !fevering)
        {
            isFever = true;
            StartCoroutine("FeverTime");
            fevering = true;
        }
    }

    void SpawnCoin()
    {
        Instantiate(coin, new Vector3(Random.Range(-4.5f, 4.5f), Random.Range(3f,  9.5f) + cam.position.y, 0), Quaternion.Euler(0, 0, 0));
    }


    void SpawnPotion()
    {
        Instantiate(potion, new Vector3(Random.Range(-4.5f, 4.5f), Random.Range(3f, 9.5f) + cam.position.y, 0), potion.transform.rotation);
    }

    IEnumerator FeverTime()
    {
        Counts.feverCount++;
        for(int i = 0; i< feverTimePeriod * 10; i++)
        {
            SpawnCoin();
            yield return new WaitForSeconds(0.1f);
        }        

        GameSystem.setBanana(0);
        isFever = false;
        fevering = false;
        potionTimeCount = 0;
    }
}
