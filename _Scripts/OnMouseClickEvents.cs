using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnMouseClickEvents : MonoBehaviour
{
    public UnityEvent onMouseEnterEvent;
    public UnityEvent onMouseOverEvent;
    public UnityEvent onMouseExitEvent;
    public UnityEvent onMouseDownEvent;
    public UnityEvent onMouseUpEvent;
    public UnityEvent onMouseUpAsButtonEvent;


    public void OnMouseEnter() { onMouseEnterEvent.Invoke(); }
    public void OnMouseOver() { onMouseOverEvent.Invoke(); }
    public void OnMouseExit() { onMouseExitEvent.Invoke(); }
    public void OnMouseDown() { onMouseDownEvent.Invoke(); }
    public void OnMouseDrag() { onMouseOverEvent.Invoke(); }
    public void OnMouseUp() { onMouseUpEvent.Invoke(); }
    public void OnMouseUpAsButton() { onMouseUpAsButtonEvent.Invoke(); }
}