using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRootMovement : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    void Start()
    {
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        gameObject.transform.position = player.transform.position;
    }
}
