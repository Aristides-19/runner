using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FootstepsAnimationEvent : MonoBehaviour
{
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip[] footstepSounds;

    public void PlayFootstepSound()
    {
        audioSource = GetComponent<AudioSource>();
        if (footstepSounds.Length == 0)
        {
            Debug.LogError("Footstep sounds are required but not assigned", this);
            return;
        }
        audioSource.pitch = Random.Range(0.8f, 1.2f);
        audioSource.PlayOneShot(footstepSounds[Random.Range(0, footstepSounds.Length)]);
    }
}
