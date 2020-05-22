using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TourmentCtrl : MonoBehaviour
{
    public const string Key_Tourment = "Key_Tourmnet";
    public const string Key_Tourment_A = "Key_Tourmnet_A";
    public const string Key_Tourment_B = "Key_Tourmnet_B";
    public static TourmentCtrl Ins;
    public List<UI_Tourment_Rivial> List_Tourment;
    public List<int[]> Result = new List<int[]>();
    [SerializeField]
  

    private void Awake()
    {
        if (Ins != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Ins = this;
        }
    }

    private void Start()
    {
        Load();
        
    }

    public void Load()
    {
     //   PlayerPrefs.DeleteKey(Key_Tourment);
        if (!PlayerPrefs.HasKey(Key_Tourment))
        {


            Debug.Log( "Match ");
            PlayerPrefs.SetInt(Key_Tourment, 0);
            // A
            Match match1 = new Match("V_1", "V_1_0", 0, 0);
            Match match2 = new Match("V_1_3", "V_1_4", 0, 0);

            BoardMatch boardmatch = new BoardMatch();
            boardmatch.Matchs.Add(match1);
            boardmatch.Matchs.Add(match2);

            Match match3 = new Match("V_2_1", "V_2_0", 0, 0);

            BoardMatch boardmatch1 = new BoardMatch();

            boardmatch1.Matchs.Add(match3);

            MatchGlobal global = new MatchGlobal();
            global.Matchs.Add(boardmatch);
            global.Matchs.Add(boardmatch1);

            string json = JsonUtility.ToJson(global);

            PlayerPrefs.SetString(Key_Tourment_A, json);
            PlayerPrefs.Save();

            ////////////////////////////////////

            Match match4 = new Match("B_V_1_0", "B_V_1_1", 0, 0);
            Match match5 = new Match("B_V_1_2", "B_V_1_3", 0, 0);
            BoardMatch boardmatch3 = new BoardMatch();
            boardmatch3.Matchs.Add(match4);
            boardmatch3.Matchs.Add(match5);


            Match match6 = new Match("B_V_2_0", "B_V_2_1", 0, 0);
            BoardMatch boardmatch4 = new BoardMatch();
            boardmatch4.Matchs.Add(match6);

            MatchGlobal global_1 = new MatchGlobal();
            global_1.Matchs.Add(boardmatch3);
            global_1.Matchs.Add(boardmatch4);

            string json1 = JsonUtility.ToJson(global_1);
            PlayerPrefs.SetString(Key_Tourment_B, json1);
            PlayerPrefs.Save();


        }

        for(int i = 0; i < List_Tourment.Count; i++)
        {
            if (List_Tourment[i].KeyRoundCurr.StartsWith("V_1") || List_Tourment[i].KeyRoundCurr.StartsWith("B_V_1"))
            {
                List_Tourment[i].TransSkin.gameObject.SetActive(true);
                
                List_Tourment[i].SetRandomGraphics();
              


            }
            else
            {
                List_Tourment[i].TransSkin.gameObject.SetActive(false);
            }
        }

        LoadMatch(LoadMatchFromFile(Key_Tourment_A));
        LoadMatch(LoadMatchFromFile(Key_Tourment_B));
    }
   
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("Save");
            SaveMatchGlobal();
        }   
    }
   
    public void SetProcess()
    {
        var a = GetTourmnet("V_1");
        a.runProcess = true;
    }
    public UI_Tourment_Rivial GetLinkTourmnet(string key)
    {
        foreach (UI_Tourment_Rivial tour in List_Tourment)
        {
            if (tour.KeyNextRound == key)
            {

                return tour;
            }
        }
        return null;
    }
    public UI_Tourment_Rivial GetTourmnet(string key)
    {
        foreach (UI_Tourment_Rivial tour in List_Tourment)
        {
            if (tour.KeyRoundCurr == key)
            {

                return tour;
            }
        }
        return null;
    }

   

    public MatchGlobal LoadMatchFromFile(string tour)
    {
        var a = JsonUtility.FromJson<MatchGlobal>(PlayerPrefs.GetString(tour)).Matchs.Count;
        
        return JsonUtility.FromJson<MatchGlobal>(PlayerPrefs.GetString(tour));
    }

    public void LoadMatch( MatchGlobal matchs)
    {
       
        for(int i = 0; i <matchs.Matchs.Count; i++)
        {
            Debug.Log("Start 0 : " + matchs.Matchs.Count);
            Debug.Log("Start 0 : "+ matchs.Matchs[i].Matchs.Count);
            for (int j = 0; j < matchs.Matchs[i].Matchs.Count; j++)
            {
                Debug.Log(matchs.Matchs[i].Matchs + " " + "Match ");
                var a = matchs.Matchs[i].Matchs[j];
                ResultMatch(a);

            }
        }
    }

    public void ResultMatch(Match match)
    {
        string a = match.resultP1.ToString();
        string b = match.resultP2.ToString();
        Debug.Log(a + "  " + b + "  " + match.P1 + "  " + match.P2);
        string r = a + b;
        if (r == "01")
        {
            var c = GetTourmnet(match.P1);
            c.isNext = false;

            var d = GetTourmnet(match.P2);
            d.isNext = true;
        }
        else if (r == "10")
        {
            var c = GetTourmnet(match.P1);
            c.isNext = true;

            var d = GetTourmnet(match.P2);
            d.isNext = false;
        }
        else
        {
            var c = GetTourmnet(match.P1);
            c.isNext = false;

            var d = GetTourmnet(match.P2);
            d.isNext = false;
        }
    }

    public void SaveMatchGlobal()
    {
        Match m1 = GetMatch("V_1", "V_1_0");
        Match m2 = GetMatch("V_1_3", "V_1_4");
        BoardMatch bm1 = new BoardMatch();
        bm1.Matchs.Add(m1);
        bm1.Matchs.Add(m2);

        Match m3 = GetMatch("V_2_0", "V_2_1");
        BoardMatch bm2 = new BoardMatch();
        bm2.Matchs.Add(m3);



       MatchGlobal mglobal = new MatchGlobal();
        mglobal.Matchs.Add(bm1);
        mglobal.Matchs.Add(bm2);
        string json = JsonUtility.ToJson(mglobal);
        PlayerPrefs.SetString(Key_Tourment_A, json);
        PlayerPrefs.Save();


    }

    public Match GetMatch(string key1,string key2)
    {
        var a = GetTourmnet(key1);
        var b = GetTourmnet(key2);

        int c = (a.isNext ? 1 : 0);
        int d = (b.isNext ? 1 : 0);

        return new Match(key1, key2, c, d);
        
    }

   
  
}
[System.Serializable]
public class MatchGlobal
{
    public List<BoardMatch> Matchs = new List<BoardMatch>();

} 

[System.Serializable]
public class BoardMatch
{
    public List<Match> Matchs = new List<Match>();

}






[System.Serializable]
public class Match
{
    public Match(string P1,string P2,int resultP1,int resultP2)
    {
        this.P1 = P1;
        this.P2 = P2;
        this.resultP1 = resultP1;
        this.resultP2 = resultP2;
     }

    public string P1;
    public string P2;
    public int resultP1;
    public int resultP2;
   

    

    


}
