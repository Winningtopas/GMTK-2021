using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayer : MonoBehaviour
{

    public Transform target = null;
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") target = other.transform;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player") target = null;
    }

    //// Update is called once per frame
    //void Update()
    //{
    //    if (target == null) return;
    //}
}
