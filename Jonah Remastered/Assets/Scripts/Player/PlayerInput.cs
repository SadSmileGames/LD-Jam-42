using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public UnityEngine.Events.UnityEvent Fire1;


    // Update is called once per frame
    void Update ()
    {
        if (Input.GetButtonDown("Fire1"))
            Fire1.Invoke();

        if (Input.GetButton("Fire1"))
            Fire1.Invoke();
    }
}
