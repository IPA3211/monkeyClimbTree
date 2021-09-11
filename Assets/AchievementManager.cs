using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AchievementManager : MonoBehaviour
{
    public List<Achievement> dailyAchievements;
    public List<Achievement> weeklyAchievements;
    public AchievementUI achievementUI;

    public List<Achievement> activedDailyAchievements;
    public List<Achievement> activedWeeklyAchievements;
    System.DateTime dailyRefreshedTime;
    System.DateTime weeklyRefreshedTime;
    GoogleManager netManager = null;
    // Start is called before the first frame update
    void Start()
    {
        GameObject temp = GameObject.FindWithTag("Network");
        
        if(temp != null){
            netManager = temp.GetComponent<GoogleManager>();
        }
        
        loadActivedAchieve();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void loadActivedAchieve(){
        long tmp = System.Convert.ToInt64(SecurityPlayerPrefs.GetString("DRT", System.DateTime.MinValue.ToBinary().ToString()));
		dailyRefreshedTime = System.DateTime.FromBinary(tmp);

        activedDailyAchievements.Clear();

        if(dailyRefreshedTime.AddDays(1) < UnbiasedTime.Instance.Now()){
            renewDailyAchieve();
            return;
        }

        activedDailyAchievements = dailyAchievements.ToList();

        for(int i = 0; i < activedDailyAchievements.Count; i++){
            int temp = SecurityPlayerPrefs.GetInt("Dachieve" + i, 0);
            if(temp == -1){
                activedDailyAchievements[i].setScore(activedDailyAchievements[i].toClear);
                activedDailyAchievements[i].isReceived = true;
            }
            else
                activedDailyAchievements[i].setScore(SecurityPlayerPrefs.GetInt("Dachieve" + i, 0));
        }
        
        makeAchieveNode();
        refreshAchieve();
    }
    
    public void saveActivedAchieve(){
        SecurityPlayerPrefs.SetString("DRT", dailyRefreshedTime.ToBinary().ToString());
        for(int i = 0; i < activedDailyAchievements.Count; i++){
            if(activedDailyAchievements[i].isReceived)
                SecurityPlayerPrefs.SetInt("Dachieve" + i, -1);
            else
                SecurityPlayerPrefs.SetInt("Dachieve" + i, activedDailyAchievements[i].getScore());
        }
        
        if(netManager != null)
            netManager.SaveCloud();
    }
    void makeAchieveNode(){
        for(int i = 0; i < activedDailyAchievements.Count; i++){
            activedDailyAchievements[i].uiNode = achievementUI.makeDailyNode().GetComponent<AchievementNode>();
        }
    }
    void DestroyAchieveNode(){
        for(int i = 0; i < activedDailyAchievements.Count; i++){
            activedDailyAchievements[i].uiNode.Destroy();
        }
    }
    public void refreshAchieve(){
        for(int i = 0; i < activedDailyAchievements.Count; i++){
            activedDailyAchievements[i].refresh();
        }
        
        Counts.clear();
        saveActivedAchieve();
    }

    void renewDailyAchieve(){
        if(UnbiasedTime.Instance.Now().Date.AddHours(5) < UnbiasedTime.Instance.Now()){
            dailyRefreshedTime = UnbiasedTime.Instance.Now().Date.AddHours(5); // 05:00 기준으로 갱신
        }
        else{
            dailyRefreshedTime = UnbiasedTime.Instance.Now().Date.AddHours(5);
            dailyRefreshedTime = dailyRefreshedTime.AddDays(-1);
        }

        activedDailyAchievements = dailyAchievements.ToList();

        makeAchieveNode();
        refreshAchieve();
    }

    void renewWeeklyAchieve(){

    }
}
