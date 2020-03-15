using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BinarySwitchEvents : MonoBehaviour
{
    [SerializeField] private bool state = false;

    public UnityEvent onEvent;
    public UnityEvent offEvent;

    private void OnMouseUpAsButton()
    {
        state = !state;

        switch (state)
        {
            case false: offEvent.Invoke();
                break;
            case true: onEvent.Invoke();
                break;
        }
    }
}
