using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MusicMgr : BaseManager<MusicMgr>
{
    private AudioSource bkMusic = null;
    private float bkValue = 1;
    private float soundVaule = 1;
    private GameObject soundObj = null;
    private List<AudioSource> soundList = new List<AudioSource>();

    public MusicMgr()
    {
        MonoMgr.GetInstance().AddUpdateListener(OnUpdate);
    }
    private void OnUpdate()
    {
        for (int i = soundList.Count - 1; i >= 0; i--)
        {
            if (soundList[i].isPlaying)
            {
                Object.Destroy(soundList[i]);
                soundList.RemoveAt(i);
            }
        }
    }

    #region  ±≥æ∞“Ù¿÷
    /// <summary>
    /// ≤•∑≈±≥æ∞“Ù¿÷
    /// </summary>
    /// <param name="name"></param>
    public void PlayBKMusic(string name)
    {
        if (bkMusic == null) {
            GameObject obj = new GameObject("BkMusic");
            bkMusic = obj.AddComponent<AudioSource>();
        }
        ResourcesMgr.GetInstance().LoadAsync<AudioClip>("Music/BK/" + name, (clip) =>
        {
            bkMusic.clip = clip;
            bkMusic.volume = bkValue;
            bkMusic.Play();
        });
    }
    /// <summary>
    /// ‘›Õ£±≥æ∞“Ù¿÷
    /// </summary>
    public void PauseBKMusic()
    {
        if (bkMusic == null) return;
        bkMusic.Pause();
    }

    /// <summary>
    /// Õ£÷π±≥æ∞“Ù¿÷
    /// </summary>
    public void StopBackMusic()
    {
        if (bkMusic == null) return;
        bkMusic.Stop();
    }
    /// <summary>
    /// ∏ƒ±‰±≥æ∞“Ù¡ø¥Û–°
    /// </summary>
    /// <param name="v">≈‰∫œ–≠≥Ã π”√</param>
    public void ChangeBKValue(float v)
    {
        bkValue = v;
        if (bkMusic != null) bkMusic.volume = bkValue;
    }
    #endregion

    /// <summary>
    /// ≤•∑≈“Ù–ß
    /// </summary>
    /// <param name="name"></param>
    public void PlaySound(string name, bool isLoop = false, UnityAction<AudioSource> callback = null)
    {
        if (soundObj == null)
        {
            soundObj = new GameObject();
            soundObj.name = "Sounds";
        }
        AudioSource source = soundObj.AddComponent<AudioSource>();
        ResourcesMgr.GetInstance().LoadAsync<AudioClip>("Music/Sounds/" + name,
            (clip) =>
            {
                source.clip = clip;
                source.loop = isLoop;
                source.volume = soundVaule;
                source.Play();
                soundList.Add(source);
                if (callback != null)
                {
                    callback(source);
                }
            });
    }
    /// <summary>
    /// Õ£÷π“Ù–ß
    /// </summary>
    public void StopSound(AudioSource source)
    {
        if (soundList.Contains(source))
        {
            soundList.Remove(source);
            source.Stop();
            Object.Destroy(source);
        }
    }
    /// <summary>
    /// ∏ƒ±‰“Ù¡ø
    /// </summary>
    /// <param name="value"></param>
    public void ChangeSoundValue(float value)
    {
        soundVaule = value;
        for (int i = 0; i < soundList.Count; ++i)
        {
            soundList[i].volume = value;
        }
    }


}
