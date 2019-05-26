using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameMusic : MonoBehaviour
{
    public AudioSource[] soundtracks;

    // Start is called before the first frame update
    void Start()
    {
        soundtracks = GetComponents<AudioSource>();
    }

    public void PlayWin()
    {
        soundtracks[0].Stop();
        soundtracks[1].Play();
    }

    public void PlayLoss()
    {
        soundtracks[0].Stop();
        soundtracks[2].Play();
    }

    public void PlayDraw()
    {
        soundtracks[0].Stop();
        soundtracks[3].Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (!soundtracks.Any(s => s.isPlaying))
        {
            soundtracks[0].Play();
        }
    }
}
