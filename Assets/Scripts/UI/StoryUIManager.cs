using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StoryUIManager : MonoBehaviour
{
    public GameObject storyUI;
    public Image displayImg;
    public Sprite[] openingSprites;
    public Sprite[] closingSprites;
    bool isOpening;
    bool isClosingStart = false;
    bool hasClosingRead = false;
    int index = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (SecurityPlayerPrefs.GetInt("hasOpeningRead", 0) == 0)
        {
            DisplayStoryUI(true);
        }

        hasClosingRead = (SecurityPlayerPrefs.GetInt("hasClosingRead", 0) == 0 ? false : true);        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Opening(bool isFullStory)
    {
        if (index == openingSprites.Length || isClosingStart == true)
        {
            if (isFullStory)
            {
                if(isClosingStart == false)
                    index = 0;
                isClosingStart = true;
                Closing();
            }
            else
            {
                storyUI.SetActive(false);
                SecurityPlayerPrefs.SetInt("hasOpeningRead", 1);
            }
        }
        else
        {
            displayImg.sprite = openingSprites[index];
        }
    }

    public void Closing()
    {
        if (index == closingSprites.Length)
        {
            isClosingStart = false;
            SecurityPlayerPrefs.SetInt("hasClosingRead", 1);

            if (isOpening == false)
            {
                GameSystem.restart();
                SceneManager.LoadScene("SampleScene");
            }
            else
            {
                storyUI.SetActive(false);
            }
            
        }
        else
        {
            displayImg.sprite = closingSprites[index];
        }
    }

    public void DisplayStoryUI(bool opening)
    {
        index = 0;
        isOpening = opening;
        if (isOpening && !hasClosingRead)
        {
            Opening(false);
        }
        else if (isOpening && hasClosingRead)
        {
            Opening(true);
        }
        else if (!isOpening)
        {
            Closing();
        }

        storyUI.SetActive(true);
    }

    public void nextBtnOnClick()
    {
        index++;
        if (isOpening && !hasClosingRead)
        {
            Opening(false);
        }
        else if (isOpening && hasClosingRead)
        {
            Opening(true);
        }
        else if (!isOpening)
        {
            Closing();
        }

       
    }
}
