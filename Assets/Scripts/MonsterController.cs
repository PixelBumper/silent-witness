using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(AudioSource), typeof(Renderer))]
public class MonsterController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private AudioSource _audioSource;

    private Renderer _renderer;

    public AudioClip GrabSound;

    public AudioClip UnGrabSound;

    public AudioClip MusicSound;

    public Color StartHighlightColor;
    
    public Color EndHighlightColor;

    private Color _originalColor;

    public float HighlightInterval;

    private bool _highlighted;

    public AnimationCurve Curve;

    // Use this for initialization
    void Start()
    {
        _renderer = GetComponent<Renderer>();
        _audioSource = GetComponent<AudioSource>();
        _audioSource.Stop();
        _audioSource.clip = MusicSound;
        _originalColor = _renderer.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_highlighted) return;
        var lerp = Mathf.PingPong(Time.time, HighlightInterval) / HighlightInterval;
        _renderer.material.color = Color.Lerp(StartHighlightColor, EndHighlightColor, lerp);
    }

    public void Grab()
    {
        _audioSource.PlayOneShot(GrabSound);
    }

    public void Ungrab()
    {
        _audioSource.PlayOneShot(UnGrabSound);
    }

    public void TurnOn()
    {
        _audioSource.Play();
    }

    public void TurnOff()
    {
        _audioSource.Stop();
    }

    private void StartHiglightingAnimation()
    {
        _highlighted = true;
        
    }

    private void StopHighlightAnimation()
    {
        _highlighted = false;
        _renderer.material.color = _originalColor;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("focussing");
        StartHiglightingAnimation();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("unfocus");
        StopHighlightAnimation();
    }
}