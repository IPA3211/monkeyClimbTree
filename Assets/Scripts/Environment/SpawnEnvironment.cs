using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnEnvironment : MonoBehaviour
{
    public GameObject warningSign;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("EnvironmentWarn");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    IEnumerator EnvironmentWarn() {
        warningSign.SetActive(true);
        RawImage rend = warningSign.GetComponent<RawImage>();
        Color rawColor = rend.material.color;
        float progress = 0;
        
        for (int i = 0; i < 2; i++)
        {
            progress = 0;
            while (progress <= 1)
            {
                rend.color = Color.Lerp(new Color(1, 1, 1, 0), new Color(1, 1, 1, 1), progress);
                progress += 0.02f;
                yield return new WaitForSeconds(0.005f);
            }
            progress = 0;
            while (progress <= 1)
            {
                rend.color = Color.Lerp(new Color(1, 1, 1, 1), new Color(1, 1, 1, 0), progress);
                progress += 0.02f;
                yield return new WaitForSeconds(0.005f);
            }
        }
        
        warningSign.SetActive(false);
    }
}
