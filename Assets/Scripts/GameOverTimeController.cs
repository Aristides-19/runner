using System;
using TMPro;
using UnityEngine;

public class GameOverTimeController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI timeText;

    void Start()
    {
        timeText.text = FormatTime(TimeController.elapsedTime) + " minutos";
    }

    private string FormatTime(float time)
    {
        TimeSpan t = TimeSpan.FromSeconds(time);
        return string.Format("{0:D2}:{1:D2}", t.Minutes, t.Seconds);
    }
}
