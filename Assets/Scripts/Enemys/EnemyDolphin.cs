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
    public AudioManager audioManager;
    public DolphinConfig dolphinConfig;
    float xPower = 5.0f;
    float yPower = 10.0f;
    float angle;
    bool isFliped = false;
    bool jumpStarted = false;


    float timeResolution =0.02f;
    float maxTime = 3.0f;

    protected override void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();

        base.LineStart();
        yPower = Random.Range(dolphinConfig.minYPower, dolphinConfig.maxYPower);
        xPower = Random.Range(dolphinConfig.minXPower, dolphinConfig.maxXPower);

        angle = Mathf.Atan2(yPower, xPower) * Mathf.Rad2Deg;        

        if(transform.position.x > 0){
            xPower = -xPower;
            isFliped = true;
            gameObject.transform.localScale = new Vector3(-gameObject.transform.localScale.x, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
        }

        setRoute();
    }

    private void Update()
    {
        // 시스템에 무리를 주려나?
        setRoute();

        if(jumpStarted)
        {
            if(!isFliped)
            {
                transform.Rotate(new Vector3(0, 0, -60f * Time.deltaTime));
            }
            else
            {
                transform.Rotate(new Vector3(0, 0, 60f * Time.deltaTime));
            }
            
        }
    }

    override protected void WarnStarted(){
        gameObject.GetComponent<Rigidbody2D>().simulated = false;
        gameObject.GetComponent<Collider2D>().enabled = false;
    }
    
    override protected void WarnEnded(){
        gameObject.GetComponent<Rigidbody2D>().simulated = true;
        gameObject.GetComponent<Collider2D>().enabled = true;
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(xPower, yPower);

        jumpStarted = true;
        if(isFliped)
            transform.rotation = Quaternion.Euler(0, 0, -angle);
        else
            transform.rotation = Quaternion.Euler(0, 0, angle);
        audioManager.Play("Dolphin");
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
