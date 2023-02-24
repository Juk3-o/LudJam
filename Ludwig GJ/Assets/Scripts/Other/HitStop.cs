using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitStop : MonoBehaviour
{
    public float Duration = 0.1f;
    private float pendingFreezeDuration = 0f;
    private bool isFrozen = false;

    void Update()
    {
        if (pendingFreezeDuration > 0 && !isFrozen)
        {
            StartCoroutine(DoFreeze());
        }
    }

    public void Freeze()
    {
        pendingFreezeDuration = Duration;
    }

    IEnumerator DoFreeze()
    {
        isFrozen = true;

        var original = Time.timeScale;
        Time.timeScale = 0f;

        yield return new WaitForSecondsRealtime(Duration);

        Time.timeScale = original;

        pendingFreezeDuration = 0;
        isFrozen = false;
    }
}
