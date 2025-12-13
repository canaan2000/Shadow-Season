using UnityEngine;

public class EmancipationGrid : MonoBehaviour
{
    InventoryNew inventory;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inventory = FindAnyObjectByType<InventoryNew>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            foreach (var ballType in inventory.ballPrefabs)
            {
                inventory.ballInventory[ballType.tag] = 0;
                Debug.Log(ballType.tag + ", " + inventory.ballInventory[ballType.tag]);
            }
        }
    }
}
