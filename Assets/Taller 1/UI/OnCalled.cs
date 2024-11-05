using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnCalled : MonoBehaviour
{
    [SerializeField] UnityEvent onBeenCalled;
    void CallAction()
    {
        onBeenCalled?.Invoke();
    }
}
