﻿using DefaultNamespace;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CharacterMusicController : MonoBehaviour
{
    [SerializeField, Tooltip("Set clip in the audio source, do not modify this audio source externally!")]
    private AudioSource _audioSource;

    private SoundPropertyReference _distanceReference;
    private float _remainingPlayingTime;
    private float _lastPlayingTime;
    private float _lastTempo;

    private Vector3 _previousPosition;
    private Vector3 _previousScale;

    private float MusicLength
    {
        get { return Mathf.Ceil(_audioSource.clip.length); }
    }

    private int MusicTotalPortions
    {
        get { return Mathf.CeilToInt(MusicLength / _distanceReference.SecondsPerPortion); }
    }

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _distanceReference = GameObject.FindGameObjectWithTag(Tags.SoundPropertyReference)
            .GetComponent<SoundPropertyReference>();
    }

    private void Start()
    {
        _audioSource.loop = false;
        _previousPosition = transform.position;

        _remainingPlayingTime = Time.time % _distanceReference.SecondsPerPortion;
        UpdateSoundProperties();
    }

    private void OnEnable()
    {
        _remainingPlayingTime = Time.time % _distanceReference.SecondsPerPortion;
        UpdateSoundProperties();
    }

    private void OnDisable()
    {
        _audioSource.Stop();
        _remainingPlayingTime = 0f;
    }

    private void Update()
    {
        _remainingPlayingTime = Mathf.Max(_remainingPlayingTime - Time.deltaTime, 0f);

        if (_previousPosition != transform.position || _previousScale != transform.localScale)
        {
            UpdateSoundProperties();
        }

        if (_remainingPlayingTime <= 0)
        {
            _remainingPlayingTime = _lastPlayingTime;
            _audioSource.Play();
        }
    }

    private void UpdateSoundProperties()
    {
        var previousSpacingSeconds = _lastPlayingTime;
        _lastPlayingTime = _distanceReference.GetPeriodInSeconds(transform);
        _lastTempo = _distanceReference.GetTempoPercentage(transform);
        var volumeScale = transform.localScale.x / _distanceReference.ScalingToVolumeDivisor;

        _audioSource.pitch = _lastTempo;
        _audioSource.volume = volumeScale;

        _remainingPlayingTime -= previousSpacingSeconds - _lastPlayingTime;
        while (_remainingPlayingTime < 0)
        {
            _remainingPlayingTime += _lastPlayingTime;
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