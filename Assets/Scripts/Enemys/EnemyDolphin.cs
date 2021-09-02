using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DolphinConfig{
    public float maxYPower = 20.0f;
    public float minYPower = 10.0f;
    public float maxXPower = 10.0f;
    public float minXPower = 4.0f;
}
public class EnemyDolphin : Enemy
{
    // Start is called before the first frame update
    public DolphinConfig dolphinConfig;
    float xPower = 5.0f;
    float yPower = 10.0f;


    float timeResolution =0.02f;
    float maxTime = 3.0f;

    protected override void Start()
    {
        base.LineStart();
        yPower = Random.Range(dolphinConfig.minYPower, dolphinConfig.maxYPower);
        xPower = Random.Range(dolphinConfig.minXPower, dolphinConfig.maxXPower);

        if(transform.position.x > 0){
            xPower = -xPower;
        }

        setRoute();
    }

    override protected void WarnStarted(){
        gameObject.GetComponent<Rigidbody2D>().simulated = false;
        gameObject.GetComponent<Collider2D>().enabled = false;
    }
    
    override protected void WarnEnded(){
        gameObject.GetComponent<Rigidbody2D>().simulated = true;
        gameObject.GetComponent<Collider2D>().enabled = true;
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(xPower, yPower);
    }

    void setRoute(){
        LineRenderer lineRender = warnSprite.GetComponent<LineRenderer>();
        Vector3 velocityVector = transform.up * yPower + transform.right * xPower;
        lineRender.positionCount = (int)(maxTime / timeResolution) + 1;
        int index = 0;
        Vector3 currentPosition = transform.position;

        for (float t = 0.0f; t < maxTime; t += timeResolution) {
            
            lineRender.SetPosition (index, currentPosition);
                
            currentPosition += velocityVector * timeResolution; 
            velocityVector += Physics.gravity * timeResolution;

            index++;
        }
    }
}
