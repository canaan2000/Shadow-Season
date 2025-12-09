using UnityEngine;

public class StickyBall : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.linearVelocity = Vector3.zero;
        rb.useGravity = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<Inventory>().numberOfBalls += 1;
        }
    }
}
