using UnityEngine;

public class DestroyTimer : MonoBehaviour
{
    public float timer = 50f;

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
