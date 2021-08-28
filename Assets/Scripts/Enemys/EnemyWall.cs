using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWall : MonoBehaviour
{
    public List<GameObject> walls;
    private List<GameObject> tempWalls = new List<GameObject>();

    public int amount;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void spawn(int a){
        tempWalls.AddRange(walls);
        if(a != 0){
            amount = a;
        }

        for(int i = 0; i < amount; i++){
            int targetIndex = Random.Range(0, tempWalls.Count);
                StartCoroutine("TreeAttack", tempWalls[targetIndex]);
                tempWalls.Remove(tempWalls[targetIndex]);
        }
        tempWalls.Clear();
    }

    IEnumerator TreeAttack(GameObject tree) {
        SpriteRenderer rend = tree.GetComponent<SpriteRenderer>();
        Color rawColor = rend.material.color;
        float progress = 0;
        for (int i = 0; i < 2; i++)
        {
            progress = 0;
            while (progress <= 1)
            {
                rend.color = Color.Lerp(rawColor, Color.red, progress);
                progress += 0.1f;
                yield return new WaitForSeconds(0.05f);
            }
            progress = 0;
            while (progress <= 1)
            {
                rend.color = Color.Lerp(Color.red, rawColor, progress);
                progress += 0.1f;
                yield return new WaitForSeconds(0.05f);
            }
        }

        yield return new WaitForSeconds(0.5f);

        progress = 0;
        
        Animator tree_anim =  tree.GetComponent<Animator>();

        tree_anim.SetBool("Attacking", true);
        tree.tag = "EnemyWall";
        //rend.color = new Color(0, 0.5f, 1f, 1f);

        yield return new WaitForSeconds(1f);

        rend.color = rawColor;
        tree_anim.SetBool("Attacking", false);
        tree.tag = "Wall";
    }
}
