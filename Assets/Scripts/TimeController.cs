using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    private bool canUpdate = false;
    private float initialTime;
    public static float idleTime = 5f;
    public static float elapsedTime = 0f;

    [SerializeField]
    private TextMeshProUGUI timeText;

    void Start()
    {
        SpeedController.speed = 0f;
        Time.timeScale = 1f;
        StartCoroutine(waitForSpeed(idleTime));
    }

    void Update()
    {
        if (!canUpdate)
            return;
        elapsedTime = Time.time - initialTime;
        timeText.text = FormatTime(elapsedTime) + " minutos";

        SpeedController.speed += SpeedController.acceleration * Time.deltaTime;
        SpeedController.speed = Mathf.Clamp(SpeedController.speed, 10f, SpeedController.maxSpeed);
    }

    private string FormatTime(float time)
    {
        TimeSpan t = TimeSpan.FromSeconds(time);
        return string.Format("{0:D2}:{1:D2}", t.Minutes, t.Seconds);
    }

    IEnumerator waitForSpeed(float secs)
    {
        yield return new WaitForSeconds(secs);
        initialTime = Time.time;
        for (int i = 0; i < 40; i++)
        {
            SpeedController.speed += 0.25f;
            elapsedTime = Time.time - initialTime;
            timeText.text = FormatTime(elapsedTime) + " minutos";
            yield return new WaitForSeconds(0.1f);
        }
        canUpdate = true;
    }
}
