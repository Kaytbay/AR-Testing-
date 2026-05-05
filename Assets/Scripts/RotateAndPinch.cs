using UnityEngine;
using UnityEngine.EventSystems;

public class RotateAndPinch : MonoBehaviour
{
    public float rotationSpeed = 0.2f;
    public float zoomSpeed = 0.01f;

    public Vector2 scaleBounds = new Vector2(0.5f, 2.0f);

    private bool isRotating = false;
    private int rotateFingerId = -1;
    private float initialPinchDistance;
    private Vector3 initialScale;

    void Awake()
    {
        // Ensure collider exists for raycasting
        if (GetComponent<Collider>() == null)
            gameObject.AddComponent<BoxCollider>();
    }

    void Update()
    {
        // No touches -> reset state
        if (Input.touchCount == 0)
        {
            isRotating = false;
            rotateFingerId = -1;
            return;
        }

        // Two-finger pinch -> scale
        if (Input.touchCount == 2)
        {
            Touch t0 = Input.GetTouch(0);
            Touch t1 = Input.GetTouch(1);

            if (t1.phase == TouchPhase.Began)
            {
                // Initialize pinch
                initialPinchDistance = Vector2.Distance(t0.position, t1.position);
                initialScale = transform.localScale;
            }
            else if (t0.phase == TouchPhase.Moved || t1.phase == TouchPhase.Moved)
            {
                float currentDistance = Vector2.Distance(t0.position, t1.position);
                float scaleFactor = (currentDistance / initialPinchDistance);
                float targetScale = Mathf.Clamp(initialScale.x * scaleFactor, scaleBounds.x, scaleBounds.y);
                transform.localScale = Vector3.one * targetScale;
            }
            return; // skip rotation when pinching
        }

        // Single-finger rotate
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            // 1. Prevent raycasting if the touch started on a UI Canvas element
            if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                return;

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    Ray ray = Camera.main.ScreenPointToRay(touch.position);

                    if (Physics.Raycast(ray, out RaycastHit hit))
                    {
                        // 2. Use IsChildOf: This ensures the touch registers whether it 
                        // hits the main parent object or any of its child meshes/colliders.
                        if (hit.transform.IsChildOf(transform))
                        {
                            isRotating = true;
                            rotateFingerId = touch.fingerId;
                        }
                    }
                    break;

                case TouchPhase.Moved:
                    if (isRotating && touch.fingerId == rotateFingerId)
                    {
                        // Note: touch.deltaPosition is measured in screen pixels. 
                        // On high-res mobile screens, this value is massive. Ensure your 
                        // 'rotationSpeed' variable in the Inspector is set to a very small decimal (e.g., 0.1f).
                        float deltaX = touch.deltaPosition.x * rotationSpeed;
                        transform.Rotate(0f, -deltaX, 0f, Space.World);
                    }
                    break;

                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    if (touch.fingerId == rotateFingerId)
                    {
                        isRotating = false;
                        rotateFingerId = -1;
                    }
                    break;
            }
        }
    }
}