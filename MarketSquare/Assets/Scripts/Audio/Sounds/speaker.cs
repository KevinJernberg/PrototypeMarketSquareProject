using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class speaker : MonoBehaviour
{
    private void Start()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.speakersAudio, this.transform.position);
    }
}
