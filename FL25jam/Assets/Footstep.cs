using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterFootsteps : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public AudioSource woodenFootstep;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            woodenFootstep.enabled = true;
        }
        else
        {
            woodenFootstep.enabled = false;
        }
    }
}
