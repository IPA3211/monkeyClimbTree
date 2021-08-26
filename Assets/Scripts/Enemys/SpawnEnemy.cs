using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{   
    [Header ("Config")]
    public float spawnPeriod;
    public AudioManager audioManager;

    [Header ("Enemies")]
    public bool spawnWall;
    public int wallAmount;
    [Space(10f)]
    public GameObject snake;
    public float snakeSpeed;
    public bool spawnSnake;
    
    [Space(10f)]
    public GameObject panzee;
    public float panzeeXPower;
    public float panzeeYPower;
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
                wallPositionSetter.wallOnCam.GetComponent<EnemyWall>().spawn(wallAmount);
            
            if(spawnSnake)
                SpawnSnake(0);
            
            if(spawnPanzee)
                SpawnPanzee();
        }
    }

    public void SpawnSnake(int spawnPoint){
        if(spawnPoint == 0)
            spawnPoint = Random.Range(1, 5);

        snake.GetComponent<EnemySnake>().speed = snakeSpeed;
        Debug.Log(spawnPoint);
        switch(spawnPoint){
            case 1:
                Instantiate(snake, new Vector3(5.1f, cam.position.y + 11.25f, 0), Quaternion.Euler(0, 0, 180));
                audioManager.Play("Snake");
            break;
            case 2:
                Instantiate(snake, new Vector3(-5.1f, cam.position.y + 11.25f, 0), Quaternion.Euler(0, 180, 180));
                audioManager.Play("Snake");
            break;
            case 3:
                Instantiate(snake, new Vector3(5.1f, cam.position.y - 11.25f, 0), Quaternion.Euler(0, 180, 0));
                audioManager.Play("Snake");
            break;
            case 4:
                Instantiate(snake, new Vector3(-5.1f, cam.position.y - 11.25f, 0), Quaternion.Euler(0, 0, 0));
                audioManager.Play("Snake");
            break;
        }
    }

    public void SpawnPanzee(){
        panzee.GetComponent<EnemyPanzee>().XPower = panzeeXPower;
        panzee.GetComponent<EnemyPanzee>().YPower = panzeeYPower;
        Instantiate(panzee, new Vector3(Random.Range(-4f, 4f), cam.position.y - 11.25f, 0), Quaternion.Euler(0, 0, 0));
        audioManager.Play("Chimpanzee");
    }
}
