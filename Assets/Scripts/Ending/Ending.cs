using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Ending
{    
    public enum EndingType { Enemy, StageClear, GameStory };
        
    [Header("Ending Config")]
    private bool isUnlocked = false;
    [Tooltip ("표시되는 엔딩의 이름")]
    public string endingName;
    [Tooltip("엔딩 정렬을 위해서 타입 구분")]
    public EndingType type;
    [TextArea(5,6)]
    [Tooltip("엔딩의 상세한 설명")]
    public string description;
    [Tooltip("엔딩 달성 조건-영어로 표시하기")]
    public string requirement;
    [Tooltip("표시되는 엔딩 썸네일")]
    public Sprite[] thumbnails;

    public bool CheckUnlock()
    {
        return isUnlocked;
    }

    public void Unlock()
    {
        isUnlocked = true;
    }
}
