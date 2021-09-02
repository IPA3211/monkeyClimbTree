using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenUI : MonoBehaviour
{
    public GameObject background;
    public float openTweenTime;
    public float closeTweenTime;

    // Start is called before the first frame update
    // Update is called once per frame
    
    void Update()
    {
        
    }

    public void OpenTween(GameObject other)
    {
        background.SetActive(true);
        other.transform.LeanScale(Vector3.one, openTweenTime).setEaseOutQuad();
        other.SetActive(true);
        //LeanTween.scale(gameObject, new Vector3(1.2f, 1.2f, 1.2f), openTweenTime).setEasePunch();
    }

    public void CloseTween(GameObject other)
    {
        background.SetActive(false);
        other.transform.LeanScale(Vector3.zero, closeTweenTime).setEaseInBack();
        //LeanTween.scale(gameObject, new Vector3(0, 0, 0), closeTweenTime).setEasePunch();
    }
}
