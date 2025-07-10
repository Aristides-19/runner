using UnityEngine;
using UnityEngine.SceneManagement;

public class ColisionPeligro : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Peligro"))
        {
            GameOver();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Peligro"))
        {
            GameOver();
        }
    }

    void GameOver()
    {
        SceneManager.LoadScene("Game Over");
    }
}
