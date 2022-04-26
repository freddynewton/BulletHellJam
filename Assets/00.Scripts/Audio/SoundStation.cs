using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.Audio;


public class SoundStation : MonoBehaviour
{
    public SoundAsset music, chefSFX, firebulletSFX, ingredientSFX;
    public enum Asset { Music, ChefSFX, FirebulletSFX, IngredientSFX }
    public static SoundStation Instance { get; private set; }

    Sound currentMusicPlayer;
    
    void Awake()
    {
        //if (Instance == null)
            Instance = this;

        SetupAsset(music);
        SetupAsset(chefSFX);
        SetupAsset(firebulletSFX);
        SetupAsset(ingredientSFX);
    }

    void SetupAsset(SoundAsset soundAsset)
    {
        foreach (Sound s in soundAsset.asset)
        {
            s.source = gameObject.AddComponent<AudioSource>();

            s.source.volume = s.volume;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = s.mixer;
        }
    }

    public void Play(Asset asset, string name)
    {
        Sound s = Array.Find(GetSounds(asset), sound => sound.name == name);
        if (s == null)
            Debug.LogWarning("Sound " + name + " does not exist");
        s.source.clip = s.clips[UnityEngine.Random.Range(0, s.clips.Length)];
        s.source.pitch = UnityEngine.Random.Range(s.pitchMin, s.pitchMax);
        s.source.Play();
    }

    public void Stop(Asset asset, string name)
    {
        Sound s = Array.Find(GetSounds(asset), sound => sound.name == name);
        if (s == null)
            Debug.LogWarning("Sound " + name + " does not exist");
        s.source.Stop();
    }

    Sound[] GetSounds(Asset asset)
    {
        switch (asset)
        {
            case Asset.Music:
                return music.asset;
            case Asset.ChefSFX:
                return chefSFX.asset;
            case Asset.FirebulletSFX:
                return firebulletSFX.asset;
            case Asset.IngredientSFX:
                return ingredientSFX.asset;
            default:
                return null;
        }
    }
}

[Serializable]
public class Sound
{
    public string name;

    public AudioClip[] clips;
    public AudioMixerGroup mixer;

    [Range(0f, 1f)] public float volume;
    [Range(0.1f, 3f)] public float pitchMin = 1, pitchMax = 1;
    public bool loop;

    [HideInInspector] public AudioSource source;
}