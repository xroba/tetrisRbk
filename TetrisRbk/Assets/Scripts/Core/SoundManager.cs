using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public bool m_musicEnabled = true;
    public bool m_fxEnabled = true;
    [Range(0,1)]
    public float m_musicVolume;
    [Range(0,1)]
    public float m_fxVolume;

    public AudioClip m_clearRowSound;
    public AudioClip m_moveSound;
    public AudioClip m_dropSound;
    public AudioClip m_gameoverSound;
    public AudioClip m_errorSound;
    public AudioClip[] m_backgroundMusic;
    public AudioClip[] m_vocalLineCompleted;
    public AudioSource m_musicSource;

    public ToggleIcon m_soundEffectIcon;
    public ToggleIcon m_musicIcon;
    


    void PlayBackgroundMusic(AudioClip audioclip)
    {

        if (!m_musicEnabled || !m_musicSource || !audioclip)
            return;

        m_musicSource.Stop();
        m_musicSource.clip = audioclip;
        m_musicSource.volume = m_musicVolume;
        m_musicSource.loop = true;
        m_musicSource.Play();

    }


    public void PlayFxMove()
    {
        PlayFxSound(m_moveSound);
    }
        

    public void PlayFxLand()
    {
        PlayFxSound(m_dropSound);
    }

    public void PlayFxClearRow()
    {
        PlayFxSound(m_clearRowSound);
    }

    public void PlayFxGameOver()
    {
        PlayFxSound(m_gameoverSound);
    }

    public void PlayFxErrorSound()
    {
        PlayFxSound(m_errorSound);
    }

    public void PlayVocalDoubleKill()
    {
        PlayFxSound(m_vocalLineCompleted[0]);
    }
    public void PlayVocalHattrick()
    {
        PlayFxSound(m_vocalLineCompleted[1]);
    }
    public void PlayVocalKillingSpree()
    {
        PlayFxSound(m_vocalLineCompleted[2]);
    }



    void PlayFxSound(AudioClip clip)
    {
        if (!m_fxEnabled || !clip)
        {
            return;
        }

        AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, m_fxVolume);

    }

    void UpdateMusic()
    {
        if(m_musicSource.isPlaying != m_musicEnabled)
        {
            if (m_musicEnabled)
            {
                PlayBackgroundMusic(m_backgroundMusic[RetrieveRandomBackroundMusicIndex()]);
            }
            else
            {
                m_musicSource.Stop();
               
            }
        }
        m_musicIcon.SetToggleIconOnOff(m_musicEnabled);
    }


    public void ToggleMusic()
    {
        m_musicEnabled = !m_musicEnabled;
        UpdateMusic();
    }

    public void ToggleEffect()
    {
        m_fxEnabled = !m_fxEnabled;
        m_soundEffectIcon.SetToggleIconOnOff(m_fxEnabled);
    }

    int RetrieveRandomBackroundMusicIndex()
    {
       return Random.Range(0, m_backgroundMusic.Length);
    }



    // Use this for initialization
    void Start () {
        PlayBackgroundMusic(m_backgroundMusic[RetrieveRandomBackroundMusicIndex()]);
	}
	
	// Update is called once per frame
	void Update () {
		
	}


}
