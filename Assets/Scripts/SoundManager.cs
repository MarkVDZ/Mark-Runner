using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour {

    public AudioSource audio;
    public AudioClip move;
    public AudioClip jump;
    public AudioClip die;
    public AudioClip hurt;
    public AudioClip pickup;
    public AudioClip music;


    // Use this for initialization
    void Start () {
        //audio.GetComponent<AudioSource>();
        playMusic();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void playMusic()
    {
        print("PLAY MUSIC NOW");
        audio.clip = music;
        audio.loop = true;
    }

    public void playSFX(AudioClip clip)
    {
        //audio.clip = clip;
        audio.PlayOneShot(clip);
        //audio.Play();
        
        //AudioSource.PlayClipAtPoint(clip, transform.position);
    }

}
