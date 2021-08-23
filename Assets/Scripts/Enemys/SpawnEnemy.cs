using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject enemy;
    public float spawnOffset;
    public float spawnPeriod;
    float timeCount;
    WallPositionSetter wallPositionSetter;
    float spawnedAmount = 0;

    // Start is called before the first frame update
    void Start()
    {
        wallPositionSetter = gameObject.GetComponent<WallPositionSetter>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timeCount += Time.deltaTime;
        if(timeCount > spawnPeriod){
            timeCount = 0;
            wallPositionSetter.wallOnCam.GetComponent<EnemyWall>().spawn(0);
        }

        if(spawnedAmount < GameSystem.playerHeight / spawnOffset){
            if(Random.Range(0f, 1f) > 0.5f){
                Instantiate(enemy, new Vector3(5.11f, GameSystem.playerHeight + 20 , 0), Quaternion.identity);
            }
            else
                Instantiate(enemy, new Vector3(-5.11f, GameSystem.playerHeight + 20 , 0), Quaternion.identity);
            spawnedAmount++;
        }
    }

    
}
