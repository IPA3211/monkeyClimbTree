using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Ending
{    
    public enum EndingType { Enemy, StageClear, GameStory };
        
    [Header("Ending Config")]
    private bool isUnlocked = false;
    [Tooltip ("ǥ�õǴ� ������ �̸�")]
    public string endingName;
    [Tooltip("���� ������ ���ؼ� Ÿ�� ����")]
    public EndingType type;
    [TextArea(5,6)]
    [Tooltip("������ ���� ����")]
    public string description;
    [Tooltip("���� �޼� ����-����� ǥ���ϱ�")]
    public string requirement;
    [Tooltip("ǥ�õǴ� ���� �����")]
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
