using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingManager : MonoBehaviour
{    
    public Sprite defaultThumbnail;
    public List<Ending> endings;
    public 
    int curEndingNum = 0;

    private void Awake()
    {
        for(int i = 0; i<endings.Count; i++)
        {
            if (SecurityPlayerPrefs.GetInt("Ending" + i.ToString(), 0) == 1)
                endings[i].Unlock();            
        }        
    }

    private void Start()
    {        
        /*
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
        */
    }

    private void Update()
    {
        
    }

    public Ending GetEnding()
    {
        return endings[curEndingNum];
    }

    public bool UnlockEnding(string requirement)
    {
        for(int i = 0; i<endings.Count; i++)
        {
            Debug.Log("Name: " + endings[i].requirement + "! " + "requirement: " + requirement + "!!");
            if(endings[i].requirement.Equals(requirement))
            {
                if(endings[i].CheckUnlock() == false)
                {
                    endings[i].Unlock();
                    curEndingNum = i;
                    return true;
                }
                else
                {
                    curEndingNum = i;
                    return false;
                }
            }            
        }

        curEndingNum = 0;
        return false;
    }
}
