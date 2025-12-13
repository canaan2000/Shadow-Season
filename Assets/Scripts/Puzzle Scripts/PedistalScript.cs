using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class PedistalScript : MonoBehaviour
{
    public GameObject requiredBall;
    public GameObject placedBall;
    public Transform heldBallPos;
    public bool isActivated = false;

    public Color pedestalColor;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pedestalColor = requiredBall.GetComponent<Renderer>().sharedMaterial.color;

        this.gameObject.GetComponent<Renderer>().material.color = pedestalColor;
    }

    // Update is called once per frame
    void Update()
    {
        if (placedBall != null)
        {
            placedBall.transform.position = heldBallPos.position;
            if (placedBall.tag == requiredBall.tag)
            {
                isActivated = true;
                KeepBall();
            }
        }

        if (placedBall == null || placedBall.tag != requiredBall.tag)
        {
            isActivated = false;
        }
    }

    void KeepBall()
    {
        placedBall.GetComponent<Rigidbody>().isKinematic = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Green Ball") || collision.gameObject.CompareTag("Red Ball") || collision.gameObject.CompareTag("Blue Ball"))
        {
            if (placedBall == null)
            {
                placedBall = collision.gameObject;
            }
        }
    }
}
