using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallGuy : MonoBehaviour
{
    private Transform objectToDodge;

    [SerializeField]
    private bool runningAway;
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private DetectPlayer detectPlayer;
    [SerializeField]
    private bool greenCandy = false;

    // Update is called once per frame
    void Update()
    {
        RunFromObject();
    }

    private void RunFromObject()
    {
        if (detectPlayer.target != null)
        {
            if (!greenCandy)
            {
                transform.position = Vector3.MoveTowards(transform.position, detectPlayer.target.position, -speed * Time.deltaTime);
                runningAway = true;
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, detectPlayer.target.position, speed * Time.deltaTime);
                runningAway = true;
            }
        }
        else
        {
            runningAway = false;
        }
    }
}
