using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabMethods : MonoBehaviour
{
    public EnergyManager playerEnergy;
    public int grabHealth = 100;
    public int grabDamage = 10;
    public int resistPower = 5;
    
    [SerializeField] private float timeStep = 1;
    
    private float _time = 0;

    public void GrabbedStep()
    {

        _time += Time.deltaTime;
        if (_time >= timeStep)
        {
            _time = 0;
            playerEnergy.DecreaseEnergy(grabDamage);
        }
    }

    public bool checkIsGrabbed()
    {
        if (grabHealth > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void DoResist()
    {
        grabHealth -= resistPower;
        Debug.Log(grabHealth);
    }
}
