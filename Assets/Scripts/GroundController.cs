using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundController : MonoBehaviour
{
    void Update()
    {
        transform.Translate(Vector3.back * SpeedController.speed * Time.deltaTime);
    }
}
