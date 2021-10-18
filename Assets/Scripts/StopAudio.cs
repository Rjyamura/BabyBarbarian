using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopAudio : MonoBehaviour
{
    private AudioSource _audioSource_1;
    private AudioSource _audioSource_2;

    // Start is called before the first frame update
    void Start()
    {
        _audioSource_1 = GameObject.Find("BGM").GetComponent<AudioSource>();
        _audioSource_1 = GameObject.Find("BGM_2").GetComponent<AudioSource>();

        _audioSource_1.Stop();
        _audioSource_2.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
