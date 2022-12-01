using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;

    [SerializeField]
    [Range(0, 1f)]
    float globalVolume;


    // Start is called before the first frame update
    void Awake ()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    void Start()
    {
        Play("levelMusic");
        resetVolumes();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnValidate()
    {
        resetVolumes();
    }

    public void Play(string name)
    {
        foreach(Sound s in sounds)
        {
            if (s.name == name && s.source != null)
            {
                s.source.Play();
                return;
            }
        }
    }

    public float getGlobalVolume()
    {
        return globalVolume;
    }

    public void setGlobalVolume(float globalVolume)
    {
        this.globalVolume = globalVolume;
        resetVolumes();
    }

    public void resetVolumes()
    {
        if (sounds != null)
        {
            foreach (Sound s in sounds)
            {
                if (s.source != null) s.source.volume = s.volume * globalVolume;
            }
        }
    }

    public void Stop(string name)
    {
        foreach (Sound s in sounds)
        {
            if (s.name == name && s.source.isPlaying)
            {
                s.source.Stop();
                return;
            }
        }
    }

    public Sound getSoundByName(string name)
    {
        foreach(Sound s in sounds)
        {
            if (s.name == name)
                return s;
        }
        return null;
    }
}
