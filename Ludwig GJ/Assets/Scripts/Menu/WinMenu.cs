using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinMenu : MonoBehaviour
{

    [SerializeField] public Animator transtion;
    public float transtionTime = 1f;

    public GameObject myFirstSelectedGameObject;

    public AudioSource Ambience;

    public AudioSource fall;

    public TextMeshProUGUI TimeUI;
    public TextMeshProUGUI Deaths;
    public TextMeshProUGUI Fish;

    public Button Play;
    public Button Menubtn;
    public Button Quit;

    private float startTime;
    private float timeforButtons = 3f;
    private bool buttonsEnabled;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        StartCoroutine(FadeAudio.FadeIn(Ambience, 2f));

        startTime = Time.time;

        Play.interactable = false;
        Menubtn.interactable = false;
        Quit.interactable = false;

        Fish.text = FishCollectable.fishCollected.ToString() + "/6";
        TimeUI.text = TimerControl.timerAmount;
        Deaths.text = Player.DeathCount.ToString();

        buttonsEnabled = false;
    }

    private void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(myFirstSelectedGameObject);
        }

        if (Time.time >= startTime + timeforButtons && !buttonsEnabled)
        {
            Play.interactable = true;
            Menubtn.interactable = true;
            Quit.interactable = true;

            buttonsEnabled = true;
        }
    }

    public void OnPlayCLick()
    {

        StartCoroutine(FadeAudio.FadeOut(Ambience, 1f));

        fall.Play();

        StartCoroutine(LoadLevel());


    }

    public void Menu()
    {
        StartCoroutine(FadeAudio.FadeOut(Ambience, 1f));

        StartCoroutine(LoadMenu());
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

    IEnumerator LoadMenu()
    {
        transtion.SetTrigger("Start");

        yield return new WaitForSeconds(transtionTime);

        SceneManager.LoadScene("MainMenu");


    }

}
