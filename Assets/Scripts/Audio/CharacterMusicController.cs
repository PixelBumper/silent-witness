using DefaultNamespace;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CharacterMusicController : MonoBehaviour
{
    [SerializeField, Tooltip("Set clip in the audio source, do not modify this audio source externally!")]
    private AudioSource _audioSource;

    protected SoundPropertyReference DistanceReference;
    protected float RemainingPlayingTime;
    private float _lastPlayingTime;
    private float _lastTempo;

    private Vector3 _previousPosition;
    private Vector3 _previousScale;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.loop = false;
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
        _audioSource.Stop();
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
            _audioSource.Play();
        }
    }

    protected void UpdateSoundProperties()
    {
        var previousSpacingSeconds = _lastPlayingTime;
        _lastPlayingTime = DistanceReference.GetPeriodInSeconds(transform);
        _lastTempo = DistanceReference.GetTempoPercentage(transform);
        var volumeScale = transform.localScale.x / DistanceReference.ScalingToVolumeDivisor;

        _audioSource.pitch = _lastTempo;
        _audioSource.volume = volumeScale;

        RemainingPlayingTime -= previousSpacingSeconds - _lastPlayingTime;
        while (RemainingPlayingTime < 0)
        {
            RemainingPlayingTime += _lastPlayingTime;
        }

        _previousPosition = transform.position;
        _previousScale = transform.localScale;
    }

    public void ToggleMute()
    {
        enabled = !enabled;
        _audioSource.enabled = enabled;
    }
}