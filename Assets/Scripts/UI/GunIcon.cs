using UnityEngine;
using UnityEngine.UI;

public class GunIcon : MonoBehaviour
{
    [SerializeField] private Sprite _disabledIcon;
    [SerializeField] private Sprite _enabledIcon;

    private Image _image;
    
    private void Awake()
    {
        _image = GetComponent<Image>();

        if (_image == null)
        {
            Debug.LogError("GunIcon needs an Image component to work properly", this);
        }
    }

    public void Disable()
    {
        _image.sprite = _disabledIcon;
    }
    
    public void Enable()
    {
        _image.sprite = _enabledIcon;
    }
}
