using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sections : MonoBehaviour{
    private int sectionsCount = 0;
    public List<GameObject> obstacles;
    
    private float speed = 10;
    public float sectionSize = 0;
    void Start(){
        sectionsCount = GameObject.FindGameObjectsWithTag("Section").Length;
        Debug.Log(sectionsCount);
        obstacles = new List<GameObject>();

        foreach (Transform child in transform.GetComponentsInChildren<Transform>(true))
        {
            if (child.CompareTag("Obstacle"))
            { // Excluye al padre principal
                obstacles.Add(child.gameObject);
            }
        }

        Debug.Log(obstacles.Count);

        EnableRamdomObstacle();
    }

    public void EnableRamdomObstacle() { 
        foreach (GameObject obstacle in obstacles){
            obstacle.SetActive(false);
        }

        int ramdomIndex = Random.Range(0, obstacles.Count);
        obstacles[ramdomIndex].SetActive(true);

    }

    void Update(){
        transform.Translate(Vector3.back * speed * Time.deltaTime);

        if(transform.position.z <= -sectionSize)
        {
            transform.Translate(Vector3.forward * sectionSize * sectionsCount);
        }
    }

}
