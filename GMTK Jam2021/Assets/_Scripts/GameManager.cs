using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public int candyCount = 0;
    public bool ballMode;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ManageResources(int candyAmount, bool badCandy)
    {
        if (badCandy)
        {
            ballMode = false;
            candyCount = 0;
            player.GetComponent<PlayerManager>().ChangePlayerSize(0, true);
            player.GetComponent<PlayerMovement>().AcitivateWalkMode();
        }
        else
        {
            candyCount += candyAmount;
            player.GetComponent<PlayerManager>().ChangePlayerSize(candyAmount, false);
            if (candyCount >= 5 && !ballMode)
            {
                ballMode = true;
                player.GetComponent<PlayerMovement>().AcitivateBallMode();
            }
        }

    }
}
