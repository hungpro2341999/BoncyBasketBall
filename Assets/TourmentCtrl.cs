using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TourmentCtrl : MonoBehaviour
{
    public const string Key_Skins = "Key_Skins";
    public const string Key_Tourment = "Key_Tourmnet";
    public const string Key_Tourment_A = "Key_Tourmnet_A";
    public const string Key_Tourment_B = "Key_Tourmnet_B";
   // public const string Key_Tourment_Player
    public static TourmentCtrl Ins;
    public List<UI_Tourment_Rivial> List_Tourment;
    [SerializeField]
    public List<Rivial> List_Rivial = new List<Rivial>();
    public List<int[]> Result = new List<int[]>();
    public bool isCompleteMatch = false;
    public string CurrTourmentPlayer = "";
    public bool isFinalMatch = false;
    public int RewardForFinallyMatch;
    public Transform Rewrard;
  

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
        PlayerPrefs.DeleteKey(Key_Tourment);
        if (!PlayerPrefs.HasKey(Key_Tourment))
        {

         

            LoadNewTour();

            
          

        }

        for(int i = 0; i < List_Tourment.Count; i++)
        {
            if (List_Tourment[i].KeyRoundCurr.StartsWith("V_1") || List_Tourment[i].KeyRoundCurr.StartsWith("B_V_1"))
            {
                List_Tourment[i].TransSkin.gameObject.SetActive(true);
                
               
              


            }
            else
            {
                List_Tourment[i].TransSkin.gameObject.SetActive(false);
            }
        }
        LoadSkin();
       

       
        LoadMatch(LoadMatchFromFile(Key_Tourment_A));
        LoadMatch(LoadMatchFromFile(Key_Tourment_B));
        if (LoadMatch_0())
        {
            isFinalMatch = false;
            Rivial rivial = new Rivial("V_1", "V_1_0");
            Rivial rivial1 = new Rivial("V_1_3", "V_1_4");
            Rivial rivial2 = new Rivial("B_V_1_0", "B_V_1_1");
            Rivial rivial3 = new Rivial("B_V_1_2", "B_V_1_3");
            List_Rivial.Add(rivial);
            List_Rivial.Add(rivial1);
            List_Rivial.Add(rivial2);
            List_Rivial.Add(rivial3);
            CurrTourmentPlayer = "V_1";
            Rewrard.gameObject.SetActive(false);
        }
        else if (LoadMatch_1())
        {
            isFinalMatch = false;
            Rivial rivial = new Rivial("V_2_0", "V_2_1");
            Rivial rivial1 = new Rivial("B_V_2_0", "B_V_2_1");
            List_Rivial.Add(rivial);
            List_Rivial.Add(rivial1);

            CurrTourmentPlayer = "V_2_0";
            Rewrard.gameObject.SetActive(false);
        }
        else
        {
            isFinalMatch = true;
            Rivial rivial = new Rivial("V_3", "B_V_3");
            CurrTourmentPlayer = "V_3";
            Rewrard.gameObject.SetActive(true);
        }




    }

    public string TourCurrPlayer()
    {
        if (!GetTourmnet("V_1").isNext)
        {
           CurrTourmentPlayer = "V_1";
        }
        else if (GetTourmnet("V_1").isNext)
        {
            CurrTourmentPlayer = "V_2_0";
        }

       
        if (GetTourmnet("V_2_0").isNext)
        {
            CurrTourmentPlayer = "V_3";
        }


        return CurrTourmentPlayer;


    }

   
   

   
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            LoadTour();
        }
    }


   public InforRivial SetMatch()
    {
        InforRivial infor = null;
        if(CurrTourmentPlayer == "V_1")
        {
            infor.Skin = GetTourmnet("V_1_0").Skin;

        }
        else if(CurrTourmentPlayer == "V_2_0")
        {
            infor.Skin = GetTourmnet("V_2_1").Skin;

        }
        else
        {
            infor.Skin = GetTourmnet("B_V_3").Skin;
        }
        return infor;
    }


    public void ReflectTour()
    {
        CompleteMatch();
        SaveMatchGlobal();
        TourCurrPlayer();
        List_Rivial = new List<Rivial>();

        if (LoadMatch_0())
        {
            Rewrard.gameObject.SetActive(false);
            isFinalMatch = false;
            Rivial rivial = new Rivial("V_1", "V_1_0");
            Rivial rivial1 = new Rivial("V_1_3", "V_1_4");
            Rivial rivial2 = new Rivial("B_V_1_0", "B_V_1_1");
            Rivial rivial3 = new Rivial("B_V_1_2", "B_V_1_3");
            List_Rivial.Add(rivial);
            List_Rivial.Add(rivial1);
            List_Rivial.Add(rivial2);
            List_Rivial.Add(rivial3);
            CurrTourmentPlayer = "V_1";

        }
        else if (LoadMatch_1())
        {
            Rewrard.gameObject.SetActive(false);
            isFinalMatch = false;
            Rivial rivial = new Rivial("V_2_0", "V_2_1");
            Rivial rivial1 = new Rivial("B_V_2_0", "B_V_2_1");
            List_Rivial.Add(rivial);
            List_Rivial.Add(rivial1);

            CurrTourmentPlayer = "V_2_0";
        }
        else
        {
            Rewrard.gameObject.SetActive(true);
            isFinalMatch = true;
            Rivial rivial = new Rivial("V_3", "B_V_3");
            CurrTourmentPlayer = "V_3";
        }

        LoadMatch(LoadMatchFromFile(Key_Tourment_A));
        LoadMatch(LoadMatchFromFile(Key_Tourment_B));
    }

    public void LoadTour()
    {

        bool isResetTour = false;

        CompleteMatch();
        SaveMatchGlobal();
        TourCurrPlayer();
        List_Rivial = new List<Rivial>();

        if (LoadMatch_0())
        {
            isFinalMatch = false;
            Rivial rivial = new Rivial("V_1", "V_1_0");
            Rivial rivial1 = new Rivial("V_1_3", "V_1_4");
            Rivial rivial2 = new Rivial("B_V_1_0", "B_V_1_1");
            Rivial rivial3 = new Rivial("B_V_1_2", "B_V_1_3");
            List_Rivial.Add(rivial);
            List_Rivial.Add(rivial1);
            List_Rivial.Add(rivial2);
            List_Rivial.Add(rivial3);
            CurrTourmentPlayer = "V_1";
            Rewrard.gameObject.SetActive(false);

        }
        else if (LoadMatch_1())
        {
            Rewrard.gameObject.SetActive(false);
            isFinalMatch = false;
            Rivial rivial = new Rivial("V_2_0", "V_2_1");
            Rivial rivial1 = new Rivial("B_V_2_0", "B_V_2_1");
            List_Rivial.Add(rivial);
            List_Rivial.Add(rivial1);

            CurrTourmentPlayer = "V_2_0";
        } 
        else if(LoadFinalMatch())
        {
            Rewrard.gameObject.SetActive(true);
            isFinalMatch = true;
            Rivial rivial = new Rivial("V_3", "B_V_3");
            CurrTourmentPlayer = "V_3";
        }
        else
        {

            isResetTour = true;

        }
        if (isResetTour)
        {
           
            LoadNewTour();
            LoadSkin();
            for (int i = 0; i < List_Tourment.Count; i++)
            {
                List_Tourment[i].ResetNewTour();
            }
            LoadMatch(LoadMatchFromFile(Key_Tourment_A));
            LoadMatch(LoadMatchFromFile(Key_Tourment_B));
            isFinalMatch = false;
            List_Rivial = new List<Rivial>();
            Rivial rivial = new Rivial("V_1", "V_1_0");
            Rivial rivial1 = new Rivial("V_1_3", "V_1_4");
            Rivial rivial2 = new Rivial("B_V_1_0", "B_V_1_1");
            Rivial rivial3 = new Rivial("B_V_1_2", "B_V_1_3");
            List_Rivial.Add(rivial);
            List_Rivial.Add(rivial1);
            List_Rivial.Add(rivial2);
            List_Rivial.Add(rivial3);
            CurrTourmentPlayer = "V_1";
            SaveMatchGlobal();
            Rewrard.gameObject.SetActive(false);

        }
        else
        {
            LoadMatch(LoadMatchFromFile(Key_Tourment_A));
            LoadMatch(LoadMatchFromFile(Key_Tourment_B));
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
    public void LoadSkin()
    {
        var a = JsonUtility.FromJson<Skins>(PlayerPrefs.GetString(Key_Skins)).SKins;
        Debug.Log("Player : " + a.Count);
      
           
      


        var a0 = GetTourmnet("V_1");
        var a1 = GetTourmnet("V_1_0");
        var a2 = GetTourmnet("V_1_3");
        var a3 = GetTourmnet("V_1_4");
        var a4 = GetTourmnet("B_V_1_0");
        var a5 = GetTourmnet("B_V_1_1");
        var a6 = GetTourmnet("B_V_1_2");
        var a7 = GetTourmnet("B_V_1_3");
        a0.Skin = a[0].skin;
        a1.Skin = a[1].skin;
        a2.Skin = a[2].skin;
        a3.Skin = a[3].skin;
        a4.Skin = a[4].skin;
        a5.Skin = a[5].skin;
        a6.Skin = a[6].skin;
        a7.Skin = a[7].skin;

       
        a0.ApplyGraphics();
        a1.ApplyGraphics();
        a2.ApplyGraphics();
        a3.ApplyGraphics();
        a4.ApplyGraphics();
        a5.ApplyGraphics();
        a6.ApplyGraphics();
        a7.ApplyGraphics();

        // Player

        
      


    }

     public void LoadNewTour()
    {
     

        int[] SKin1 = UI_Tourment_Rivial.RandomSKin();
        int[] SKin2 = UI_Tourment_Rivial.RandomSKin();
        int[] SKin3 = UI_Tourment_Rivial.RandomSKin();
        int[] SKin4 = UI_Tourment_Rivial.RandomSKin();
        int[] SKin5 = UI_Tourment_Rivial.RandomSKin();
        int[] SKin6 = UI_Tourment_Rivial.RandomSKin();
        int[] SKin7 = UI_Tourment_Rivial.RandomSKin();
        int[] SKin8 = UI_Tourment_Rivial.RandomSKin();

        Skin Skin1 = new Skin(SKin1);
        Skin Skin2 = new Skin(SKin2);
        Skin Skin3 = new Skin(SKin3);
        Skin Skin4 = new Skin(SKin4);
        Skin Skin5 = new Skin(SKin5);
        Skin Skin6 = new Skin(SKin6);
        Skin Skin7 = new Skin(SKin7);
        Skin Skin8 = new Skin(SKin8);
      
        List<Skin> SKins = new List<Skin>();
        SKins.Add(Skin1);
        SKins.Add(Skin2);
        SKins.Add(Skin3);
        SKins.Add(Skin4);
        SKins.Add(Skin5);
        SKins.Add(Skin6);
        SKins.Add(Skin7);
        SKins.Add(Skin8);


        Skins ListSins = new Skins(SKins);
      
        string json = JsonUtility.ToJson(ListSins);

        PlayerPrefs.SetString(Key_Skins, json);
        PlayerPrefs.Save();


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

        string json2 = JsonUtility.ToJson(global);

        PlayerPrefs.SetString(Key_Tourment_A, json2);
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

    public void ResultMatch(Match match)
    {
        string a = match.resultP1.ToString();
        string b = match.resultP2.ToString();
     //   Debug.Log(a + "  " + b + "  " + match.P1 + "  " + match.P2);
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

    public bool LoadMatch_0()
    {
        if((!GetTourmnet("V_1").isNext && !GetTourmnet("V_1_0").isNext))
        {
            return true;
        }
        return false;
    }

    public bool LoadMatch_1()
    {
        if (!GetTourmnet("V_2_0").isNext && !GetTourmnet("V_2_1").isNext)
        {
            return true;
        }

        return false;
    }

    public bool LoadFinalMatch()
    {
        if (!GetTourmnet("V_3").isNext && !GetTourmnet("B_V_3").isNext)
        {
            return true;
        }
        return false;
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

        ///////////////

        Match mb1 = GetMatch("B_V_1_0", "B_V_1_1");
        Match mb2 = GetMatch("B_V_1_2", "B_V_1_3");
        BoardMatch bmb1 = new BoardMatch();
        bmb1.Matchs.Add(mb1);
        bmb1.Matchs.Add(mb2);

        Match mb3 = GetMatch("B_V_2_0", "B_V_2_1");
        BoardMatch bmb2 = new BoardMatch();
        bmb2.Matchs.Add(mb3);



        MatchGlobal mglobalB = new MatchGlobal();
        mglobalB.Matchs.Add(bmb1);
        mglobalB.Matchs.Add(bmb2);
        string jsonb = JsonUtility.ToJson(mglobalB);
        PlayerPrefs.SetString(Key_Tourment_B, jsonb);
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

    public void CompleteMatch()
    {
        for(int i = 0; i < List_Rivial.Count; i++)
        {
             
            Debug.Log("Match L "+List_Rivial[i].P1 + "  " + List_Rivial[i].P2);

         //   if (!isMatchPlayer(List_Rivial[i]))
///{
                if (Random.Range(0, 2) == 0)
                {
                    GetTourmnet(List_Rivial[i].P1).isNext = true;
                }
                else
                {
                    GetTourmnet(List_Rivial[i].P2).isNext = true;
                }



        //    }




        }
       



    }




    public bool isMatchPlayer(Rivial rivial)
    {
        var rivial2 = GetTourmnet(CurrTourmentPlayer).MatchRivial;
        if (rivial.P1 == CurrTourmentPlayer || rivial.P2 == CurrTourmentPlayer || rivial.P1 == rivial2 || rivial.P2 == rivial2)
        {
            return true;
        }
        return false;
    }

    public void Play()
    {
        VsScreen.isMatchRandom = false;
     
        VsScreen.SkinUse =  TourmentCtrl.Ins.GetTourmnet(TourmentCtrl.Ins.GetTourmnet(TourmentCtrl.Ins.TourCurrPlayer()).MatchRivial).Skin;
        for(int i=0;i < VsScreen.SkinUse.Length; i++)
        {
            Debug.Log("IDDDD : " + VsScreen.SkinUse[i]);
        }
        GameMananger.Ins.OpenScreen(TypeScreen.Vs);
      
       
    }
    public void SetMatchPlayer()
    {

        if (!TourmentCtrl.Ins.isFinalMatchTour())
        {
            GetTourmnet(TourCurrPlayer()).isNext = true;
            TourmentCtrl.Ins.GetTourmnet(GetTourmnet(TourCurrPlayer()).MatchRivial).isNext = false;
        }
           
      
    }

    public bool isFinalMatchTour()
    {
        if (List_Rivial.Count == 2)
        {
            return true;
        }
        return false;
    }

}


[System.Serializable]
public class Rivial
{
    public Rivial(string P1,string P2)
    {
        this.P1 = P1;
        this.P2 = P2;
    }
    public string P1;
    public string P2;
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
public class Skin
{
    public int[] skin;
    public Skin(int[] skin)
    {
        this.skin = skin;
    }
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

public class InforRivial
{
    public int[] Skin;
    public string name  = "AI";
}



[System.Serializable]
public class Skins
{
    public Skins(List<Skin> SKins)
    {
        this.SKins = SKins;
    }
    public List<Skin> SKins = new List<Skin>();
}
