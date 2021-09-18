using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GambleUIManager : MonoBehaviour
{
    public GameObject gameManager;    
    public GameObject boxImage1;    // ?????? ???
    public GameObject boxImage2;    // ?????? ???
    public GameObject boxCover;
    public GameObject unfortuneUI;
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

    public float shakeAmount = 1500f;
    int gamblePrice = 100;
    int normalWeight = 50;
    int rareWeight = 35;
    int hardWeight = 15;
    bool isAlreadyEarned = false;
    bool isShaking = false;
    bool isPrivewing = false;
    bool isGambling = false;
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
        buyText.text = gamblePrice.ToString();

        GameObject temp = GameObject.FindWithTag("Network");

        if (temp != null)
        {
            netManager = temp.GetComponent<GoogleManager>();
        }
        else
            JsonManager.Load();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        moneyText.text = GameSystem.getCoin().ToString();
        if (GameSystem.getCoin() < gamblePrice && !GameSystem.hasFreeSkin)
        {
            buyText.text = gamblePrice.ToString();
            buyText.color = Color.red;
            buyBtn.interactable = false;
        }            
        else if(GameSystem.hasFreeSkin){
            buyText.color = new Color32(34, 118, 212, 255);
            buyText.text = "공짜";
        }
        else{
            buyText.text = gamblePrice.ToString();
            buyText.color = new Color(0.2f, 0.2f, 0.2f);
        }


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
        unfortuneUI.SetActive(false);

        boxImage1.transform.LeanMoveLocalY(55f, 1f).setEaseOutBounce();
        boxImage2.transform.LeanMoveLocalY(55f, 1f).setEaseOutBounce();
    }

    public void ConfirmBtnOnClick()
    {
        isPrivewing = false;
    }

    public void GambleBtnOnClick()
    {
        Counts.skinGambleCount++;
        if(!GameSystem.hasFreeSkin){
            GameSystem.addCoin(-gamblePrice);
        }
        else{
            GameSystem.hasFreeSkin = false;
        }
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
        isGambling = true;
        AudioManager.instance.bgm.Pause();
        AudioManager.instance.Play("GambleMusic");
        Sprite[] sprites = null;
        buyBtn.gameObject.SetActive(false);
        confirmBtn.gameObject.SetActive(false);
        titleText.gameObject.SetActive(false);
        unfortuneUI.SetActive(false);

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

        previewName.text = skinEarned.skinName.ToString();
        sprites = Resources.LoadAll<Sprite>("Sprites/Skin/" + skinEarned.texture.name);

        GameSystem.warnSkin = true;

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

        if(isAlreadyEarned){
            gradeText.text = "중복...";
            GameSystem.warnSkin = false;
            gradeText.color = Color.gray;
        }

        skinPreview.sprite = sprites[0];
        skinPreview.transform.localScale = new Vector3(0, 0, 0);
        skinPreview.gameObject.SetActive(true);
        skinPreview.transform.LeanScale(Vector3.one, 2.5f).setEaseOutExpo();
        yield return new WaitForSeconds(1.2f);

        gradeText.gameObject.SetActive(true);
        previewName.gameObject.SetActive(true);

        isPrivewing = true;

        confirmBtn.gameObject.SetActive(true);
        
        buyBtn.gameObject.SetActive(true);
        buyBtn.interactable = true;

        if(isAlreadyEarned){
            unfortuneUI.SetActive(true);
            
            GameSystem.addCoin(20);
            Counts.unfortuneCount += 20;
            SecurityPlayerPrefs.SetInt("Coin", GameSystem.getCoin());
        }
        isGambling = false;
        yield return new WaitForSeconds(1.75f);
        if(!isGambling)
            AudioManager.instance.bgm.UnPause();

        while (isPrivewing)
        {
            for (int i = 0; i < sprites.Length; i++)
            {
                if (!isPrivewing)
                    break;
                skinPreview.sprite = sprites[i];
                yield return new WaitForSeconds(0.3f);
            }
        }
        

        GetComponent<SkinUI>().refreshSkinUI();
    }

    private void GambleSkin()
    {
        int total = 0;
        int randomNum;
        isAlreadyEarned = false;
        skinEarned = null;
        List<int> weights = new List<int>();
        weights.Add(0);        

        // ????? ?? ????? ??
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

        // ???? ???????? ?????? ????
        for(int i = 1; i<skinList.Count; i++)
        {
            if(randomNum < weights[i])
            {
                if(skinList[i].isUnlocked == true){
                    isAlreadyEarned = true;
                }
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
