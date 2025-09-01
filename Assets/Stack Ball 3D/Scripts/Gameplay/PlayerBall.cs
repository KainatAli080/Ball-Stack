using UnityEngine;

public class PlayerBall : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private bool smash;
    [SerializeField] private bool invisible;
    [SerializeField] private float invisibilityMeter;

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


        // The checker for superpower and how slow time passed
        // if invisibility power reached? Time will decrease slowly as in our fill meter will slowly fizzle out
        // invisibilityMeter is the fill meter for our invisibility power
        if (invisible)
        {
            invisibilityMeter -= Time.deltaTime * 0.35f;  // empty out slowly
        }
        else
        {
            if (smash)
                invisibilityMeter += Time.deltaTime * 0.8f;   // fill up faster when smashing
            else
                invisibilityMeter -= Time.deltaTime * .5f;    // empty out a little faster when idle 
        }


        // invisibilityMeter acts like a fill meter
        // if 1? then invisibility is activated
        // if less? then invisbility power not activated
        if (invisibilityMeter >= 1)
        {
            invisibilityMeter = 1;
            invisible = true;
        }
        else if (invisibilityMeter <= 0)
        {
            invisibilityMeter = 0;
            invisible = false;
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
        // Meaning No Touch on player screen, push player up
        if(!smash)
        {
            rb.linearVelocity = new Vector3(0, 50 * Time.deltaTime * 5, 0);
        }
        // Meaning Player Touch and Collision Detected
        else
        {
            if (invisible)
            {
                // if invibility active, doesn't matter which side we collide with, it will break
                if (collision.gameObject.tag == "enemy" || collision.gameObject.tag == "plane")
                {
                    collision.transform.parent.GetComponent<StackController>().ShatterAllParts();
                }
            }
            else
            {
                if (collision.gameObject.tag == "enemy")
                {
                    collision.transform.parent.GetComponent<StackController>().ShatterAllParts();                    
                }
                else if (collision.gameObject.tag == "plane")
                {
                    Debug.Log("GAME OVER");
                }
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
