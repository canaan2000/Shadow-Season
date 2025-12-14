using System.Collections.Generic;
using UnityEngine;

public class ResetBallsScript : MonoBehaviour
{
    List<Vector3> startBallPoses = new List<Vector3>();

    public List<Vector3> spawnPoses = new List<Vector3>();

    int spawnIndex = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UniversalBallScript[] startBallScripts = FindObjectsByType<UniversalBallScript>(FindObjectsSortMode.None);

        foreach (UniversalBallScript ballScript in startBallScripts)
        {
            startBallPoses.Add(ballScript.gameObject.transform.position);
        }

        GameObject[] ballSpawnPos = GameObject.FindGameObjectsWithTag("BallSpawnPos");

        foreach (GameObject spawnPos in ballSpawnPos)
        {
            spawnPoses.Add(spawnPos.transform.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetBallToStartPos(GameObject ball)
    {
        if (spawnPoses.Count > 0)
        {
            ball.transform.position = spawnPoses[spawnIndex % spawnPoses.Count];
            spawnIndex++;
            ball.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
        }
        else
        {
            ball.transform.position = startBallPoses[spawnIndex % startBallPoses.Count];
            spawnIndex++;
            ball.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
        }
    }
}
