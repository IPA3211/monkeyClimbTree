using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ending : MonoBehaviour
{    
    public enum EndingType { Enemy, StageClear, GameStory };
        
    [Header("Ending Config")]
    public bool isUnlocked = false;
    [Tooltip ("표시되는 엔딩의 이름")]
    public string endingName;
    [Tooltip("엔딩 정렬을 위해서 타입 구분")]
    public EndingType type;
    [Multiline (3)]
    [Tooltip("엔딩의 상세한 설명")]
    public string description;
    [Tooltip("엔딩 달성 조건-영어로 표시하기")]
    public string requirement;
    [Tooltip("표시되는 엔딩 썸네일")]
    public Sprite thumbnail;

    public bool UnlockCheck()
    {
        return isUnlocked;
    }
}
