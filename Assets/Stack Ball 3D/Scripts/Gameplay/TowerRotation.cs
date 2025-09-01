using UnityEngine;

public class TowerRotation : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 10f;

    private void Update()
    {
        transform.Rotate(0, -rotationSpeed * Time.deltaTime, 0);
    }
}
