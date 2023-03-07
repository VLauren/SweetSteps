using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static float FXVolumeMultiplier = 1;

    static AudioSource source;
    static Dictionary<string, AudioSource> loopingSounds;

    static AudioManager Instance;

    void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
        {
            Instance = this;
            source = GetComponent<AudioSource>();
            loopingSounds = new Dictionary<string, AudioSource>();
        }
    }

    void Start()
    {
        DontDestroyOnLoad(this);
    }

    public static void Play(string _name, bool _loop, float _volume = 0.5f)
    {
        AudioClip clip = Resources.Load<AudioClip>(_name);

        if (!_loop)
            source.PlayOneShot(clip, _volume);
        else
        {
            if(!loopingSounds.ContainsKey(_name))
            {
                AudioSource newSource = source.gameObject.AddComponent<AudioSource>();
                loopingSounds.Add(_name, newSource);

                newSource.clip = clip;
                newSource.spatialBlend = 0;
                newSource.volume = _volume;
                newSource.loop = true;

                newSource.Play();
            }
            else
            {
                loopingSounds[_name].volume = _volume;
            }
        }
    }

    public static void Stop(string _name)
    {
        if(loopingSounds.ContainsKey(_name))
        {
            loopingSounds[_name].Stop();
            Destroy(loopingSounds[_name]);
            loopingSounds.Remove(_name);
        }
    }
}
