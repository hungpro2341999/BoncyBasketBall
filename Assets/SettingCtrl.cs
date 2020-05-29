using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingCtrl : MonoBehaviour
{
    public const string Key_Sound = "Sound";
    public const string Key_Music = "Music";

    public List<Button> ButtonSound = new List<Button>();
    public List<Button> ButtonMusic = new List<Button>();


    public static SettingCtrl Ins;
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
        Init();
    }
    private void Start()
    {
        ApplySetting();
    }
    public void Init()
    {
        if (!PlayerPrefs.HasKey(Key_Sound))
        {
            PlayerPrefs.SetInt(Key_Sound,1);
            PlayerPrefs.Save();

        }

        if (!PlayerPrefs.HasKey(Key_Music))
        {

            PlayerPrefs.SetInt(Key_Music, 1);
            PlayerPrefs.Save();
        }

        


    }

    public void ApplySetting()
    {
        if (isSound())
        {
           
            ButtonSound[0].gameObject.SetActive(true);
            ButtonSound[1].gameObject.SetActive(false);
        }
        else
        {
            ButtonSound[0].gameObject.SetActive(false);
            ButtonSound[1].gameObject.SetActive(true);
        }

        if (isMusic())
        {
            AudioCtrl.Ins.Play("BG");
            ButtonMusic[0].gameObject.SetActive(true);
            ButtonMusic[1].gameObject.SetActive(false);
        }
        else
        {
            AudioCtrl.Ins.Mute("BG");
            ButtonMusic[0].gameObject.SetActive(false);
            ButtonMusic[1].gameObject.SetActive(true);
        }
    }
    public void ChangeSound()
    {
        if (isSound())
        {
            PlayerPrefs.SetInt(Key_Sound, 0);
            PlayerPrefs.Save();
        }
        else
        {
            PlayerPrefs.SetInt(Key_Sound, 1);
            PlayerPrefs.Save();
        }

        ApplySetting();
    }
    public void ChangeMucis()
    {
        if (isMusic())
        {

            PlayerPrefs.SetInt(Key_Music, 0);
            PlayerPrefs.Save();
        }
        else
        {

            PlayerPrefs.SetInt(Key_Music, 1);
            PlayerPrefs.Save();

        }
        ApplySetting();
    }

    public bool isSound()
    {
        if (PlayerPrefs.GetInt(Key_Sound) == 1)
        {
            return true;
        }
        return false; 
    }

    public bool isMusic()
    {
        if (PlayerPrefs.GetInt(Key_Music) == 1)
        {
            return true;
        }
        return false;
    }

    public void SetSound(int i)
    {
        PlayerPrefs.SetInt(Key_Sound, i);
        PlayerPrefs.Save();
    }

    public void SetMusic(int i)
    {
        PlayerPrefs.SetInt(Key_Music, i);
        PlayerPrefs.Save();
    }
  


}
