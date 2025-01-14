﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour
{

    private DialogTrigger _dt;
    private EventManager _eventManager;
    private NewPlayer _player;
    private bool _crushDialogActive;
    private bool _helpHintOn;

    [SerializeField] private Dialog _clubFoundDialog;
    [SerializeField] private AudioClip _hitSound;
    [SerializeField] private float _sfxVol;
    [SerializeField] private GameObject _skeletonCrushDT;
    [SerializeField] private GameObject _boulder;

    // Start is called before the first frame update
    void Start()
    {
        _dt = GetComponent<DialogTrigger>();
        _player = FindObjectOfType<NewPlayer>();

        _eventManager = EventManager.Instance != null ? EventManager.Instance : FindObjectOfType<EventManager>();
        if (_eventManager == null)
            Debug.Log("Event Manager is null");

        _eventManager.ClubFound += ClubFound;
        _eventManager.StartGammieScene += TurnOffDialog;

        _skeletonCrushDT.SetActive(false);
        _helpHintOn = true;

        _sfxVol = 1.0f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && _crushDialogActive)
        {
            _skeletonCrushDT.SetActive(false);
        }    
    }

    public void ClubFound()
    {
        _dt.dialog = _clubFoundDialog;
        _helpHintOn = false;
    }

    public void TurnOffDialog() => _dt.TurnOffDialogTrigger();

    public void TurnOffHelpHint() => _helpHintOn = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && _helpHintOn)
        {
            UpdateHelp();
        }

        if(other.CompareTag("Bird Cage"))
        {
            CameraShake.Instance.ShakeCamera(15.0f, 3f);
            _boulder.SetActive(true);
            SkeletonCrush();
            PlayAudio(_hitSound, _sfxVol);
        }
    }

    public void SkeletonCrush()
    {
        _skeletonCrushDT.SetActive(true);
        _crushDialogActive = true;
        _eventManager.CrushedSkeleton();
        Debug.Log("Skeleton dialog triggered crush bird cage");

    } 

    private void PlayAudio(AudioClip _soundFX, float _sfxVolume)
    {
        if (_soundFX != null)
        {
            AudioManager.Instance.PlayEffect(_soundFX, _sfxVolume);
        }
    }

    private void UpdateHelp()
    {
        int helpLvl = 6;
        _helpHintOn = false;
        _eventManager.UpdateHelpText(helpLvl);
        Debug.Log("Skeleton level over: " + helpLvl);
    }

    private void OnDestroy()
    {
        _eventManager.ClubFound -= ClubFound;
        _eventManager.StartGammieScene -= TurnOffDialog;
    }
}
