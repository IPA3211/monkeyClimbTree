using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenUI : MonoBehaviour
{
    public GameObject background;
    public float openTweenTime;
    public float closeTweenTime;
    public int openedUIs = 0;
    HashSet<GameObject> opend;

    // Start is called before the first frame update
    // Update is called once per frame
    
    void Start(){
        opend = new HashSet<GameObject>();
    }
    void Update()
    {
        
    }

    public void OpenTween(GameObject other)
    {
        opend.Add(other);
        if(opend.Count >= 1){
            background.SetActive(true);
        }
        other.transform.LeanScale(Vector3.one, openTweenTime).setEaseOutQuad();
        //LeanTween.scale(gameObject, new Vector3(1.2f, 1.2f, 1.2f), openTweenTime).setEasePunch();
    }

    public void CloseTween(GameObject other)
    {
        opend.Remove(other);
        if(opend.Count == 0){
            background.SetActive(false);
        }
        other.transform.LeanScale(Vector3.zero, closeTweenTime).setEaseInBack();
        //LeanTween.scale(gameObject, new Vector3(0, 0, 0), closeTweenTime).setEasePunch();
    }
}
