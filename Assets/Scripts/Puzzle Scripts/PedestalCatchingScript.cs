using UnityEngine;

public class PedestalCatchingScript : MonoBehaviour
{
    PedistalScript PedistalScript;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PedistalScript = GetComponent<PedistalScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Green Ball") || collision.gameObject.CompareTag("Red Ball") || collision.gameObject.CompareTag("Blue Ball"))
        {
            

            Destroy(collision.gameObject);
        }
    }
}
