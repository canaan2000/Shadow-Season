using UnityEngine;

public class CarryBallScript : MonoBehaviour
{
    Inventory inventory;

    public Transform carryPoint;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inventory = GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject matchingPrefab = null;

        foreach (GameObject ball in inventory.ballprefabs)
        {
            if (inventory.currentBall != null && inventory.currentBall.name.Contains(ball.name))
            {
                matchingPrefab = ball;
                break;
            }
        }

        if (inventory.ballInventory[matchingPrefab] <= 0)
        {
            ChangeBall();
        }
    }

    public void CarryBall(GameObject ball)
    {
        ChangeBall();

        if (ball != null)
        {
            ball.transform.parent = carryPoint;
            ball.transform.position = carryPoint.position;
            ball.transform.rotation = carryPoint.rotation;
            ball.GetComponent<Rigidbody>().isKinematic = true;
            ball.GetComponent<Collider>().enabled = false;
            switch (ball.tag)
            {
                case "Green Ball":
                    inventory.ballIndex = 0;
                    break;
                case "Red Ball":
                    inventory.ballIndex = 1;
                    break;
                case "Blue Ball":
                    inventory.ballIndex = 2;
                    break;
            }

            inventory.currentBall = ball;
        }
    }

    public void ChangeBall()
    {
        if (carryPoint.childCount == 0)
        {
            return;
        }
        GameObject carriedBall = carryPoint.GetChild(0).gameObject;
        Destroy(carriedBall);
    }
}
