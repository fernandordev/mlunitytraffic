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

    void FixedUpdate()
    {
        timer += Time.deltaTime;
        if (timer >= interval)
        {
            // Check if the maximum number of children has been reached
            if (transform.childCount < maxChildren)
            {
                int randomIndex = Random.Range(0, objectsToCreate.Count);
                GameObject objectToCreate = Instantiate(objectsToCreate[randomIndex], transform);

                timer -= interval;
            }
        }
    }
}
