using UnityEditor.Rendering;
using UnityEngine;

public class PedistalScript : MonoBehaviour
{
    public GameObject requiredBall;
    public GameObject placedBall;
    public bool isActivated = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (placedBall != null)
        {
            placedBall.transform.position = transform.position + new Vector3(0, 0.5f, 0);
            if (placedBall.tag == requiredBall.tag)
            {
                KeepBall();
            }
        }
    }

    void KeepBall()
    {
        isActivated = true;
        placedBall.GetComponent<Rigidbody>().isKinematic = true;
        Destroy(placedBall.GetComponentInChildren<Collider>());
    }
}
