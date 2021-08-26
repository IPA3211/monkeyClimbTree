using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowSetting : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Screen.SetResolution(1080, 1920, false);
    }
}
