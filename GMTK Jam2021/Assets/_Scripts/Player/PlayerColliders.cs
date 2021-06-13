using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerColliders : MonoBehaviour
{
    [SerializeField]
    private GameObject gameManager;
    [SerializeField]
    private float wallVelocity;

    private Rigidbody rb;
    private bool isColliding;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        isColliding = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Candy")
        {
            gameManager.GetComponent<GameManager>().ManageResources(1, false);
            Destroy(other.gameObject);
        }
        if (other.tag == "BadCandy")
        {
            if (isColliding) return;
            isColliding = true;

            Destroy(other.gameObject);
            gameManager.GetComponent<GameManager>().ManageResources(0, true);
            Debug.Log("Collide bad candy");
        }

        if (other.tag == "PushableWall" && rb.velocity.magnitude > 30f)
        {
            if (GetComponent<PlayerMovement>().ballMode)
            {
                other.GetComponent<Rigidbody>().AddForce(Vector3.left * wallVelocity, ForceMode.Impulse);
            }
        }

        if(other.tag == "Finish")
        {
            StartCoroutine(StartNextLevel());
        }

        if (other.tag == "ChocolateMilk")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }


    private IEnumerator StartNextLevel()
    {

        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
