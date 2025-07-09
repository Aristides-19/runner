using UnityEngine;

public class TimeController : MonoBehaviour
{
    private float initialTime;

    void Start()
    {
        Time.timeScale = 1f;
        initialTime = Time.time;
    }

    void Update()
    {
        float elapsedTime = Time.time - initialTime;
        Debug.Log("Elapsed Time: " + elapsedTime + " seconds");

        SpeedController.speed += SpeedController.acceleration * Time.deltaTime;
        SpeedController.speed = Mathf.Clamp(SpeedController.speed, 10f, SpeedController.maxSpeed);
        Debug.Log("Current Speed: " + SpeedController.speed);
    }
}
