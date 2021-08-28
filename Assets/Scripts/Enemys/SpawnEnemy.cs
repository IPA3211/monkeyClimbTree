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
    public bool spawnSnake;
    public GameObject snake;
    public float snakeSpeed;
    
    [Space(10f)]
    public bool spawnPanzee;
    public GameObject panzee;
    public float panzeeXPower;
    public float panzeeYPower;

    [Space(10f)]
    public bool spawnApple;
    public GameObject apple;

    [Space(10f)]
    public bool spawnEagle;
    public GameObject eagle;


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
        if(GameSystem.isStarted && !GameSystem.isDead)
            timeCount += Time.deltaTime;
        else
            timeCount = 0;
        

        if(timeCount > spawnPeriod && !GameSystem.isLevelUping && !GameSystem.isLeveluped){
            timeCount = 0;
            if(spawnWall)
                wallPositionSetter.wallOnCam.GetComponent<EnemyWall>().spawn(wallAmount);
            
            if(spawnSnake)
                SpawnSnake(0);
            
            if(spawnPanzee)
                SpawnPanzee();
            
            if(spawnApple)
                SpawnApple(0);
            
            if(spawnEagle)
                SpawnEagle(0);
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
            break;
            case 2:
                Instantiate(snake, new Vector3(-5.1f, cam.position.y + 11.25f, 0), Quaternion.Euler(0, 180, 180));
            break;
            case 3:
                Instantiate(snake, new Vector3(5.1f, cam.position.y - 11.25f, 0), Quaternion.Euler(0, 180, 0));
            break;
            case 4:
                Instantiate(snake, new Vector3(-5.1f, cam.position.y - 11.25f, 0), Quaternion.Euler(0, 0, 0));
            break;
        }

        audioManager.Play("Snake");
    }

    public void SpawnPanzee(){
        panzee.GetComponent<EnemyPanzee>().XPower = panzeeXPower;
        panzee.GetComponent<EnemyPanzee>().YPower = panzeeYPower;
        Instantiate(panzee, new Vector3(Random.Range(-4f, 4f), cam.position.y - 11.25f, 0), Quaternion.Euler(0, 0, 0));
        audioManager.Play("Chimpanzee");
    }

    public void SpawnApple(int spawnPoint){
        if(spawnPoint == 0)
            spawnPoint = Random.Range(1, 3);

        switch(spawnPoint){
            case 1:
                Instantiate(apple, new Vector3(4.7f, cam.position.y + 10.5f, 0), Quaternion.Euler(0, 0, 0));
            break;
            case 2:
                Instantiate(apple, new Vector3(-4.7f, cam.position.y + 10.5f, 0), Quaternion.Euler(0, 0, 0));
            break;
        }
    }

    public void SpawnEagle(int spawnPoint){
         if(spawnPoint == 0)
            spawnPoint = Random.Range(1, 3);

        switch(spawnPoint){
        case 1:
            Instantiate(eagle, new Vector3(Random.Range(-4f, 4f), cam.position.y + 10.5f, 0), Quaternion.Euler(0, 0, 180));
        break;
        case 2:
            Instantiate(eagle, new Vector3(Random.Range(-4f, 4f), cam.position.y - 10.5f, 0), Quaternion.Euler(0, 0, 0));
        break;
        }
    }
}