using UnityEngine;

public class GroundController : MonoBehaviour
{
    [SerializeField]
    private int groundLength = 845;

    void Update()
    {
        transform.Translate(Vector3.back * SpeedController.speed * Time.deltaTime);

        if (transform.position.z <= -groundLength)
        {
            transform.Translate(Vector3.forward * groundLength);
        }
    }
}
