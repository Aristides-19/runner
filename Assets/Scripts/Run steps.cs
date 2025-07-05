using UnityEngine;

public class RunningFootsteps : MonoBehaviour
{
    public AudioClip runningSound; // Sonido continuo de correr
    public float startDelay = 2.5f; // Tiempo antes de empezar a sonar
    public float totalDuration = 60f; // Duraci�n total del sonido

    private AudioSource audioSource;
    private bool isRunning = false;
    private float runStartTime;
    private bool soundStarted = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = runningSound;
        audioSource.loop = true; // Para reproducci�n continua
    }

    void Update()
    {
        if (isRunning)
        {
            // Calcular tiempo transcurrido desde que comenz� a correr
            float elapsedTime = Time.time - runStartTime;

            // Si pas� el delay inicial y a�n no empez� el sonido
            if (elapsedTime >= startDelay && !soundStarted)
            {
                audioSource.Play();
                soundStarted = true;
            }

            // Detener despu�s de totalDuration + startDelay
            if (elapsedTime >= totalDuration + startDelay)
            {
                StopRunningSound();
            }
        }
    }

    // Llamar esta funci�n cuando el personaje comienza a correr
    public void StartRunning()
    {
        if (!isRunning)
        {
            isRunning = true;
            soundStarted = false;
            runStartTime = Time.time;
        }
    }

    // Llamar esta funci�n cuando el personaje deja de correr
    public void StopRunningSound()
    {
        isRunning = false;
        soundStarted = false;
        audioSource.Stop();
    }
}