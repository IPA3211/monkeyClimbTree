using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GambleUIManager : MonoBehaviour
{
    public GameObject gameManager;    
    public GameObject boxImage1;    // 나뉘는 박스
    public GameObject boxImage2;    // 합쳐진 박스
    public GameObject boxCover;
    public Image skinPreview;
    public Button buyBtn;
    public Button confirmBtn;
    public Text titleText;
    public Text gradeText;
    public Text moneyText;
    public Text previewName;
    Text buyText;
    
    GoogleManager netManager = null;
    List<Skin> skinList;
    ObjReskin skinManager;
    Skin skinEarned;

    public float shakeAmount = 4000f;
    int gamblePrice = 100;
    int normalWeight = 60;
    int rareWeight = 30;
    int hardWeight = 10;
    bool isShaking = false;
    bool isPrivewing = false;
    Vector3 defaultBox = new Vector3();
    Vector3 defaultCover = new Vector3();

    // Start is called before the first frame update
    void Start()
    {
        defaultBox = boxImage1.transform.localPosition;
        defaultCover = boxCover.transform.localPosition;
        buyText = buyBtn.gameObject.transform.GetChild(0).GetComponent<Text>();
        skinManager = gameManager.GetComponent<ObjReskin>();
        skinList = skinManager.skinDatas;

        GameObject temp = GameObject.FindWithTag("Network");

        if (temp != null)
        {
            netManager = temp.GetComponent<GoogleManager>();
        }
        else
            JsonManager.Load();

        if (netManager != null && netManager.loadingFailed)
        {
            JsonManager.Load();
            // 네트워크에서 불러오는데에 실패했을 때
            if (netManager.CheckLogin())
            {
                // 네트워크에 연결된 상태라면
                if (SecurityPlayerPrefs.GetString("UserID", "").Equals(Social.localUser.id) || SecurityPlayerPrefs.GetString("UserID", "").Equals(""))
                    // 현재 네트워크에 연결된 ID 와 로컬에 있는 ID 가 같거나 UserID 가 적혀있지 않다면
                    netManager.SaveCloud();
                else
                {
                    // 로컬에 적혀있는 ID 가 현재 연결된 ID 와 다르다면
                    SecurityPlayerPrefs.DeleteAll();
                    SecurityPlayerPrefs.SetString("UserID", Social.localUser.id);
                }
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        moneyText.text = GameSystem.getCoin().ToString();
        if (GameSystem.getCoin() < gamblePrice)
        {
            buyText.color = Color.red;
            buyBtn.interactable = false;
        }            
        else
            buyText.color = new Color(0.2f, 0.2f, 0.2f);


        if (isShaking)
        {
            Vector3 newPos = Random.insideUnitSphere * (Time.deltaTime * shakeAmount);
            newPos.y = boxImage1.transform.localPosition.y;
            newPos.z = boxImage1.transform.localPosition.z;

            boxImage1.transform.localPosition = newPos;
            boxImage2.transform.localPosition = newPos;
        }
    }

    public void ResetGambleUI()
    {
        buyBtn.interactable = true;
        boxImage1.SetActive(true);
        boxImage2.SetActive(true);
        boxImage1.transform.localPosition = defaultBox;
        boxImage2.transform.localPosition = defaultBox;        
        boxCover.transform.localPosition = defaultCover;
        boxCover.transform.localRotation = Quaternion.identity;
        titleText.gameObject.SetActive(true);
        gradeText.gameObject.SetActive(false);
        skinPreview.gameObject.SetActive(false);
        previewName.gameObject.SetActive(false);

        boxImage1.transform.LeanMoveLocalY(55f, 1f).setEaseOutBounce();
        boxImage2.transform.LeanMoveLocalY(55f, 1f).setEaseOutBounce();
    }

    public void ConfirmBtnOnClick()
    {
        isPrivewing = false;
    }

    public void GambleBtnOnClick()
    {
        GameSystem.addCoin(-gamblePrice);
        SecurityPlayerPrefs.SetInt("Coin", GameSystem.getCoin());

        if (!isPrivewing)
        {
            GambleSkin();
            StartCoroutine("GambleEffect");            
        }
        else
        {
            isPrivewing = false;
            LeanTween.cancelAll();
            ResetGambleUI();
            StartCoroutine("WaitForAfterGamble");            
        }        
    }

    public void MoneyBtnOnClick()
    {
        ResetGambleUI();
    }

    IEnumerator WaitForAfterGamble()
    {
        buyBtn.gameObject.SetActive(false);
        confirmBtn.gameObject.SetActive(false);
        titleText.gameObject.SetActive(false);

        GambleSkin();
        yield return new WaitForSeconds(1.25f);
        
        StartCoroutine("GambleEffect");
    }

    IEnumerator GambleEffect()
    {
        Sprite[] sprites = null;
        buyBtn.gameObject.SetActive(false);
        confirmBtn.gameObject.SetActive(false);
        titleText.gameObject.SetActive(false);

        Vector3 originalPos = boxImage1.transform.localPosition;
        if(isShaking == false)
            isShaking = true;

        yield return new WaitForSeconds(0.75f);

        isShaking = false;
        boxImage1.transform.localPosition = originalPos;
        boxImage2.transform.localPosition = originalPos;

        boxImage2.SetActive(false);
        boxCover.transform.LeanMoveLocal(new Vector3(300, 650, 0), 1.25f).setEaseOutQuint();
        boxCover.transform.LeanRotateAroundLocal(new Vector3(0, 0, 1), -90f, 1.5f).setEaseOutExpo();
        yield return new WaitForSeconds(0.75f);

        boxImage1.SetActive(false);
        confirmBtn.gameObject.SetActive(true);
        buyBtn.gameObject.SetActive(true);
        buyBtn.interactable = true;

        previewName.text = skinEarned.skinName.ToString();
        sprites = Resources.LoadAll<Sprite>("Sprites/Skin/" + skinEarned.texture.name);

        if(skinEarned.rareNum == rare.Normal)
        {
            gradeText.text = "노말 등급";
            gradeText.color = Color.gray;
        }
        else if(skinEarned.rareNum == rare.Rare)
        {
            gradeText.text = "레어 등급!";
            gradeText.color = new Color32(0, 186, 155, 255);
        }
        else if (skinEarned.rareNum == rare.Hard)
        {
            gradeText.text = "전설 등급!?!";
            gradeText.color = new Color32(255, 0, 108, 255);
        }

        skinPreview.sprite = sprites[0];
        skinPreview.transform.localScale = new Vector3(0, 0, 0);
        skinPreview.gameObject.SetActive(true);
        skinPreview.transform.LeanScale(Vector3.one, 2.5f).setEaseOutExpo();
        yield return new WaitForSeconds(1.2f);

        gradeText.gameObject.SetActive(true);
        previewName.gameObject.SetActive(true);

        isPrivewing = true;
        while(isPrivewing)
        {
            for (int i = 0; i < sprites.Length; i++)
            {
                if (!isPrivewing)
                    break;
                skinPreview.sprite = sprites[i];
                yield return new WaitForSeconds(0.3f);
            }
        }

        
    }

    private void GambleSkin()
    {
        int total = 0;
        int randomNum;
        skinEarned = null;
        List<int> weights = new List<int>();
        weights.Add(0);        

        // 가중치 합 구하는 거
        for(int i = 1; i<skinList.Count; i++)
        {
            if(skinList[i].rareNum == rare.Normal)
            {
                weights.Add(normalWeight);
                total += normalWeight;
            }
            else if (skinList[i].rareNum == rare.Rare)
            {
                weights.Add(rareWeight);
                total += rareWeight;
            }
            else if (skinList[i].rareNum == rare.Hard)
            {
                weights.Add(hardWeight);
                total += hardWeight;
            }
        }
        randomNum = Random.Range(0, total);

        // 뭐가 뽑혔는지 확인하는 과정
        for(int i = 1; i<skinList.Count; i++)
        {
            if(randomNum < weights[i])
            {
                skinList[i].isUnlocked = true;
                skinEarned = skinList[i];
                break;
            }
            else
            {
                randomNum -= weights[i];
                continue;
            }            
        }

        skinManager.SaveSkinData();
        if (netManager != null)
            netManager.SaveCloud();
    }
}
