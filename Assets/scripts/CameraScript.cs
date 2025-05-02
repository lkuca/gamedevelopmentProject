using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform player;
    public Transform car;
    private Transform target;

    public Vector3 defaultOffset = new Vector3(0, 0, -10);
    public Vector3 carOffset = new Vector3(0, 2f, -12); // камеру чуть выше и дальше
    private Vector3 currentOffset;

    private bool followCursor = false;
    private Camera cam;

    public float defaultZoom = 5f;     // Стандартное приближение
    public float carZoom = 7f;         // Отдаление, когда в машине
    private float currentZoom;

    void Start()
    {
        target = player;
        currentOffset = defaultOffset;
        cam = Camera.main;
        currentZoom = defaultZoom;

    }

    void Update()
    {
        // Обновляем текущую цель
        if (car.GetComponent<CarController>().enabled)
        {
            target = car;
            currentOffset = Vector3.Lerp(currentOffset, carOffset, Time.deltaTime * 2f);
            currentZoom = Mathf.Lerp(currentZoom, carZoom, Time.deltaTime * 2f);
        }
        else
        {
            target = player;
            currentOffset = Vector3.Lerp(currentOffset, defaultOffset, Time.deltaTime * 2f);
            currentZoom = Mathf.Lerp(currentZoom, defaultZoom, Time.deltaTime * 2f);
        }
        Camera.main.orthographicSize = currentZoom;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            followCursor = true;
        }
        else
        {
            followCursor = false;
        }

        if (followCursor)
        {
            MoveCameraWithMouse();
        }
        else
        {
            FollowTarget();
        }
    }

    void FollowTarget()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + currentOffset, Time.deltaTime * 5f);
    }

    void MoveCameraWithMouse()
    {
        Vector3 cursorWorldPos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.nearClipPlane));
        transform.position = Vector3.Lerp(transform.position, cursorWorldPos, Time.deltaTime * 5f);
    }
}
