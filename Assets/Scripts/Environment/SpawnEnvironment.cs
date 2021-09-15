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
    Color[] colors = { Color.red, Color.magenta, Color.blue, new Color32(0, 255, 255, 255), Color.green, Color.yellow, Color.red };
    
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").transform;
        lastSpawn = enviLevel.spawnHeight;
    }

    void FixedUpdate()
    {
        if(GameSystem.isRestarted){
            lastSpawn = enviLevel.spawnHeight;
        }
        if((GameSystem.playerHeight % enviLevel.spawnHeight < 1f) && (GameSystem.playerHeight > (1f + lastSpawn))){
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
                Instantiate(envis.bush, new Vector3(4.8f, cam.position.y + Random.Range(5, 15), 0), Quaternion.Euler(0, 0, 0));
            break;
            case 2:
                Instantiate(envis.bush, new Vector3(-4.8f, cam.position.y + Random.Range(5, 15), 0), Quaternion.Euler(0, 0, 0));
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
        StartCoroutine("colorChange");

        Image rend = warningSign.GetComponent<Image>();
        float progress = 0;
        float speed = 1.3f;
        warningSign.gameObject.transform.localScale = new Vector3(0, 0, 0);        
        warningSign.gameObject.transform.LeanScale(new Vector3(1, 1, 1), 1.5f).setEaseOutCubic();

        yield return new WaitForSeconds(0.2f);
        AudioManager.instance.Play("LevelUp");
        /*
        for (int i = 0; i < 1; i++)
        {
            progress = 0;
            while (progress <= 1)
            {
                rend.color = Color.Lerp(new Color(rend.color.r, rend.color.g, rend.color.b, 0), new Color32(255, 87, 0, 255), progress);
                progress += (Time.deltaTime * speed);
                yield return new WaitForFixedUpdate();
            }
            progress = 0;
            yield return new WaitForSeconds(1f);
            while (progress <= 1)
            {
                
                rend.color = Color.Lerp(new Color32(255, 87, 0, 255), new Color32(253, 87, 0, 0), progress);
                progress += (Time.deltaTime * speed / 1.5f);
                //if (progress > 1)
                //    rend.color = Color.Lerp(new Color32(253, 251, 0, 0), new Color32(255, 160, 100, 0), progress);
                yield return new WaitForFixedUpdate();
            }
        }
        */
        //rend.color = Color.Lerp(new Color32(255, 160, 100, 40), new Color32(255, 160, 100, 0), 0.3f);
        //yield return new WaitForSeconds(0.3f);
        yield return new WaitForSeconds(2.5f);
        warningSign.gameObject.transform.LeanScale(new Vector3(0, 0, 0), 1.25f).setEaseInOutCubic();
        yield return new WaitForSeconds(1.25f);
        StopCoroutine("colorChange");
        warningSign.SetActive(false);
    }

    IEnumerator colorChange()
    {
        Image rend = warningSign.GetComponent<Image>();
        int colorIdx = 0;
        while (true)
        {            
            for (float i = 0; i <= 1; i+= Time.deltaTime * 3f)
            {
                rend.color = Color.Lerp(colors[colorIdx], colors[colorIdx + 1], i);
                yield return new WaitForFixedUpdate();
            }
            if (colorIdx == colors.Length - 2)
                colorIdx = 0;
            else
                colorIdx++;
        }
        
    }
}
