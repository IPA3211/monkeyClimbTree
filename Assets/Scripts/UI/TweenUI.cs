using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenUI : MonoBehaviour
{
    public float openTweenTime;
    public float closeTweenTime;

    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenTween()
    {
        transform.LeanScale(Vector3.one, openTweenTime).setEaseOutQuad();
        //LeanTween.scale(gameObject, new Vector3(1.2f, 1.2f, 1.2f), openTweenTime).setEasePunch();
    }

    public void CloseTween()
    {
        transform.LeanScale(Vector3.zero, closeTweenTime).setEaseInBack();
        //LeanTween.scale(gameObject, new Vector3(0, 0, 0), closeTweenTime).setEasePunch();
    }
}
