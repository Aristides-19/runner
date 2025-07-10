using System.Collections;
using UnityEngine;

public class PriestFollowsRunner : MonoBehaviour
{
    [SerializeField]
    private Transform runnerTransform;

    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        StartCoroutine(waitForIdle());
    }

    IEnumerator waitForIdle()
    {
        yield return new WaitForSeconds(TimeController.idleTime);
        animator.SetBool("isIdle", false);
    }

    void Update()
    {
        transform.position = new Vector3(
            runnerTransform.position.x,
            transform.position.y,
            transform.position.z
        );
    }
}
