using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    [SerializeField] float _cameraFollowSpeed;
    private Camera cam;
    void Awake()
    {
        cam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
    }

    void FixedUpdate()
    {
        Vector3 targetPosition = new Vector3(transform.position.x, transform.position.y, cam.transform.position.z);
        cam.transform.position = Vector3.Lerp(cam.transform.position, targetPosition, _cameraFollowSpeed);
    }
}
