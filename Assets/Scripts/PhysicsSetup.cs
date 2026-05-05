using UnityEngine;

public class PhysicsSetup : MonoBehaviour
{
    
    public GameObject target;

    public Vector3 colliderSize = new Vector3(1f, 1f, 1f);

    public Vector3 initialScale = Vector3.one * 1.0f;

    void Start()
    {
        BoxCollider box = target.AddComponent<BoxCollider>();  
        box.size = colliderSize;

        target.transform.localScale = initialScale;  
    }
}
