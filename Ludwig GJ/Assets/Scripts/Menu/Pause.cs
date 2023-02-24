using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static System.TimeZoneInfo;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{

    [SerializeField] GameObject PlayerGO;

    PlayerInputHandler playerInput;

    public GameObject pauseMenuUI;

 
    bool IsPaused;

    public static bool canPause;

    public GameObject FirstSelectedGameObject;

    [SerializeField] public Animator transtion;

    public float transtionTime = 3f;

    private void Start()
    {

        playerInput = PlayerGO.GetComponent<PlayerInputHandler>();

        canPause = true;

    }

    private void Update()
    {


        if (canPause)
        {

            if (playerInput.PauseInput)
            {
                if (IsPaused)
                {
                    Resume();
                }
                else
                {
                    Paused();

                    if (EventSystem.current.currentSelectedGameObject == null)
                    {
                        EventSystem.current.SetSelectedGameObject(FirstSelectedGameObject);
                    }
                }
            }
        }

    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        IsPaused = false;
        playerInput.CanUseInput = true;
        playerInput.PauseInput = false;
    }

    public void Paused()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        IsPaused = true;
        playerInput.CanUseInput = false;
        playerInput.PauseInput = false;

    }

    public void Menu()
    {
        StartCoroutine(IReturnToMenu());
    }

    IEnumerator IReturnToMenu()
    {
        transtion.SetTrigger("Start");

        yield return new WaitForSecondsRealtime(transtionTime);

        Time.timeScale = 1f;

        SceneManager.LoadScene("MainMenu");


    }

}
