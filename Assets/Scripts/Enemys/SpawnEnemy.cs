using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class enemyPrefabs{
    public int numEnemy = 4;
    public GameObject snake;
    public GameObject panzee;
    public GameObject apple;
    public GameObject eagle;
}
public class SpawnEnemy : MonoBehaviour
{   
    [Header ("Config")]
    public AudioManager audioManager;
    public enemyPrefabs enemys;
    public EnemyLevel enemyLevel;

    float timeCount;
    WallPositionSetter wallPositionSetter;
    float spawnedAmount = 0;
    Transform cam;
    
    delegate void FunctionPointer();
    List<FunctionPointer> onList = new List<FunctionPointer>();

    // Start is called before the first frame update
    void Start()
    {
        wallPositionSetter = gameObject.GetComponent<WallPositionSetter>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(GameSystem.CanTimeCount())
            timeCount += Time.deltaTime;
        else
            timeCount = 0;
        

        if(timeCount > enemyLevel.spawnPeriod && !GameSystem.isLevelUping && !GameSystem.isLeveluped){
            timeCount = 0;

            if(enemyLevel.spawnWall)
                onList.Add(SpawnWall);

            if(enemyLevel.spawnSnake)
                onList.Add(SpawnSnake);
            
            if(enemyLevel.spawnPanzee)
                onList.Add(SpawnPanzee);
            
            if(enemyLevel.spawnApple)
                onList.Add(SpawnApple);
            
            if(enemyLevel.spawnEagle)
                onList.Add(SpawnEagle);

            int amount = Random.Range(enemyLevel.minSpawnAmount, enemyLevel.maxSpawnAmount + 1);
            for(int i = 0; i < amount; i++){
                if(onList.Count == 0)
                    break;
                int randomSpawn = Random.Range(0, onList.Count);
                Debug.Log("2 : " + randomSpawn);
                onList[randomSpawn]();
                onList.RemoveAt(randomSpawn);
            }

            onList.Clear();
        }
    }

    public void SpawnWall(){
        wallPositionSetter.wallOnCam.GetComponent<EnemyWall>().spawn(enemyLevel.wallAmount);
    }
    public void SpawnSnake(){ SpawnSnake(0);}

    public void SpawnSnake(int spawnPoint){
        if(spawnPoint == 0)
            spawnPoint = Random.Range(1, 5);

        enemys.snake.GetComponent<EnemySnake>().speed = enemyLevel.snakeSpeed;
        Debug.Log(spawnPoint);
        
        switch(spawnPoint){
            case 1:
                Instantiate(enemys.snake, new Vector3(5.1f, cam.position.y + 11.25f, 0), Quaternion.Euler(0, 0, 180));
            break;
            case 2:
                Instantiate(enemys.snake, new Vector3(-5.1f, cam.position.y + 11.25f, 0), Quaternion.Euler(0, 180, 180));
            break;
            case 3:
                Instantiate(enemys.snake, new Vector3(5.1f, cam.position.y - 11.25f, 0), Quaternion.Euler(0, 180, 0));
            break;
            case 4:
                Instantiate(enemys.snake, new Vector3(-5.1f, cam.position.y - 11.25f, 0), Quaternion.Euler(0, 0, 0));
            break;
        }

        audioManager.Play("Snake");
    }

    public void SpawnPanzee(){
        enemys.panzee.GetComponent<EnemyPanzee>().XPower = enemyLevel.panzeeXPower;
        enemys.panzee.GetComponent<EnemyPanzee>().YPower = enemyLevel.panzeeYPower;
        Instantiate(enemys.panzee, new Vector3(Random.Range(-4f, 4f), cam.position.y - 11.25f, 0), Quaternion.Euler(0, 0, 0));
        audioManager.Play("Chimpanzee");
    }

    public void SpawnApple() { SpawnApple(0);}
    public void SpawnApple(int spawnPoint){
        if(spawnPoint == 0)
            spawnPoint = Random.Range(1, 3);

        switch(spawnPoint){
            case 1:
                Instantiate(enemys.apple, new Vector3(4.7f, cam.position.y + 10.5f, 0), Quaternion.Euler(0, 0, 0));
            break;
            case 2:
                Instantiate(enemys.apple, new Vector3(-4.7f, cam.position.y + 10.5f, 0), Quaternion.Euler(0, 0, 0));
            break;
        }
    }

    public void SpawnEagle() { SpawnEagle(0);}
    public void SpawnEagle(int spawnPoint){
         if(spawnPoint == 0)
            spawnPoint = Random.Range(1, 3);

        switch(spawnPoint){
        case 1:
            Instantiate(enemys.eagle, new Vector3(Random.Range(-4f, 4f), cam.position.y + 10.5f, 0), Quaternion.Euler(0, 0, 180));
        break;
        case 2:
            Instantiate(enemys.eagle, new Vector3(Random.Range(-4f, 4f), cam.position.y - 10.5f, 0), Quaternion.Euler(0, 0, 0));
        break;
        }
    }
}