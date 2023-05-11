using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public List<GameObject> objectsToCreate;
    [SerializeField]
    private float interval = 5f;

    float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= interval)
        {
            int randomIndex = Random.Range(0, objectsToCreate.Count);
            GameObject objectToCreate = Instantiate(objectsToCreate[randomIndex], transform);
            DestroyTimer destroyTimer = objectToCreate.AddComponent<DestroyTimer>();

            destroyTimer.timer = 50f;

            timer -= interval;
        }
    }
}
