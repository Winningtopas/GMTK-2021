using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColliders : MonoBehaviour
{
    [SerializeField]
    private GameObject gameManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
            gameManager.GetComponent<GameManager>().ManageResources(0, true);
            Destroy(other.gameObject);
        }
    }
}
