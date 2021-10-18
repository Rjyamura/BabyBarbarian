using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObject : MonoBehaviour
{
    private Animator _anim;
    private NewPlayer _player;
    [SerializeField] private BoxCollider2D _bc;

    [SerializeField] private AudioClip[] _hitSound;
    [SerializeField] private AudioClip _treasureHitSound;
    [SerializeField] private float _sfxVol;

    private void Start()
    {
        _anim = GetComponent<Animator>();
        _player = FindObjectOfType<NewPlayer>();
        _bc = GetComponent<BoxCollider2D>();

        _sfxVol = 1.5f;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {

        //items only breakable when raging
        if (other.tag == "Weapon" || other.tag == "Object")
        {
            if (_player.isRaging == true)
            {
                ActivateAnim();
                _player.rageTutorialPlusOne();

                if (this.tag == "Chest")
                {
                    _player.rageTutorialChest();
                    _player.StopActions(true);
                    PlayAudio(_treasureHitSound, _sfxVol);
                }
                else
                {
                    int ran = Random.Range(0, 1);
                    CameraShake.Instance.ShakeCamera(3.0f, 0.3f);
                    PlayAudio(_hitSound[ran], _sfxVol);
                }
                       
                _player.HitItem();
               
            }
        }
        if (other.CompareTag("Bird Cage"))
        {
            ActivateAnim();
            CameraShake.Instance.ShakeCamera(5.0f, 0.1f);
        }
        if (other.CompareTag("Weapon"))
        {
            _bc.enabled = false;
        }
    }

    private void PlayAudio(AudioClip _soundFX, float _sfxVolume)
    {
        if (_soundFX != null)
        {
            AudioManager.Instance.PlayEffect(_soundFX, _sfxVolume);
        }
    }

    public void ActivateAnim() => _anim.SetTrigger("Hit");
}
