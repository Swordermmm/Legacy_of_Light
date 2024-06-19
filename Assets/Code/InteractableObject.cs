using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class InteractableObject : ColliadleableObject
{
    public float degrees;
    private bool isActivated;
    public float cooldownTime = 1;

    private float nextActivation = 0;

    protected override void OnCollided(GameObject collidedObject)
    {
        if (Input.GetKey(KeyCode.E) && (Time.time > nextActivation))
        {
            OnInteract_E();
            nextActivation = Time.time + cooldownTime;
        }
    }

    private void OnInteract_E()
    {
        
        if (!isActivated)
        {
            isActivated = true;
            transform.Rotate(0,0, degrees);
        } 
        else
        {
            isActivated = false;
            transform.Rotate(0, 0, -degrees);
        }        
    }

}
