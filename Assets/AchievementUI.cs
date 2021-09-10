using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementUI : MonoBehaviour
{
    public GameObject dailyPanel, weeklyPanel;
    public GameObject achieveUI;

    public GameObject makeDailyNode(){
        return Instantiate(achieveUI, dailyPanel.transform);
    }
}
