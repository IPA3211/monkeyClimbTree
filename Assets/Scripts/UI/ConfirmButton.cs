using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmButton : MonoBehaviour
{
    Button confirmBtn;
    // Start is called before the first frame update
    void Start()
    {
        confirmBtn = gameObject.GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ConfirmButtonOnClick()
    {
        confirmBtn.interactable = false;
    }

    void InteractableTrue()
    {
        confirmBtn.interactable = true;
    }
}
