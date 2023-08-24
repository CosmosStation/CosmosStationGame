using System;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;
    
    private void Awake()
    {
        current = this;
    }

    private void Start()
    {
        Debug.Log(onPlayerGrabbed);
    }

    public event Action onPlayerMovementLock;
    
    public event Action onPlayerMovementUnlock;

    public event Action onPlayerGrabbed;
    
    public void PlayerMovementLock()
    {
        if (onPlayerMovementLock != null)
        {
            onPlayerMovementLock();
        }
    }

    public void PlayerMovementUnlock()
    {
        if (onPlayerMovementUnlock != null)
        {
            onPlayerMovementUnlock();
        }
    }

    public void PlayerGrabbed()
    {
        if (onPlayerGrabbed != null)
        {
            onPlayerGrabbed();
        }
    }
}
