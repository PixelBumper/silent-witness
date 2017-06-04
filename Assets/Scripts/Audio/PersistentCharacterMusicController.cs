using UnityEngine;

public class PersistentCharacterMusicController : CharacterMusicController
{
    [SerializeField] private float _initialRemainingTime;

    public float InitialRemainingTime
    {
        get { return _initialRemainingTime; }
        set { _initialRemainingTime = value; }
    }

    protected override void InitializeSong()
    {
        RemainingPlayingTime = Time.time % DistanceReference.SecondsPerPortion + InitialRemainingTime;
        UpdateSoundProperties();
    }
}