using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private float sizeincrease = 0.25f;

    [SerializeField]
    private float maxSize = 5f;

    [SerializeField]
    private GameObject gummyBearParticles;

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangePlayerSize(int magnitude, bool returnToNormal)
    {
        if (!returnToNormal)
        {
            if (transform.localScale.x < maxSize)
            {
                float currentSizeIncrease = sizeincrease * magnitude;
                transform.localScale = transform.localScale + new Vector3(currentSizeIncrease, currentSizeIncrease, currentSizeIncrease);
            }
        }
        else
        {
            Instantiate(gummyBearParticles, transform.position, Quaternion.identity);
            transform.localScale = Vector3.one;
        }

    }
}
