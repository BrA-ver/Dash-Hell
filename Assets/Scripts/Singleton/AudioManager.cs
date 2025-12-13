using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : Singleton<AudioManager>
{
    // We need 2 Sound.cs arrays to hold the music and sound effects
    public Sound[] musicSounds, sfxSounds;

    // We need the music and sfx audio sources to play the sounds with
    public AudioSource musicSource, sfxSource;

    // I made looping true by defualt since most songs will loop
    public void PlayMusic(string name, bool looping = true)
    {
        // Stop whatever song is currently playing so they don't overlap
        StopMusic();

        // Get the sound of the song name from the music array. You can use this method `using System`;
        Sound s = Array.Find(musicSounds, x => x.name == name);

        // Do nothing if we don't find the sound, otherwise pass the sound to the music 
        // audio source, play the audio and tell it to loop
        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
            musicSource.loop = looping;
        }
    }

    // This method just needs the name of the sound it will play
    public void PlaySFX(string name)
    {
        // Get the sound of the song name from the music array. You can use this method `using System`;
        Sound s = Array.Find(sfxSounds, x => x.name == name);

        // Do nothing if you can't find the sound, otherwise tell the sfx audio source to play the sound's clip once
        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }
        else
        {
            sfxSource.PlayOneShot(s.clip);
        }
    }

    // This method is made just in case you want to stop the music for e.g. cutscenes
    public void StopMusic()
    {
        // Tell the music audio source to stop if it is playing
        if (musicSource.isPlaying)
        {
            musicSource.Stop();
        }
    }

}

// Make the class serializable so we can edit it's values 
[System.Serializable]
public class Sound
{
    // We need to give a name to the sound clip we will be playing and have that clip avaialable
    public string name;
    public AudioClip clip;
}