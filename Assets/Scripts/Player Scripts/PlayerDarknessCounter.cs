using UnityEngine;

public class PlayerDarknessCounter : MonoBehaviour
{
    public float darkTillDeath = 5f;
    public float currentDark = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentDark += Time.deltaTime;

        if (currentDark >= darkTillDeath)
        {
            Destroy(gameObject);
            Debug.Log("Player has died from darkness.");
        }
    }
}
