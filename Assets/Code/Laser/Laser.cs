using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public Sensor sensor;

    public LineRenderer LaserBeam;

    public int reflections;
    public float MaxDist;
    public LayerMask layerDetection;

    private void Start()
    {
        Physics2D.queriesStartInColliders = false;
    }

    private void Update()
    {
        LaserBeam.positionCount = 1;
        LaserBeam.SetPosition(0, transform.position);

        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.right, MaxDist, layerDetection);

        Ray2D ray = new Ray2D(transform.position, transform.right);

        bool isMirror = false;
        Vector2 mirrorHitPoint = Vector2.zero;
        Vector2 mirrorHitNormal = Vector2.zero;

        for (int i = 0; i < reflections; i++) 
        {
            LaserBeam.positionCount++;

            if (hitInfo.collider != null)
            {
                LaserBeam.SetPosition(LaserBeam.positionCount - 1, hitInfo.point - ray.direction * -0.1f);

                isMirror = false;

                if (hitInfo.collider.CompareTag("Sensor") && !sensor.sensorState)
                {
                    sensor.sensorState = true;
                }

                if (hitInfo.collider.CompareTag("Mirror"))
                {
                    mirrorHitPoint = (Vector2)hitInfo.point;
                    mirrorHitNormal = (Vector2)hitInfo.normal;
                    hitInfo = Physics2D.Raycast((Vector2)hitInfo.point - ray.direction * -0.1f, Vector2.Reflect(hitInfo.point - ray.direction * -0.1f, hitInfo.normal), MaxDist, layerDetection);
                    isMirror = true;
                }
                else
                    break;
            }
            else
            {
                if (isMirror)
                {
                    LaserBeam.SetPosition(LaserBeam.positionCount - 1, mirrorHitPoint + Vector2.Reflect(mirrorHitPoint, mirrorHitNormal) * MaxDist);
                    break;
                }
                else
                {
                    LaserBeam.SetPosition(LaserBeam.positionCount - 1, transform.position + transform.right * MaxDist);
                    break;
                }
            }
        }
    }
}