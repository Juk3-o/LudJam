using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    [SerializeField] public Animator transtion;
    public float transtionTime = 1f;

    public GameObject myFirstSelectedGameObject;

    public AudioSource Music;
    public AudioSource Ambience;

    public AudioSource fall;
        
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(myFirstSelectedGameObject);
        }
    }

    public void OnPlayCLick()
    {

        StartCoroutine(FadeAudio.FadeOut(Music, 1f));
        StartCoroutine(FadeAudio.FadeOut(Ambience, 1f));

        fall.Play();

        StartCoroutine(LoadLevel());


    }


    public void QuitGame()
    {
        Debug.Log("Game Quit");
        Application.Quit();
    }


    IEnumerator LoadLevel()
    {
        transtion.SetTrigger("Start");

        yield return new WaitForSeconds(transtionTime);

        SceneManager.LoadScene("Scene1");


    }
}
