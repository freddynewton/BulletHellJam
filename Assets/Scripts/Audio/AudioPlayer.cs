using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

public class AudioPlayer : MonoBehaviour
{
    public AudioSource AudioSource { get; private set; }
    [SerializeField] AudioMixerGroup musicGroup, SFXGroup;

    AudioStation audioStation;

    bool started;

    void Awake()
    {
        AudioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        audioStation = AudioStation.Instance;
    }

    public void SetupSFX(AudioClip[] clips, float audioPitchMin, float audioPitchMax, bool is2D)
    {
        AudioClip randomClip = clips[Random.Range(0, clips.Length)];
        SetupSFX(randomClip, audioPitchMin, audioPitchMax, is2D);
    }

    public void SetupSFX(AudioClip clip, float audioPitchMin, float audioPitchMax, bool is2D)
    {
        AudioSource.volume = .55f;
        AudioSource.clip = clip;
        AudioSource.outputAudioMixerGroup = SFXGroup;
        AudioSource.pitch = Random.Range(audioPitchMin, audioPitchMax);
        AudioSource.spatialBlend = is2D ? 0 : 1;
    }

    public void SetupMusic(AudioClip clip, bool loop)
    {
        AudioSource.volume = .5f;
        AudioSource.clip = clip;
        AudioSource.outputAudioMixerGroup = musicGroup;
        AudioSource.loop = loop;
        AudioSource.pitch = 1;
        AudioSource.reverbZoneMix = 0; // NO REVERB ZONE EFFECT TO MUSIC
        AudioSource.spatialBlend = 0;
    }

    public void Play()
    {
        name = AudioSource.clip.name;
        AudioSource.Play();
        started = true;
    }

    void Update()
    {
        if (started)
            if (!AudioSource.loop)
                StartCoroutine(RecycleAfterAudioEnd());
    }

    IEnumerator RecycleAfterAudioEnd()
    {
        yield return new WaitForSeconds(AudioSource.clip.length + .5f);
        audioStation.audioPlayers.Remove(this);
    }
}