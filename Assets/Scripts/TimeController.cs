using System.Collections;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    private float initialTime;
    public static float idleTime = 5f;
    public static bool canUpdate = false;

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
        float elapsedTime = Time.time - initialTime;
        Debug.Log("Elapsed Time: " + elapsedTime + " seconds");

        SpeedController.speed += SpeedController.acceleration * Time.deltaTime;
        SpeedController.speed = Mathf.Clamp(SpeedController.speed, 10f, SpeedController.maxSpeed);
        Debug.Log("Current Speed: " + SpeedController.speed);
    }

    IEnumerator waitForSpeed(float secs)
    {
        yield return new WaitForSeconds(secs);
        initialTime = Time.time;
        for (int i = 0; i < 40; i++)
        {
            SpeedController.speed += 0.25f;
            yield return new WaitForSeconds(0.1f);
        }
        canUpdate = true;
    }
}
