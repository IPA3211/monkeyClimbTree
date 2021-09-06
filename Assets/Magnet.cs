using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    public GameObject player;

    private void FixedUpdate()
    {
        transform.position = player.transform.position;
    }
}
