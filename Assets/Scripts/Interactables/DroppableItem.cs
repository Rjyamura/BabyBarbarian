﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppableItem : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    private Animator _anim;
    private EventManager _eventManager;
    private Collider2D _water;
    private CapsuleCollider2D _c2D;
    private ButtonSound _buttonSound;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _c2D = GetComponent<CapsuleCollider2D>();
        if (_c2D == null)
            Debug.Log("Collider is NULL");

        _buttonSound = FindObjectOfType<ButtonSound>();
        if (_buttonSound == null)
            Debug.Log("Button sound NULL");

        _water = GameObject.Find("Water").GetComponent<Collider2D>();
        _eventManager = EventManager.Instance != null ? EventManager.Instance : FindObjectOfType<EventManager>();
        if (_eventManager == null)
            Debug.Log("Event Manager is null");
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Weapon"))
        {
            _rb.gravityScale = 1.0f;
            _anim.enabled = false;
          

            switch (gameObject.name)
            {
                case "Bird_Cage":
                    _c2D.isTrigger = false;
                    _eventManager.BirdCageKnockedDown();
                    CameraShake.Instance.ShakeCamera(5.0f, .1f);
                    break;

                case "Crate":
                    UpdateHelpTxtCrate();
                    CameraShake.Instance.ShakeCamera(5.0f, .1f);
                    _buttonSound.CrateSfxOn();
                    break;

                default:
                    Debug.Log("Error: Value NUll");
                    break;
            }
        }
        if (other.CompareTag("Water"))
        {
            Physics2D.IgnoreCollision(_c2D, _water);
        }
        if (other.CompareTag("Floor"))
        {
            CameraShake.Instance.ShakeCamera(5.0f, .1f);
        }
    }

    public void UpdateHelpTxtCrate()
    {
        int helpLvl = 2;
        _eventManager.UpdateHelpText(helpLvl);
    }
}
