using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable_Item : MonoBehaviour
{
    private NewPlayer _player;
    private Animator _anim;
    [SerializeField] private BoxCollider2D _bc;
    [SerializeField] private GameObject _table;

    [SerializeField] private AudioClip _crashSound;
    [SerializeField] private AudioClip _hitSound;
    [SerializeField] private float _sfxVol;

    private void Start()
    {
        _player = FindObjectOfType<NewPlayer>();
        _anim = GetComponent<Animator>();

        _bc = GetComponent<BoxCollider2D>();

        Physics2D.IgnoreCollision(_bc, _table.GetComponent<BoxCollider2D>());
    }


    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("table_break"))
        {
            Animator animator = _table.GetComponent<Animator>();
            animator.SetTrigger("Hit");
            HandleTrigger(false);
            CameraShake.Instance.ShakeCamera(10.0f, .5f);
            _sfxVol = 1.0f;
            PlayAudio(_crashSound, _sfxVol);
        }

        if (other.tag == "Weapon")
        {
            if (_player.isRaging == true)
            {
                CameraShake.Instance.ShakeCamera(7.0f, 0.5f);
                _sfxVol = 1.5f;
                PlayAudio(_hitSound, _sfxVol);
                _player.HitItem();
                _anim.SetTrigger("Hit");
                _bc.enabled = false;
            }
        }
    }


    private void PlayAudio(AudioClip _soundFX, float _sfxVolume)
    {
        if (_soundFX != null)
        {
            AudioManager.Instance.PlayEffect(_soundFX, _sfxVolume);
        }
    }


    private void HandleTrigger(bool isEnabled) => _bc.isTrigger = isEnabled;
    

}
