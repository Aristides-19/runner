using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Footsteps : MonoBehaviour
{
    public AudioClip[] footstepSounds;
    public float stepInterval = 0.4f;
    public float minVelocity = 0.1f;

    private AudioSource audioSource;
    private CharacterController characterController;
    private float nextStepTime;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        characterController = GetComponent<CharacterController>();
        nextStepTime = stepInterval; // Inicializa el primer paso
    }

    void Update()
    {
        ProgressStepCycle();
    }

    private void ProgressStepCycle()
    {
        if (characterController.velocity.sqrMagnitude > minVelocity && characterController.isGrounded)
        {
            nextStepTime -= Time.deltaTime; // Decrementa el temporizador

            if (nextStepTime <= 0)
            {
                PlayFootstepSound();
                nextStepTime = stepInterval; // Reinicia el temporizador
            }
        }
    }

    private void PlayFootstepSound()
    {
        if (footstepSounds.Length == 0) return;

        int randomIndex = Random.Range(0, footstepSounds.Length);
        audioSource.clip = footstepSounds[randomIndex];
        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.PlayOneShot(audioSource.clip); // Usa PlayOneShot para evitar cortes
    }
}