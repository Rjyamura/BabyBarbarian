using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonCrush : MonoBehaviour
{

    private DialogTrigger _dt;
    private EventManager _eventManager;

    // Start is called before the first frame update
    void Start()
    {
        _dt = GetComponent<DialogTrigger>();

        _eventManager = EventManager.Instance != null ? EventManager.Instance : FindObjectOfType<EventManager>();
        if (_eventManager == null)
            Debug.Log("Event Manager is null");

        _eventManager.SkeletonCrushed += TriggerCrushDialog;
    }

    public void TriggerCrushDialog() =>  _dt.TriggerDialog();

    private void OnDestroy()
    {
        _eventManager.SkeletonCrushed -= TriggerCrushDialog;
    }

}
