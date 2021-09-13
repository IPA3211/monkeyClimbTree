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

        for(int i= 0; i < dailyAchievements.Count; i++){
            dailyAchievements[i].isDaily = true;
        }
        
        loadActivedDailyAchieve();
        loadActivedWeeklyAchieve();
        saveActivedAchieve();
        Counts.clear();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void loadActivedDailyAchieve(){
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
        
        makeDailyAchieveNode();
        refreshDailyAchieve();
    }

    void loadActivedWeeklyAchieve(){
        long tmp2 = System.Convert.ToInt64(SecurityPlayerPrefs.GetString("WRT", System.DateTime.MinValue.ToBinary().ToString()));
		weeklyRefreshedTime = System.DateTime.FromBinary(tmp2);

        activedWeeklyAchievements.Clear();

        if(weeklyRefreshedTime.AddDays(7) < UnbiasedTime.Instance.Now()){
            renewWeeklyAchieve();
            return;
        }

        activedWeeklyAchievements = weeklyAchievements.ToList();

        for(int i = 0; i < activedWeeklyAchievements.Count; i++){
            int temp = SecurityPlayerPrefs.GetInt("Wachieve" + i, 0);
            if(temp == -1){
                activedWeeklyAchievements[i].setScore(activedWeeklyAchievements[i].toClear);
                activedWeeklyAchievements[i].isReceived = true;
            }
            else
                activedWeeklyAchievements[i].setScore(SecurityPlayerPrefs.GetInt("Wachieve" + i, 0));
        }
        
        makeWeeklyAchieveNode();
        refreshWeeklyAchieve();
    }
    
    public void saveActivedAchieve(){
        saveDailyActivedAchieve();
        saveWeeklyActivedAchieve();
        
        if(netManager != null)
            netManager.SaveCloud();
    }
    public void saveDailyActivedAchieve(){
        SecurityPlayerPrefs.SetString("DRT", dailyRefreshedTime.ToBinary().ToString());
        for(int i = 0; i < activedDailyAchievements.Count; i++){
            if(activedDailyAchievements[i].isReceived)
                SecurityPlayerPrefs.SetInt("Dachieve" + i, -1);
            else
                SecurityPlayerPrefs.SetInt("Dachieve" + i, activedDailyAchievements[i].getScore());
        }
    }
    public void saveWeeklyActivedAchieve(){
        SecurityPlayerPrefs.SetString("WRT", weeklyRefreshedTime.ToBinary().ToString());
        for(int i = 0; i < activedWeeklyAchievements.Count; i++){
            if(activedWeeklyAchievements[i].isReceived)
                SecurityPlayerPrefs.SetInt("Wachieve" + i, -1);
            else
                SecurityPlayerPrefs.SetInt("Wachieve" + i, activedWeeklyAchievements[i].getScore());
        }
    }

    void makeDailyAchieveNode(){
        for(int i = 0; i < activedDailyAchievements.Count; i++){
            activedDailyAchievements[i].uiNode = achievementUI.makeDailyNode().GetComponent<AchievementNode>();
        }
    }
    void makeWeeklyAchieveNode(){
        for(int i = 0; i < activedWeeklyAchievements.Count; i++){
            activedWeeklyAchievements[i].uiNode = achievementUI.makeWeeklyNode().GetComponent<AchievementNode>();
        }
    }
    void DestroyAchieveNode(){
        for(int i = 0; i < activedDailyAchievements.Count; i++){
            activedDailyAchievements[i].uiNode.Destroy();
        }
        for(int i = 0; i < activedWeeklyAchievements.Count; i++){
            activedWeeklyAchievements[i].uiNode.Destroy();
        }
    }

    public void refreshAchieve(){
        refreshDailyAchieve();
        refreshWeeklyAchieve();

        Counts.clear();
        saveActivedAchieve();
    }
    public void refreshDailyAchieve(){
        for(int i = 0; i < activedDailyAchievements.Count; i++){
            activedDailyAchievements[i].refresh();
        }
    }

    public void refreshWeeklyAchieve(){
        for(int i = 0; i < activedWeeklyAchievements.Count; i++){
            activedWeeklyAchievements[i].refresh();
        }
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

        makeDailyAchieveNode();
        refreshDailyAchieve();
        Counts.logInCount++;
    }

    void renewWeeklyAchieve(){
        System.DateTime temp = UnbiasedTime.Instance.Now().Date.AddHours(5);

        if(temp.DayOfWeek == System.DayOfWeek.Sunday){
            temp.AddDays(-6);
        }
        else{
            temp.AddDays(1 - (int)temp.DayOfWeek);
        }

        weeklyRefreshedTime = temp;

        activedWeeklyAchievements = weeklyAchievements.ToList();

        makeWeeklyAchieveNode();
        refreshWeeklyAchieve();
    }
}
