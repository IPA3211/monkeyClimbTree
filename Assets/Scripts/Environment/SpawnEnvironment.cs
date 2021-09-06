using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class EnviPrefabs{
    public GameObject branch;
    public GameObject bush;
}
public class SpawnEnvironment : MonoBehaviour
{
    [Header ("Config")]
    public GameObject warningSign;
    public EnviPrefabs envis;
    public EnviLevel enviLevel;
    
    Transform cam;
    float lastSpawn = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameSystem.isRestarted){
            lastSpawn = enviLevel.spawnHeight;
        }
        if(GameSystem.playerHeight % enviLevel.spawnHeight < 0.1f && GameSystem.playerHeight > 1f + lastSpawn){
            SpawnBranch(0);
            lastSpawn += enviLevel.spawnHeight;
        }
    }

    public void Spawn(){
        if(!GameSystem.isLevelUping && !GameSystem.isStageCleared)
        {
            StartCoroutine("EnvironmentWarn");

            if(enviLevel.spawnBush)
                StartCoroutine("SpawnBushWithTime", enviLevel.bushNum);
        }
    }

    void SpawnBranch(int spawnPoint){
        if(spawnPoint == 0)
            spawnPoint = Random.Range(1, 3);

        switch(spawnPoint){
            case 1:
                Instantiate(envis.branch, new Vector3(4.0f, cam.position.y + 11, 0), Quaternion.Euler(0, 0, 90));
            break;
            case 2:
                Instantiate(envis.branch, new Vector3(-4.0f, cam.position.y + 11, 0), Quaternion.Euler(0, 0, 90));
            break;
        }
    }

    void SpawnBush(int spawnPoint){
        if(spawnPoint == 0)
            spawnPoint = Random.Range(1, 3);

        switch(spawnPoint){
            case 1:
                Instantiate(envis.bush, new Vector3(4.8f, cam.position.y + Random.Range(0, 10), 0), Quaternion.Euler(0, 0, 0));
            break;
            case 2:
                Instantiate(envis.bush, new Vector3(-4.8f, cam.position.y + Random.Range(0, 10), 0), Quaternion.Euler(0, 0, 0));
            break;
        }
    }

    IEnumerator SpawnBushWithTime(int amount){
        for(int i = 0; i < amount; i++){
            SpawnBush((i % 2) + 1);
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator EnvironmentWarn() {
        warningSign.SetActive(true);
        RawImage rend = warningSign.GetComponent<RawImage>();
        Color rawColor = rend.material.color;
        float progress = 0;
        
        for (int i = 0; i < 3; i++)
        {
            progress = 0;
            while (progress <= 1)
            {
                rend.color = Color.Lerp(new Color(1, 1, 1, 0), new Color(1, 1, 1, 1), progress);
                progress += Time.deltaTime;
                yield return new WaitForFixedUpdate();
            }
            progress = 0;
            while (progress <= 1)
            {
                rend.color = Color.Lerp(new Color(1, 1, 1, 1), new Color(1, 1, 1, 0), progress);
                progress += Time.deltaTime;
                yield return new WaitForFixedUpdate();
            }
        }
        
        warningSign.SetActive(false);
    }
}
