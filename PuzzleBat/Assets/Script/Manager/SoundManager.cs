using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ESoundType
{
    BGM,
    BtnClicked,
    BlockRelease,
    MaxCnt
}

public class SoundManager : Singleton<SoundManager>
{
    private bool bgmON = true;
    private bool effectON = true;

    private AudioSource[] audioSources = new AudioSource[(int)ESoundType.MaxCnt];
    private Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();

    // getter setter
    public bool BGM_ON { get { return bgmON; } set { bgmON = value; } }
    public bool Effect_ON { get { return effectON; } set { effectON = value; } }

    void Awake()
    {
        Init();
    }

    void Init()
    {
        // create audio source and clip object
        GameObject sound = GameObject.Find("Sound");
        if (sound == null)
        {
            sound = new GameObject { name = "Sound" };
            DontDestroyOnLoad(sound);

            string[] soundNames = System.Enum.GetNames(typeof(ESoundType));

            for (int i = 0; i < soundNames.Length - 1; i++)
            {
                GameObject go = new GameObject { name = soundNames[i] };
                audioSources[i] = go.AddComponent<AudioSource>();
                go.transform.parent = sound.transform;

                string path = "Sound/" + soundNames[i];
                AudioClip audioClip = Resources.Load<AudioClip>(path);
                
                if(audioClip != null)
                {
                    audioClips.Add(soundNames[i], audioClip);
                }
            }

            audioSources[(int)ESoundType.BGM].loop = true;
        }

    }

    public void Play(ESoundType soundType)
    {
        string[] soundNames = System.Enum.GetNames(typeof(ESoundType));

        switch ((int)soundType)
        {
            case (int)ESoundType.BGM:
                {
                    if (bgmON == false)
                    {
                        break;
                    }

                    AudioSource audio = audioSources[(int)ESoundType.BGM];
                    audio.clip = audioClips[soundNames[(int)ESoundType.BGM]];
                    audio.Play();
                }
                break;
            case (int)ESoundType.BtnClicked:
                {
                    if (effectON == false)
                    {
                        break;
                    }

                    AudioSource audio = audioSources[(int)ESoundType.BtnClicked];
                    AudioClip clip = audioClips[soundNames[(int)ESoundType.BtnClicked]];
                    audio.PlayOneShot(clip);
                }
                break;
            case (int)ESoundType.BlockRelease:
                {
                    if (effectON == false)
                    {
                        break;
                    }

                    AudioSource audio = audioSources[(int)ESoundType.BlockRelease];
                    AudioClip clip = audioClips[soundNames[(int)ESoundType.BlockRelease]];
                    audio.PlayOneShot(clip);
                }
                break;
            default:
                break;
        }
    }

    public bool IsPlayBGM()
    {
        AudioSource audio = audioSources[(int)ESoundType.BGM];
        return audio.isPlaying;
    }

    public void Stop(ESoundType soundType)
    {
        string[] soundNames = System.Enum.GetNames(typeof(ESoundType));

        switch ((int)soundType)
        {
            case (int)ESoundType.BGM:
                {
                    AudioSource audio = audioSources[(int)ESoundType.BGM];
                    audio.clip = audioClips[soundNames[(int)ESoundType.BGM]];
                    audio.Stop();
                }
                break;
            default:
                break;
        }
    }
}
