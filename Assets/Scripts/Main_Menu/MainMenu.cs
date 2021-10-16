using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    [SerializeField]
    private AudioMixer _audioMixer;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private GameObject fader;

    //private CanvasGroup canvasGroup;

    private void Start()
    {
        //canvasGroup = GetComponent<CanvasGroup>();
        //canvasGroup.interactable = false;
    }

    public void StartGame()
    {
        fader.SetActive(true);

        Animator fadeAnim = fader.GetComponent<Animator>();

        fadeAnim.SetTrigger("LoadScene");

        //LoadScene();
    }
    public void Quit()
    {
        //Application.Quit();

 
            if (Application.platform == RuntimePlatform.WebGLPlayer)
            {
                // Browsers go back to home
                Application.OpenURL("https://mmcrae.itch.io/babybarbariantester");
            }
            else if (Application.platform == RuntimePlatform.WindowsEditor
                || Application.platform == RuntimePlatform.OSXEditor)
            {
                // Editors stop game mode
                UnityEditor.EditorApplication.isPlaying = false;
            }
            else
            {
                // Standalone builds just quit
                Application.Quit();
            }
        
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(7);
    }

    public void LoadCredits()
    {
        SceneManager.LoadScene(9);
    }

}
