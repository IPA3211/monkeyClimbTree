using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ending : MonoBehaviour
{    
    public enum EndingType { Enemy, StageClear, GameStory };
        
    [Header("Ending Config")]
    public bool isUnlocked = false;
    [Tooltip ("ǥ�õǴ� ������ �̸�")]
    public string endingName;
    [Tooltip("���� ������ ���ؼ� Ÿ�� ����")]
    public EndingType type;
    [Multiline (3)]
    [Tooltip("������ ���� ����")]
    public string description;
    [Tooltip("���� �޼� ����-����� ǥ���ϱ�")]
    public string requirement;
    [Tooltip("ǥ�õǴ� ���� �����")]
    public Sprite thumbnail;

    public bool UnlockCheck()
    {
        return isUnlocked;
    }
}
