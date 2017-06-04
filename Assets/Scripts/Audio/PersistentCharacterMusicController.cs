using UnityEngine;

public class PersistentCharacterMusicController : CharacterMusicController
{
    [SerializeField] private float _initialRemainingTime;

    public float InitialRemainingTime
    {
        get { return _initialRemainingTime; }
        set { _initialRemainingTime = value; }
    }

    protected override void Awake()
    {
        AudioSource = GetComponent<AudioSource>();
        AudioSource.playOnAwake = false;
        AudioSource.loop = false;
        base.Awake();
    }

    public void CopyFromController(CharacterMusicController musicController)
    {
        Awake();
        AudioSource.clip = musicController.AudioSource.clip;
        AudioSource.outputAudioMixerGroup = musicController.AudioSource.outputAudioMixerGroup;
        InitialRemainingTime = musicController.RemainingPlayingTime;
    }

    protected override void InitializeSong()
    {
        RemainingPlayingTime = Time.time % DistanceReference.SecondsPerPortion + InitialRemainingTime;
        UpdateSoundProperties();
    }
}