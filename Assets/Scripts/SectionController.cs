using System.Collections.Generic;
using UnityEngine;

public class Sections : MonoBehaviour
{
    private int sectionsCount = 0;
    private int currentObstacleIndex = 0;

    private List<GameObject> obstacles;

    [SerializeField]
    private float sectionSize = 48;

    void Start()
    {
        sectionsCount = GameObject.FindGameObjectsWithTag("Section").Length;
        obstacles = new List<GameObject>();

        foreach (Transform child in transform.GetComponentsInChildren<Transform>(true))
        {
            if (child.CompareTag("Obstacle"))
            { // Excluye al padre principal
                obstacles.Add(child.gameObject);
            }
        }

        randomObstacle();
    }

    public void randomObstacle()
    {
        obstacles[currentObstacleIndex].SetActive(false);

        currentObstacleIndex = Random.Range(0, obstacles.Count);
        obstacles[currentObstacleIndex].SetActive(true);
    }

    void Update()
    {
        transform.Translate(Vector3.back * SpeedController.speed * Time.deltaTime);

        if (transform.localPosition.z <= -sectionSize)
        {
            randomObstacle();
            transform.Translate(Vector3.forward * sectionSize * sectionsCount);
        }
    }
}
