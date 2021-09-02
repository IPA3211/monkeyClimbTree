using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingList : MonoBehaviour
{
    public Sprite defaultThumbnail;
    public Ending[] endings;
    Image thumbnail;

    private void Start()
    {        
        for (int i = 0; i < endings.Length; i++)
        {
            thumbnail = endings[i].transform.GetChild(0).gameObject.GetComponent<Image>();
            //thumbnail = endings[i].GetComponentInChildren<Image>();
            if (endings[i].UnlockCheck() == false)
            {
                thumbnail.sprite = defaultThumbnail;
            }
            else
            {
                thumbnail.sprite = endings[i].thumbnail;
            }
        }
    }

    private void Update()
    {
        
    }

    public void UnlockEnding()
    {
        
    }
}
