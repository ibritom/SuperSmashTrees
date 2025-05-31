using UnityEngine;
using System.Collections.Generic;

public class Jukebox : MonoBehaviour
{
    public List<AudioClip> Tracks;
    public AudioSource AudioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayRandomTrack();
    }

    public void PlayRandomTrack()
    {
        int randomIndex = Random.Range(0, Tracks.Count);
        AudioSource.clip = Tracks[randomIndex];
        AudioSource.Play();
    }

}
