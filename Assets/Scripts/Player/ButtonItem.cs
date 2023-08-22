using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonItem : MonoBehaviour
{
    public UnityEvent pressedMethod;
    
    // Start is called before the first frame update
    public void pressButton()
    {
        pressedMethod.Invoke();
    }
}
