using UnityEngine;
using System.Collections.Generic;

public class CarDestroyer : MonoBehaviour
{
    public bool drawDebugBox = true;
    public string tagToDetect = "AutonomousVehicle";
    public List<GameObject> objectsInsideCollider = new List<GameObject>();
    [SerializeField]
    private float timeToDestroy = 5f;

    void Start()
    {
        // Chama a função DestroyRandomObject() a cada x segundos
        InvokeRepeating("DestroyRandomObject", 50f, timeToDestroy);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(tagToDetect))
        {
            objectsInsideCollider.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(tagToDetect))
        {
            objectsInsideCollider.Remove(other.gameObject);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (drawDebugBox)
        {
            BoxCollider myCollider = GetComponent<BoxCollider>();
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(myCollider.bounds.center, myCollider.bounds.size);
        }
    }


    void DestroyRandomObject()
    {
        // Se houver pelo menos um objeto na lista, escolhe um aleatoriamente para destruir
        if (objectsInsideCollider.Count > 0)
        {
            int randomIndex = Random.Range(0, objectsInsideCollider.Count);
            Destroy(objectsInsideCollider[randomIndex]);
            objectsInsideCollider.RemoveAt(randomIndex);
        }
    }
}
