using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{   
    [Header ("Config")]
    public float spawnPeriod;

    [Header ("Enemies")]
    public bool spawnWall;
    [Space(10f)]

    public GameObject snake;
    public bool spawnSnake;
    [Space(10f)]
    public GameObject panzee;
    public bool spawnPanzee;
    float timeCount;
    WallPositionSetter wallPositionSetter;
    float spawnedAmount = 0;
    Transform cam;

    // Start is called before the first frame update
    void Start()
    {
        wallPositionSetter = gameObject.GetComponent<WallPositionSetter>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timeCount += Time.deltaTime;
        if(timeCount > spawnPeriod && !GameSystem.isLevelUping && !GameSystem.isLeveluped){
            timeCount = 0;
            if(spawnWall)
                wallPositionSetter.wallOnCam.GetComponent<EnemyWall>().spawn(0);
            
            if(spawnSnake)
                SpawnSnake(0);
            
            if(spawnPanzee)
                SpawnPanzee();
        }
    }

    public void SpawnSnake(int spawnPoint){
        if(spawnPoint == 0)
            spawnPoint = Random.Range(1, 5);

        Debug.Log(spawnPoint);
        switch(spawnPoint){
            case 1:
                Instantiate(snake, new Vector3(4.6f, cam.position.y + 11.25f, 0), Quaternion.Euler(0, 0, 180));
            break;
            case 2:
                Instantiate(snake, new Vector3(-4.6f, cam.position.y + 11.25f, 0), Quaternion.Euler(0, 0, 180));
            break;
            case 3:
                Instantiate(snake, new Vector3(4.6f, cam.position.y - 11.25f, 0), Quaternion.Euler(0, 0, 0));
            break;
            case 4:
                Instantiate(snake, new Vector3(-4.6f, cam.position.y - 11.25f, 0), Quaternion.Euler(0, 0, 0));
            break;
        }
    }

    public void SpawnPanzee(){
        Instantiate(panzee, new Vector3(Random.Range(-4f, 4f), cam.position.y - 11.25f, 0), Quaternion.Euler(0, 0, 0));
    }
}
