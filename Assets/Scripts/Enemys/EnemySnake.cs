using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySnake : MonoBehaviour
{
    public GameObject warnSprite;
    public float speed;
    bool isInAction = false;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("WarnAttack");
        Destroy(gameObject, 10);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(isInAction){
            gameObject.transform.Translate(Vector3.up * Time.deltaTime * speed);
        }

        if(GameSystem.isDead)
            Destroy(gameObject);
    }

    IEnumerator WarnAttack() {
        SpriteRenderer rend = warnSprite.GetComponent<SpriteRenderer>();
        gameObject.GetComponent<Collider2D>().enabled = false;
        Color rawColor = rend.material.color;
        float progress = 0;
        for (int i = 0; i < 2; i++)
        {
            progress = 0;
            while (progress < 1)
            {
                rend.color = Color.Lerp(new Color(1, 0, 0, 0), new Color(1, 0, 0, 0.5f), progress);
                progress += 0.1f;
                yield return new WaitForSeconds(0.05f);
            }
            progress = 0;
            while (progress < 1)
            {
                rend.color = Color.Lerp(new Color(1, 0, 0, 0.5f), new Color(1, 0, 0, 0), progress);
                progress += 0.1f;
                yield return new WaitForSeconds(0.05f);
            }
        }
        rend.color = new Color(1, 0, 0, 0);
        gameObject.GetComponent<Collider2D>().enabled = true;
        isInAction = true;
    }
}
