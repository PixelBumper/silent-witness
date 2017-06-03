using DefaultNamespace;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CharacterMusicController : MonoBehaviour
{
    [SerializeField] private AudioClip _music;

    private GameObject _distanceReference;
    private AudioSource _audioSource;
    private float _remainingPlayingTime;
    private float _remainingSpacingTime;

    private float _lastTempo;
    private float _lastLoopSpacing;

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
    }

    private void Update()
    {
        _remainingPlayingTime = Mathf.Max(_remainingPlayingTime - Time.deltaTime, 0f);
        
        if (_previousPosition != transform.position || _previousScale != transform.localScale)
        {
            UpdateSoundProperties();
            _previousPosition = transform.position;
            _previousScale = transform.localScale;
        }

        if (_remainingPlayingTime <= 0)
        {
            _remainingSpacingTime = Mathf.Max(_remainingSpacingTime - Time.deltaTime, 0f);

            if (_remainingSpacingTime <= 0)
            {
                _remainingPlayingTime = _audioSource.clip.length;
                _remainingSpacingTime = _lastTempo * _lastLoopSpacing;
                _audioSource.Play();
            }
        }
    }

    private void UpdateSoundProperties()
    {
        var referencePosition = _distanceReference.transform.position;
        var position = transform.position;
        _lastLoopSpacing = GetPercentageDistance(referencePosition.x, position.x);
        var previousSpacing = GetPercentageDistance(referencePosition.x, _previousPosition.x);
        var pitch = GetPercentageDistance(referencePosition.z, position.z);
        _lastTempo = GetPercentageDistance(referencePosition.y, position.y);
        var previousTempo = GetPercentageDistance(referencePosition.y, _previousPosition.y);
        var volumeScale = transform.localScale.x;

        var tempoDifferenceMultiplier = 1f + previousTempo - _lastTempo;
        _remainingPlayingTime *= tempoDifferenceMultiplier;

        _audioSource.pitch = _lastTempo;
        _audioSource.volume = volumeScale;

        if (_remainingPlayingTime > 0)
        {
            _remainingSpacingTime = _lastLoopSpacing;
        }
        else
        {
            _remainingSpacingTime -= previousSpacing - _lastLoopSpacing;
        }
    }

    private float GetPercentageDistance(float referenceAxis, float actualAxis)
    {
        return Mathf.Abs(referenceAxis - actualAxis);
    }
}