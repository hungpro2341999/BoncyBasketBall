using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioCtrl : MonoBehaviour
{
    public List<AudioSource> Sounds = new List<AudioSource>();
    // Start is called before the first frame update
    public List<AudioClip> AudioClip; 
    public static AudioCtrl Ins;
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

        for(int i = 0; i < AudioClip.Count; i++)
        {
            var audio =  gameObject.AddComponent<AudioSource>();
          if(audio.name == "BG")
            {
                audio.loop = true;
            }
            audio.clip = AudioClip[i];
            Sounds.Add(audio);
        }

        if (SettingCtrl.Ins.isMusic())
        {
            Play("BG");
        }
    }
    //public void ChangeMusic()
    //{
    //    if (SettingCtrl.Ins.isMusic())
    //    {
    //        Play("BG");

    //    }
    //    else
    //    {
    //        Mute("BG");
    //    }
    //}

    public void Play(string name)
    {
        if (SettingCtrl.Ins.isSound())
        {
            foreach (var sound in Sounds)
            {
                if (sound.clip.name == name)
                {
                    sound.mute = false;
                    sound.Play();
                }

            }
        }
       
    }
    public void Mute(string name)
    {
        foreach (var sound in Sounds)
        {
            if (sound.clip.name == name)
            {
                sound.mute = true;
                sound.Play();
            }
                
        }
    }
    public void SetVolumn(string name,float volume)
    {
        foreach (var sound in Sounds)
        {
            if (sound.clip.name == name)
            {
                sound.volume = volume;
                sound.Play();
            }

        }
    }
    public void Pause(string name)
    {
        foreach (var sound in Sounds)
        {
            if (sound.clip.name == name)
            {

                sound.Pause();
            }

        }
    }
    public void ContinuePlay(string name)
    {
        foreach (var sound in Sounds)
        {
            if (sound.clip.name == name)
            {

                sound.UnPause();
            }

        }
    }

}
