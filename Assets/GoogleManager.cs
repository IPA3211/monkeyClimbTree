using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;
public class GoogleManager : MonoBehaviour
{
    static bool isLoaded = false;
    public bool loadingFailed = true;
    public GameObject LoadingUI;
    public GameObject SavingText;
    public Text NetStatusText;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        if(!isLoaded){
            LoadingUI.SetActive(true);
            PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().EnableSavedGames().Build();
            PlayGamesPlatform.InitializeInstance(config);

            PlayGamesPlatform.DebugLogEnabled = false;
            PlayGamesPlatform.Activate();
            isLoaded = true;
            LogIn();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LogIn(){
        Social.localUser.Authenticate((bool success) =>{
            if(success) {
                NetStatusText.text = Social.localUser.id + " " + Social.localUser.userName;
                SecurityPlayerPrefs.SetString("UserID", Social.localUser.id);
                LoadCloud();
            }
            else {
                NetStatusText.text = "failed";
                LoadingCompelete();
            };
        });
    }

    public bool CheckLogin(){
        return Social.localUser.authenticated;
    }

    public void LogOut(){
        ((PlayGamesPlatform)Social.Active).SignOut();
    }

    #region 클라우드 저장

    ISavedGameClient SavedGame() 
    {
        return PlayGamesPlatform.Instance.SavedGame;
    }


    public void LoadCloud() 
	{
        SavedGame().OpenWithAutomaticConflictResolution("mysave",
            DataSource.ReadCacheOrNetwork, ConflictResolutionStrategy.UseLastKnownGood, LoadGame);
    }

    void LoadGame(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        if (status == SavedGameRequestStatus.Success)
            SavedGame().ReadBinaryData(game, LoadData);
    }

    void LoadData(SavedGameRequestStatus status, byte[] LoadedData) 
    {
        if (status == SavedGameRequestStatus.Success) 
        {
            try
            {
                string data = System.Text.Encoding.UTF8.GetString(LoadedData);
                JsonManager.LoadDataFromString(data);
                NetStatusText.text = "로드 성공";
                LoadingCompelete();
                loadingFailed = false;
            }
            catch (System.Exception)
            {
                NetStatusText.text = "로드 실패 : 서버에 저장된 데이터가 없습니다.";
                LoadingCompelete();
                throw;
            }
            
        }
        else {
            NetStatusText.text = "로드 실패";
            LoadingCompelete();
        }
    }

    void LoadingCompelete(){
        SceneManager.LoadScene("SampleScene");
        LoadingUI.transform.LeanScale(Vector3.zero, 1f).setEaseInBack();
    }

    public void SaveCloud()
    {
        if(CheckLogin()){
        SavingText.SetActive(true);
        Debug.Log("save start");
        SavedGame().OpenWithAutomaticConflictResolution("mysave",
            DataSource.ReadCacheOrNetwork, ConflictResolutionStrategy.UseLastKnownGood, SaveGame);
        }
    }

    public void SaveGame(SavedGameRequestStatus status, ISavedGameMetadata game) 
    {
        if (status == SavedGameRequestStatus.Success)
        {
            var update = new SavedGameMetadataUpdate.Builder().Build();
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(
                File.ReadAllText(Application.persistentDataPath
                                                    + "/ItemData.json"));
            SavedGame().CommitUpdate(game, update, bytes, SaveData);
        }
    }

    void SaveData(SavedGameRequestStatus status, ISavedGameMetadata game) 
    {
        if (status == SavedGameRequestStatus.Success)
        {
            NetStatusText.text = "저장 성공";
        }
        else NetStatusText.text = "저장 실패";
        
        SavingText.SetActive(false);
    }

    public void DeleteCloud()
    {
        SavedGame().OpenWithAutomaticConflictResolution("mysave", 
            DataSource.ReadCacheOrNetwork, ConflictResolutionStrategy.UseLongestPlaytime, DeleteGame);
    }

    void DeleteGame(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            SavedGame().Delete(game);
            Debug.Log("삭제 성공");
        }
        else Debug.Log("삭제 실패");
    }

    #endregion

    
}
