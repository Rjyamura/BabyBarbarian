﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class EventManager : MonoBehaviour
{
    private static EventManager _instance;
    public static EventManager Instance { get { return _instance; } }


    void Awake()
    {
        if (_instance != null && _instance != this)
           Destroy(this);
        
        _instance = this;
    }

    public event Action<bool> DialogActive;
    public void DialogActivated(bool isActive)
    {
        if (DialogActive != null)
            DialogActive(isActive);
    }
    
    public event Action<int> NewHelpText;
    public void UpdateHelpText(int helpTxtNum)
    {
        if (NewHelpText != null)
            NewHelpText(helpTxtNum);
    }


    public event Action ClubFound;
    public void FoundClub()
    {
        if (ClubFound != null)
            ClubFound();
    }

    public event Action SkeletonCrushed;
    public void CrushedSkeleton()
    {
        if (SkeletonCrushed != null)
            SkeletonCrushed();
    }

    public Action StartGammieScene;
    public void BirdCageKnockedDown()
    {
        if(StartGammieScene != null)
            StartGammieScene();
        
    }

    public event Action FreeGammieSceneActive;
    public void FreeGammieSceneActivated()
    {
        if (FreeGammieSceneActive != null)
            FreeGammieSceneActive();
    }


    public event Action CageFallActive;
    public void CageFall()
    {
        if (CageFallActive != null)
            CageFallActive();
    }


    public event Action StartRage;
    public void GammyTaughtRage()
    {
        if (StartRage != null)
            StartRage();
    }

    public event Action WaterSceneTriggered;
    public void TriggerWaterScene()
    {
        if (WaterSceneTriggered != null)
            WaterSceneTriggered();
    }


    public event Action WaterSceneActive;
    public void WaterSceneActivated()
    {
        if (WaterSceneActive != null)
            WaterSceneActive();
    }
}

