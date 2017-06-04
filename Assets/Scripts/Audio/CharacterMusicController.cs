using DefaultNamespace;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CharacterMusicController : MonoBehaviour, ISilencable
{
    [SerializeField, Tooltip("Set clip in the audio source, do not modify this audio source externally!")]
    protected internal AudioSource AudioSource;

    protected SoundPropertyReference DistanceReference;
    protected internal float RemainingPlayingTime;
    private float _lastPlayingTime;
    private float _lastTempo;

    private Vector3 _previousPosition;
    private Vector3 _previousScale;

    protected virtual void Awake()
    {
        DistanceReference = GameObject.FindGameObjectWithTag(Tags.SoundPropertyReference)
            .GetComponent<SoundPropertyReference>();
    }

    private void Start()
    {
        _previousScale = transform.localScale;
        _previousPosition = transform.position;
        OnEnable();
    }

    private void OnEnable()
    {
        InitializeSong();
    }

    protected virtual void InitializeSong()
    {
        RemainingPlayingTime = Time.time % DistanceReference.SecondsPerPortion;
        UpdateSoundProperties();
    }

    private void OnDisable()
    {
        if (AudioSource)
        {
            AudioSource.Stop();
        }
        RemainingPlayingTime = 0f;
    }

    private void Update()
    {
        RemainingPlayingTime = Mathf.Max(RemainingPlayingTime - Time.deltaTime, 0f);

        if (_previousPosition != transform.position || _previousScale != transform.localScale)
        {
            UpdateSoundProperties();
        }

        if (RemainingPlayingTime <= 0)
        {
            RemainingPlayingTime = _lastPlayingTime;
            AudioSource.Play();
        }
    }

    protected void UpdateSoundProperties()
    {
        var previousSpacingSeconds = _lastPlayingTime;
        _lastPlayingTime = DistanceReference.GetPeriodInSeconds(transform);
        _lastTempo = DistanceReference.GetTempoPercentage(transform);
        var volumeScale = transform.localScale.x / DistanceReference.ScalingToVolumeDivisor;

        AudioSource.pitch = _lastTempo;
        AudioSource.volume = volumeScale;

        RemainingPlayingTime -= previousSpacingSeconds - _lastPlayingTime;
        while (RemainingPlayingTime < 0)
        {
            RemainingPlayingTime += _lastPlayingTime;
        }

        _previousPosition = transform.position;
        _previousScale = transform.localScale;
    }

    public void ToggleSilence()
    {
        enabled = !enabled;
        AudioSource.enabled = enabled;
    }
}
