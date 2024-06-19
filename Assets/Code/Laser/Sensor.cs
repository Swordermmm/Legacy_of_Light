using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Sensor : MonoBehaviour
{
    CircleCollider2D sensorObj;
    public bool sensorState = false;

    private void Start()
    {
        sensorObj = GetComponent<CircleCollider2D>();
    }

}
