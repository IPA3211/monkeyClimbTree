using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenUI : MonoBehaviour
{
    public GameObject background;
    public float openTweenTime;
    public float closeTweenTime;
    public int openedUIs = 0;
    
    public HashSet<GameObject> opend;

    // Start is called before the first frame update
    // Update is called once per frame
    
    void Start(){
        opend = new HashSet<GameObject>();
    }
    void Update()
    {
        openedUIs = opend.Count;
    }

    public void OpenTween(GameObject other)
    {
        if(other.name.Equals("SkinUI")){
            GameSystem.warnSkin = false;
        }
        else if(other.name.Equals("EndingUI")){
            GameSystem.warnEnding = false;
        }
        else if(other.name.Equals("AchievementUI")){
            GameSystem.warnAchieve = false;
        }

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
        if (opend.Count == 0)
        {
            background.SetActive(false);
        }
        if (other.name.Equals("AchievementUI"))
        {
            GameSystem.warnAchieve = false;
        }
        other.transform.LeanScale(Vector3.zero, closeTweenTime).setEaseInBack();
        //LeanTween.scale(gameObject, new Vector3(0, 0, 0), closeTweenTime).setEasePunch();
    }

    public void OpenPauseTween(GameObject other)
    {
        other.transform.localScale = new Vector3(1, 1, 1);
        other.transform.localPosition = new Vector3(other.transform.localPosition.x, 909f, other.transform.localPosition.z);
        other.transform.LeanMoveLocalY(0, openTweenTime).setEaseOutBack();
        opend.Add(other);
        if(opend.Count >= 1){
            background.SetActive(true);
        }
    }

    public void ClosePauseTween(GameObject other)
    {        
        other.transform.LeanMoveLocalY(909f, closeTweenTime).setEaseInBack();
        opend.Remove(other);
        if (opend.Count == 0)
        {
            background.SetActive(false);
        }
    }
    
}
