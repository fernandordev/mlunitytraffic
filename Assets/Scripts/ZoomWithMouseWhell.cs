using UnityEngine;

public class ZoomWithMouseWhell : MonoBehaviour
{
    [SerializeField]
    private float ScrollSpeed = 10f;

    private Camera ZoomCamera;
    
    [SerializeField]
    private float minScroll = 3f;
    [SerializeField]
    private float maxScroll = 13f;

    private void Start()
    {
        ZoomCamera = Camera.main;
    }

    void Update()
    {
        if (ZoomCamera.orthographic)
        {
            ZoomCamera.orthographicSize -= Input.GetAxis("Mouse ScrollWheel") * ScrollSpeed;


            ZoomCamera.orthographicSize = Mathf.Clamp(ZoomCamera.orthographicSize, minScroll, maxScroll);
        } else 
        {
            ZoomCamera.fieldOfView -= Input.GetAxis("Mouse ScrollWheel") * ScrollSpeed;
        }

    }
}