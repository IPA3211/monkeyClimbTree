using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgFilterSetter : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject bgFilter;
    public float changeTime = 2;
    SpriteRenderer bgf;
    void Start()
    {
        bgf = bgFilter.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame

    IEnumerator ChangeColor(Color color) {
        float progress = 0;
        while (progress <= 1)
        {
            bgf.color = Color.Lerp(bgf.color, color, progress);
            progress += (Time.unscaledDeltaTime / changeTime);
            yield return new WaitForFixedUpdate();
        }
    }
}
