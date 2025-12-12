using UnityEngine;

public class SafeZoneScript : MonoBehaviour
{
    PlayerDarknessCounter playerDarkness;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerDarkness = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerDarknessCounter>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerDarkness.currentDark = 0;
        }
    }
}
