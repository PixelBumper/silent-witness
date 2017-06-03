using DefaultNamespace;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CharacterMusicController : MonoBehaviour
{
    [SerializeField] private AudioClip _music;

    private GameObject _distanceReference;
    private AudioSource _audioSource;
    private float _remainingPlayingTime;
    private float _lastPlayingTime;
    private float _lastTempo;

    private Vector3 _previousPosition;
    private Vector3 _previousScale;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _distanceReference = GameObject.FindGameObjectWithTag(Tags.SoundPropertyReference);
    }

    private void Start()
    {
        _audioSource.loop = false;
        _audioSource.clip = _music;
        _previousPosition = transform.position;

        _remainingPlayingTime = _music.length;
        UpdateSoundProperties();
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
        var referencePosition = _distanceReference.transform.position;
        var position = transform.position;
        var previousSpacingSeconds = _lastPlayingTime;
        _lastPlayingTime = GetSpacingDistanceSeconds(referencePosition.x, position.x);
        _lastTempo = GetPercentageDistance(referencePosition.y, position.y);
        var volumeScale = transform.localScale.x;

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

    private float GetSpacingDistanceSeconds(float referenceAxis, float actualAxis)
    {
        var actualDistance = Mathf.Abs(referenceAxis - actualAxis);
        var adjustedDistance = Mathf.Max((actualDistance - 10f) / 10f + 1f, 0.1f);

        return _music.length * adjustedDistance;
    }

    private float GetPercentageDistance(float referenceAxis, float actualAxis)
    {
        return Mathf.Abs(referenceAxis - actualAxis);
    }
}