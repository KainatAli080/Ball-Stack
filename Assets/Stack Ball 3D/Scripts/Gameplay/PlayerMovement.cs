using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Ball Speed Settings")]
    public float fallForce = 10f;
    public float bounceForce = 5f;

    private Rigidbody rb;
    private bool isPressing = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Getting input in Update
        isPressing = Input.GetMouseButtonDown(0);
    }

    // Applying physics in FixedUpdate
    private void FixedUpdate()
    {
        // is player pressing mouse or screen, apply dowbnward force
        if (isPressing)
        {
            rb.AddForce(Vector3.down * fallForce, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Finish"))
        {
            // Testing bounceforce
            rb.AddForce(Vector3.up * bounceForce, ForceMode.Impulse);
        }
    }
}
