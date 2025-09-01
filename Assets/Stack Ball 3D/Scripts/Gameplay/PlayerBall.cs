using UnityEngine;

public class PlayerBall : MonoBehaviour
{
    private Rigidbody rb;
    public bool smash;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Getting input in Update
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            smash = true;
        }
        if(Input.GetMouseButtonUp(0))
        {
            smash = false;
        }
    }

    // Applyying physics in FiedUpdate
    private void FixedUpdate()
    {
        if(smash)
        {
            rb.linearVelocity = new Vector3(0, -100 * Time.fixedDeltaTime * 7, 0);
        }

        // Stopping velocty from going beyond 5
        if (rb.linearVelocity.y > 5)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 5, rb.linearVelocity.z);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Meaning No Touch
        if(!smash)
        {
            rb.linearVelocity = new Vector3(0, 50 * Time.deltaTime * 5, 0);
        }
        // Meaning Touch and Collision Detected
        else
        {
            if (collision.gameObject.tag == "enemy")
            {
                Destroy(collision.transform.parent.gameObject);
            }
            else if (collision.gameObject.tag == "plane")
            {
                Debug.Log("GAME OVER");
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (!smash || collision.gameObject.tag == "Finish")
        {
            rb.linearVelocity = new Vector3(0, 50 * Time.deltaTime * 5, 0);
        }
    }
}
