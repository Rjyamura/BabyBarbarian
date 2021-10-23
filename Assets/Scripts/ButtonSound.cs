using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    [SerializeField] private AudioClip _buttonOn;

    [SerializeField] private AudioClip _buttonOff;

    [SerializeField] private AudioClip _crateSfx;

    private bool _isCrate;

    // Start is called before the first frame update
    void Start()
    {
        _isCrate = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CrateSfxOn() => _isCrate = true;

    public void PlaySwitchOn()
    {
        if (!_isCrate)
        {
            AudioManager.Instance.PlayEffect(_buttonOn, 0.15f);
        }
        else if(_isCrate)
        {
            AudioManager.Instance.PlayEffect(_crateSfx, 0.2f);
            _isCrate = false;
        }
    }


    public void PlaySwitchOff()
    {
        AudioManager.Instance.PlayEffect(_buttonOff, 0.15f);
    }
}
