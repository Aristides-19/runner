using UnityEngine;

public class RunningFootsteps : MonoBehaviour
{
    public AudioClip runningSound; // Sonido continuo de correr
    public float startDelay = 2.5f; // Tiempo antes de empezar a sonar
    public float totalDuration = 60f; // Duración total del sonido

    private AudioSource audioSource;
    private bool isRunning = false;
    private float runStartTime;
    private bool soundStarted = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = runningSound;
        audioSource.loop = true; // Para reproducción continua
    }

    void Update()
    {
        if (isRunning)
        {
            // Calcular tiempo transcurrido desde que comenzó a correr
            float elapsedTime = Time.time - runStartTime;

            // Si pasó el delay inicial y aún no empezó el sonido
            if (elapsedTime >= startDelay && !soundStarted)
            {
                audioSource.Play();
                soundStarted = true;
            }

            // Detener después de totalDuration + startDelay
            if (elapsedTime >= totalDuration + startDelay)
            {
                StopRunningSound();
            }
        }
    }

    // Llamar esta función cuando el personaje comienza a correr
    public void StartRunning()
    {
        if (!isRunning)
        {
            isRunning = true;
            soundStarted = false;
            runStartTime = Time.time;
        }
    }

    // Llamar esta función cuando el personaje deja de correr
    public void StopRunningSound()
    {
        isRunning = false;
        soundStarted = false;
        audioSource.Stop();
    }
}