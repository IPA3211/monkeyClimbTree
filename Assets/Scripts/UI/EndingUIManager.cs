using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EndingUIManager : MonoBehaviour
{
    public GameObject gameManager;
    public GameObject endingPanel;
    public Image endingPicture;
    public Text title;
    public Text description;
    public Sprite frame;
    public Sprite defaultThumbnail;
    public List<GameObject> buttons;
    List<Ending> endings;

    private void Awake()
    {
        endings = gameManager.GetComponent<EndingManager>().endings;
    }

    // Start is called before the first frame update
    void Start()
    {        
        endingPanel.transform.localScale = Vector3.zero;
        SettingButtons();
    }

    // Update is called once per frame
    void Update()
    {
        SettingButtons();
    }

    public void SettingButtons()
    {
        for(int i=0; i<endings.Count; i++)
        {
            if (i >= buttons.Count)
                return;
            if (endings[i].CheckUnlock())
            {
                buttons[i].transform.GetChild(0).GetComponent<Image>().sprite = endings[i].thumbnails[0];
            }                
            else
            {
                buttons[i].transform.GetChild(0).GetComponent<Image>().sprite = defaultThumbnail;
            }

            buttons[i].GetComponent<Image>().sprite = frame;
        }
    }

    public void OpenEndingPanel()
    {
        string name = EventSystem.current.currentSelectedGameObject.name;
        int num = int.Parse(name);
        Ending ending = endings[num];

        Debug.Log(num.ToString() + ":" + ending.CheckUnlock().ToString());

        if (!ending.CheckUnlock())
        {
            //buttons[num].transform.LeanMoveX(buttons[num].transform.position.x + 10f, 2f).setEaseInOutBounce();
            return;
        }
        if (ending.type == Ending.EndingType.Enemy)
            title.color = new Color32(253, 144, 145, 255);
        else if(ending.type == Ending.EndingType.StageClear)
            title.color = new Color32(144, 183, 253, 255);
        title.text = ending.endingName;
        description.text = ending.description;
        endingPicture.sprite = ending.thumbnails[0];
        endingPanel.transform.LeanScale(Vector3.one, 0.7f).setEaseOutQuad();
    }

    public void CloseEndingPanel()
    {
        endingPanel.transform.LeanScale(Vector3.zero, 0.7f).setEaseInOutQuad();
    }
}
