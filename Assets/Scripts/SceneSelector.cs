using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneSelector : MonoBehaviour
{
    private Canvas _canvas;
    private CanvasScaler _canvasScaler;
    private NewPlayer _player;
    private Gammie _gammie;
    private WaterTrigger _water;
    private EndTrigger _endTrigger;
    private TutorialRage _tutorialRage;
    private EventManager _eventManager;
    private BjornArmActivate _bjornActivate;
    private Skeleton _skeleton;

    private void Start()
    {
        _canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        _canvasScaler = GameObject.Find("Canvas").GetComponent<CanvasScaler>();
        _player = FindObjectOfType<NewPlayer>();
        _gammie = FindObjectOfType<Gammie>();
        _water = FindObjectOfType<WaterTrigger>();
        _endTrigger = FindObjectOfType<EndTrigger>();
        _tutorialRage = FindObjectOfType<TutorialRage>();
        _bjornActivate = FindObjectOfType<BjornArmActivate>();

        _eventManager = EventManager.Instance != null ? EventManager.Instance : FindObjectOfType<EventManager>();
        if (_eventManager == null)
            Debug.Log("Event Manager is NULL");

    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void IntroComplete()
    {
        SceneManager.LoadScene(2);
    }
    public void FoundClub()
    {
        _player.StopActions(true);
        SceneManager.LoadScene(8, LoadSceneMode.Additive);
        _canvas.enabled = false;
        _canvasScaler.enabled = false;
        _player.enabled = false;
    }
    public void UnloadClubScene()
    {
        _player.StopActions(false);
        SceneManager.UnloadSceneAsync(8);
        _canvas.enabled = true;
        _canvasScaler.enabled = true;
        _player.enabled = true;
    }
    public void FreeGammyScene()
    {
        _player.StopActions(true);
        _gammie.TransformtoGammie();
        StartCoroutine(LoadGammyCoroutine());
    }

    IEnumerator LoadGammyCoroutine()
    {
        _player.StopActions(true);
        yield return new WaitForSeconds(3.0f);
        _player.StopActions(true);
        SceneManager.LoadScene(10, LoadSceneMode.Additive);
       _canvas.enabled = false;
  //    _player.enabled = false;

    }

    public void UnloadFreeGammy()
    {
        _player.BromMovePos();
        _player.StopActions(false);
        SceneManager.UnloadSceneAsync(10);
        _canvas.enabled = true;
        _canvasScaler.enabled = true;
        _eventManager.FreeGammieSceneActivated();
        //_gammie.TransformtoGammie();
        _player.TeachBromRage();
        _tutorialRage.startTutorialRage();
        _player.enabled = true;
    }
    public void WaterScene()
    {

        _player.StopActions(true);
        AudioManager.Instance.SfxSwitch(false);
        SceneManager.LoadScene(11, LoadSceneMode.Additive);
        _canvas.enabled = false;
        _canvasScaler.enabled = false;
  //      _player.enabled = false;

        //deactivates all interactables in scene
        GameObject[] interactables = GameObject.FindGameObjectsWithTag("Interactable");
        foreach(GameObject interactable in interactables)
        {
            interactable.SetActive(false);
        }
    }
    public void UnloadWaterScene()
    {
        _player.StopActions(false);
        _player.setPlayerColorDefault();
        SceneManager.UnloadSceneAsync(11);
        _canvas.enabled = true;
        _canvasScaler.enabled = true;
        _water.WaterTriggerStart();
        _bjornActivate.ActivateBjorn();
        _player.enabled = true;
        _eventManager.WaterSceneActivated();
    }
    public void EndScene()
    {
        _player.StopActions(true);
        SceneManager.LoadScene(13, LoadSceneMode.Additive);
        AudioManager.Instance.LoopEffectStop();
        _canvas.enabled = false;
        _canvasScaler.enabled = false;
        _player.enabled = false;
    }
    public void ToBeContinuedScene()
    {
        SceneManager.LoadScene(12);
    }

    public void CreditsScene()
    {
        SceneManager.LoadScene(9);
    }
}
