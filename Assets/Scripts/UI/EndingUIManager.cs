using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EndingUIManager : MonoBehaviour
{
    public GameObject endingPanel;
    public Text title;
    public Text description;
    public Image picture;
    Ending[] endings;

    // Start is called before the first frame update
    void Start()
    {
        endingPanel.transform.localScale = Vector3.zero;
        endings = GetComponent<EndingList>().endings;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenEndingPanel()
    {
        string name = EventSystem.current.currentSelectedGameObject.name;
        Ending ending = endings[int.Parse(name)];

        // 나중에 ending.isUnlocked가 true인지 체크하고 열어보게 할 것임

        title.text = ending.endingName;
        description.text = ending.description;
        picture.sprite = ending.thumbnail;
        endingPanel.transform.LeanScale(Vector3.one, 0.7f).setEaseOutQuad();
    }

    public void CloseEndingPanel()
    {
        endingPanel.transform.LeanScale(Vector3.zero, 0.7f).setEaseInOutQuad();
    }
}
