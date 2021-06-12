using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    public int candyCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ManageResources(int candyAmount)
    {
        candyCount += candyAmount;
        player.GetComponent<PlayerManager>().IncreasePlayerSize(candyAmount);
        if (candyCount >= 5)
        {
            player.GetComponent<PlayerMovement>().AcitivateBallMode();
        }
    }
}
