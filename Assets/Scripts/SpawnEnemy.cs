using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject enemy;
    public float spawnOffset;
    float spawnedAmount = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
