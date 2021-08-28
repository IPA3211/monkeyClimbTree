using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowSetting : MonoBehaviour
{
    // Start is called before the first frame update
    public void Awake()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Screen.SetResolution(Screen.width, Screen.width * 16 / 9, true);
    }
}
