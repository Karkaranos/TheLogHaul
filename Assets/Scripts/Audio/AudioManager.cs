/*****************************************************************************
// File Name :         AudioManager.cs
// Author :            Cade R. Naylor
// Creation Date :     January 28, 2024
//
//Based on:             Brackeys Audio Manager
//Tutorial video:       https://youtu.be/6OT43pvUyfY

// Brief Description :  Creates an array of all sound effects and music then adds an
audio source to each of them. Can call sound by using the name of the audio in the
inspector
*****************************************************************************/
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public Sound[] Sounds;
    public AudioMixerGroup masterMixer;
    public float musicVolume;

    public LevelSoundBlend[] Levels;

    private bool gameStarted = false;

    private string previousTrack;
    private int maxWeight;
    private Coroutine background;

    [Header("Audio Random Controls")]
    [SerializeField] private bool useWeightedAmounts = true;
    [SerializeField] private float minNatureTime = .2f;
    [SerializeField] private float maxNatureTime = 3;
    [SerializeField] private float minHumanTime = .5f;
    [SerializeField] private float maxHumanTime = 6;

    //public Texture2D glassTexture;
    //public CursorMode cursorMode = CursorMode.Auto;
    //public Vector2 hotSpot = Vector2.zero;

    /// <summary>
    /// Start is called before the first frame update. It ensures only one instance
    /// of this script and initializes Sound class
    /// </summary>
    void Start()
    {
        int numAM = FindObjectsOfType<AudioManager>().Length;
        if (numAM != 1)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }

        foreach (Sound sound in Sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();


            sound.source.clip = sound.audClip;
            sound.source.outputAudioMixerGroup = masterMixer;
            sound.source.volume = sound.clipVolume;
            sound.source.pitch = sound.clipPitch;
            sound.source.loop = sound.canLoop;
            sound.source.rolloffMode = AudioRolloffMode.Linear;
            sound.source.playOnAwake = false;
        }

        WeighSounds();

        //Cursor.SetCursor(glassTexture, hotSpot, cursorMode);
    }


    #region Sound Controls

    /// <summary>
    /// Gets the name of a sound and plays it at its point
    /// </summary>
    /// <param name="audioName">the sound name to play</param>
    public void Play(string audioName)
    {
        //Searches through the Sound array until it finds a sound with the 
        //specified name
        Sound sound = Array.Find(Sounds, sound => sound.name == audioName);
        if (sound != null)
        {
            sound.source.Play();
            //print(audioName + " started");
        }
    }

    /// <summary>
    /// Gets the name of a sound and stops it at its point
    /// </summary>
    /// <param name="audioName">the sound name to stop</param>
    public void Stop(string audioName)
    {
        Sound sound = Array.Find(Sounds, sound => sound.name == audioName);
        if (sound != null)
        {
            sound.source.Stop();
            //print(audioName + " stopped");
        }

    }


    /// <summary>
    /// Gets the name of a sound and pauses it at its point
    /// </summary>
    /// <param name="audioName">the sound name to pause</param>
    public void Pause(string audioName)
    {
        Sound sound = Array.Find(Sounds, sound => sound.name == audioName);
        if (sound != null)
        {
            sound.source.Pause();
        }
    }


    /// <summary>
    /// Gets the name of a sound and resumes it at its point
    /// </summary>
    /// <param name="audioName">the sound name to resume</param>
    public void UnPause(string audioName)
    {
        Sound sound = Array.Find(Sounds, sound => sound.name == audioName);
        if (sound != null)
        {
            sound.source.UnPause();
        }
    }

    /// <summary>
    /// Gets the name of a sound and disables its volume when it starts playing
    /// </summary>
    /// <param name="audioName">the sound name to play muted</param>
    public void PlayMuted(string audioName)
    {
        Sound sound = Array.Find(Sounds, sound => sound.name == audioName);
        if (sound != null)
        {
            sound.source.Play();
            sound.source.volume = 0.0f;
        }
    }

    /// <summary>
    /// Gets the name of a sound and mutes it
    /// </summary>
    /// <param name="audioName">the sound name to mute</param>
    public void Mute(string audioName)
    {
        Sound sound = Array.Find(Sounds, sound => sound.name == audioName);
        if (sound != null)
        {
            sound.source.volume = 0.0f;
        }
    }

    /// <summary>
    /// Gets the name of a sound and unmutes it
    /// </summary>
    /// <param name="audioName">the sound name to unmute</param>
    public void Unmute(string audioName)
    {
        Sound sound = Array.Find(Sounds, sound => sound.name == audioName);
        if (sound != null)
        {
            sound.source.volume = musicVolume;
        }
    }

    private void WeighSounds()
    {
        foreach(Sound s in Sounds)
        {
            s.weightedRangeLow = maxWeight;
            s.weightedRangeHigh = s.soundWeight + s.weightedRangeLow;
            maxWeight = s.weightedRangeHigh;
        }

    }

    #endregion

    #region Play

    public void StopAllSounds()
    {
        for (int i = 0; i < Sounds.Length; i++)
        {
            Sounds[i].source.Stop();
        }
    }

    private void StopAllBackground()
    {
        if(background!=null)
        {
            StopCoroutine(background);
        }
        for (int i = 0; i < Sounds.Length; i++)
        {
            if (Sounds[i].isBackground)
            {
                Stop(Sounds[i].name);
            }
        }
    }

    public void PlayBirds()
    {
        StopAllBackground();
        Play("Birds");
        print("should play audio");
    }

    public void PlayCars()
    {
        StopAllBackground();
        Play("Cars");
    }

    public void PlayStaticLevelBackground(int level)
    {
        StopAllBackground();
        background = StartCoroutine(StaticBackground(level));
    }

    private IEnumerator StaticBackground(int level)
    {
        float maxTimeLeft= 0, timeLeft = 0;
        if(Levels[level-1].progressiveDeforestation)
        {
            maxTimeLeft = Levels[level - 1].secondsToFinish;
            timeLeft = maxTimeLeft;
        }
        while(true)
        {
            float minTime, maxTime, natureChance;
            natureChance = (7f - Levels[level - 1].deforestationLevel) / 6f;
            if (Levels[level-1].progressiveDeforestation && level != Levels.Length)
            {
                print("Time left: " + timeLeft + " / " + maxTimeLeft);
                natureChance -= (1 -(timeLeft / maxTimeLeft)) * (((7f - Levels[level - 1].deforestationLevel) / 6f) - ((7f - Levels[level].deforestationLevel) / 6f));
                print(natureChance);
            }

            

            float randomChance = UnityEngine.Random.Range(0f, 1f);

            Sound.SoundFlavor flavor;

            if(randomChance < natureChance)
            {
                flavor = Sound.SoundFlavor.NATURE_ENVIRONMENT;
                minTime = minNatureTime;
                maxTime = maxNatureTime;
            }
            else
            {
                flavor = Sound.SoundFlavor.HUMAN_ENVIRONMENT;
                minTime = minHumanTime;
                maxTime = maxHumanTime;
            }

            if (useWeightedAmounts)
            {
                PlayWeightedBackgroundSound(flavor);
            }
            else
            {
                PlayBackgroundSound(flavor);
            }

            float waitForMe = UnityEngine.Random.Range(minTime, maxTime);
            timeLeft = Mathf.Clamp(timeLeft-waitForMe, 0, maxTimeLeft);
            
            yield return new WaitForSeconds(waitForMe);

        }
    }

    private void PlayWeightedBackgroundSound(Sound.SoundFlavor flavor)
    {
        print(flavor);
        string playMe = "";
        while(playMe.Equals(""))
        {
            int checkVal = UnityEngine.Random.Range(0, maxWeight);
            foreach(Sound s in Sounds)
            {
                //This condition is never true for human sounds
                if(checkVal < s.weightedRangeHigh && checkVal >= s.weightedRangeLow)
                {
                    if(s.soundSource == flavor && s.isBackground)
                    {
                        playMe = s.name;
                        break;
                    }
                }
            }
        }

        if(playMe!="")
        {
            Play(playMe);
        }
    }

    private void PlayBackgroundSound(Sound.SoundFlavor flavor)
    {
        string playMe = "";
        while (playMe.Equals(""))
        {
            int checkVal = UnityEngine.Random.Range(0, Sounds.Length);
            if(Sounds[checkVal].isBackground && Sounds[checkVal].soundSource == flavor)
            {
                playMe = Sounds[checkVal].name;
            }
        }

        Play(playMe);
    }

    #endregion
}
