using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    public GameObject player;
    public GameObject icon;
    SpriteRenderer magnet_icon;

    private void Start()
    {
        magnet_icon = icon.GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        transform.position = player.transform.position;

        if (GameSystem.hasMagnetic == true)
        {
            if(!GameSystem.isDead)
            {
                //icon.SetActive(true);
                magnet_icon.color = Color.white;
            }                
        }
        else if (GameSystem.hasMagnetic == false)
        {
            magnet_icon.color = Color.clear;
        }
    }
}
