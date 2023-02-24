using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.TimeZoneInfo;
using UnityEngine.SceneManagement;
using TMPro;

public class WinCollider : MonoBehaviour
{

    [SerializeField] public Animator transtion;

    public float transtionTime = 3f;

    public TextMeshProUGUI Timer;

    TimerControl timerControl;

    public GameObject Menu;

    private void Start()
    {
        timerControl = Menu.GetComponent<TimerControl>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            StartCoroutine(IWin());
        
            TimerControl.timerAmount = Timer.text;

            timerControl.EndTimer();


        }
    }

    IEnumerator IWin()
    {
        transtion.SetTrigger("Start");

        yield return new WaitForSecondsRealtime(transtionTime);

        SceneManager.LoadScene("WinScene");


    }

}
