using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public List<GameObject> objectsToCreate;
    [SerializeField]
    private float interval = 5f;

    [SerializeField]
    private int maxChildren = 12;

    float timer;

    void Update()
    {
        timer += Time.deltaTime;

        // Check if the maximum number of children has been reached
        if (transform.childCount < maxChildren)
        {
            if (timer >= interval)
            {
                int randomIndex = Random.Range(0, objectsToCreate.Count);
                GameObject objectToCreate = Instantiate(objectsToCreate[randomIndex], transform);
                timer = 0f; // Reset the timer
            }
        }
    }
}
