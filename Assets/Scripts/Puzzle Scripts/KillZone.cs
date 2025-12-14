using UnityEngine;
using UnityEngine.SceneManagement;

public class KillZone : MonoBehaviour
{
    ResetBallsScript resetBallsScript;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        resetBallsScript = FindAnyObjectByType<ResetBallsScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (other.gameObject.CompareTag("ball1"))
        {
            resetBallsScript.ResetBallToStartPos(other.gameObject.transform.parent.gameObject);
        }
    }
}
