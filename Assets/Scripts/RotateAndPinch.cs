using UnityEngine;
using UnityEngine.EventSystems;

public class RotateAndPinch : MonoBehaviour
{
    public float rotationSpeed = 0.2f;
    public Vector2 scaleBounds = new Vector2(0.5f, 2.0f);

    private float initialPinchDistance;
    private Vector3 initialScale;

    void Update()
    {
        #region lap test

#if UNITY_EDITOR
        // --- 1. Testing Rotation (Click & Drag) ---
        if (Input.GetMouseButton(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    if (hit.transform.IsChildOf(transform))
                    {
                        float deltaX = Input.GetAxis("Mouse X") * rotationSpeed * 10f;
                        transform.Rotate(0f, -deltaX, 0f, Space.World);
                    }
                }
            }
        }

        // --- 2. Testing Scale / Pinch (Mouse Scroll Wheel) ---
        float scroll = Input.mouseScrollDelta.y;
        if (scroll != 0f)
        {
            float scaleFactor = scroll * 0.05f;
            Vector3 newScale = transform.localScale + new Vector3(scaleFactor, scaleFactor, scaleFactor);
            if (newScale.x > 0.1f)
            {
                transform.localScale = newScale;
            }
        }
#endif
        #endregion

        // Two-finger pinch -> scale
        if (Input.touchCount == 2)
        {
            Touch t0 = Input.GetTouch(0);
            Touch t1 = Input.GetTouch(1);

            if (t1.phase == TouchPhase.Began)
            {
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
            return;
        }




        // Single-finger rotate
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                return;

            if (touch.phase == TouchPhase.Moved)
            {
                float deltaX = touch.deltaPosition.x * rotationSpeed;
                transform.Rotate(0f, -deltaX, 0f, Space.World);
            }
        }
    }
}