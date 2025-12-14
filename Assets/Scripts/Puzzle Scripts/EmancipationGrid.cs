using UnityEngine;

public class EmancipationGrid : MonoBehaviour
{
    InventoryNew inventory;
    ResetBallsScript resetBallsScript;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inventory = FindAnyObjectByType<InventoryNew>();
        resetBallsScript = FindAnyObjectByType<ResetBallsScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // Reset all balls in inventory if player enters the emancipation grid
        if (other.gameObject.CompareTag("Player"))
        {
            foreach (var ballType in inventory.ballPrefabs)
            {
                if (inventory.ballInventory[ballType.tag] > 0)
                {
                    for (int i = 0; i < inventory.ballInventory[ballType.tag]; i++)
                    {
                        GameObject ballToReset = Instantiate(ballType);

                        resetBallsScript.ResetBallToStartPos(ballToReset);
                    }

                    inventory.ballInventory[ballType.tag] = 0;
                }

                Debug.Log(ballType.tag + ", " + inventory.ballInventory[ballType.tag]);
            }
        }

        // Reset individual balls that enter the emancipation grid
        if (other.gameObject.CompareTag("ball1"))
        {
            resetBallsScript.ResetBallToStartPos(other.gameObject.transform.parent.gameObject);
        }
    }
}
