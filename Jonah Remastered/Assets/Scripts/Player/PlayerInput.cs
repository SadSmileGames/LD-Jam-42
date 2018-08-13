using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public UnityEngine.Events.UnityEvent Fire1;

    private bool isInputEnabled;

    private void OnEnable()
    {
        GameController.OnNextWave += DisableInput;
        GameController.OnNextWaveBegin += EnableInput;
    }

    private void OnDisable()
    {
        GameController.OnNextWave -= DisableInput;
        GameController.OnNextWaveBegin += EnableInput;
    }

    // Update is called once per frame
    void Update ()
    {
        if (Input.GetButtonDown("Fire1") && isInputEnabled)
            Fire1.Invoke();

        if (Input.GetButton("Fire1") && isInputEnabled)
            Fire1.Invoke();
    }

    public void EnableInput()
    {
        isInputEnabled = true;
    }

    public void DisableInput()
    {
        isInputEnabled = false;
    }
}
