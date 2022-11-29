using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;

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
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
