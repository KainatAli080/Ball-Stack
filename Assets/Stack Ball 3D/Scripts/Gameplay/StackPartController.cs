using UnityEngine;

// controller of each individual piece of that stack.
public class StackPartController : MonoBehaviour
{
    private Rigidbody rigidBodyComponent;
    private MeshRenderer meshRendererComponent;
    private StackController stackControllerScript;
    private Collider colliderComponent;

    private void Awake()
    {
        rigidBodyComponent = GetComponent<Rigidbody>();
        meshRendererComponent = GetComponent<MeshRenderer>();
        stackControllerScript = GetComponent<StackController>();
        colliderComponent = GetComponent<Collider>();
    }

    public void ShatterObject()
    {
        rigidBodyComponent.isKinematic = false; // meaning physics would no longer affact body
        colliderComponent.enabled = false;

        Vector3 forcePoint = transform.parent.position;
        float parentXpos = transform.parent.position.x;
        float Xpos = meshRendererComponent.bounds.center.x;

        Vector3 subDirection = (parentXpos - Xpos < 0) ? Vector3.right : Vector3.left;
        Vector3 direction = (Vector3.up * 1.5f + subDirection).normalized;

        float force = Random.Range(20, 35);
        float torque = Random.Range(110, 180);

        rigidBodyComponent.AddForceAtPosition(direction * force, forcePoint, ForceMode.Impulse);
        rigidBodyComponent.AddTorque(Vector3.left * torque);
        rigidBodyComponent.linearVelocity = Vector3.down;
    }

    public void RemoveAllChildren()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).SetParent(null);
            i--;
        }
    }
}
