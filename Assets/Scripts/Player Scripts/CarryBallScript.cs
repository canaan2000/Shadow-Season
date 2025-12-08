using UnityEngine;

public class CarryBallScript : MonoBehaviour
{
    public Transform carryPoint;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CarryBall(GameObject ball)
    {
        if (ball != null)
        {
            ball.transform.position = carryPoint.position;
            ball.transform.rotation = carryPoint.rotation;
            ball.GetComponent<Rigidbody>().isKinematic = true;
            ball.GetComponent<Collider>().enabled = false;
            ball.transform.parent = carryPoint;
        }
    }
}
