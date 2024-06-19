using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newlaser : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private float laserMaxLength = 100;
    [SerializeField] private int laserMaxHits = 5;
    public LayerMask layerDetection;

    public Sensor sensor;

    private void Start()
    {
        //This is to stop a raycast from inside a collider from hitting said collider 
        //(risk of happening with the "reflected" raycasts):
        Physics2D.queriesStartInColliders = false;
        lineRenderer.useWorldSpace = false;
    }

    void Update()
    {
        RaycastHit2D[] hits = new RaycastHit2D[laserMaxHits];
        Vector3[] linePoints = new Vector3[hits.Length + 1];
        float laserLength = laserMaxLength;
        Vector2 laserDirection = transform.right;
        hits[0] = Physics2D.Raycast(transform.position, laserDirection, laserLength, layerDetection);
        linePoints[0] = Vector2.zero;

        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i])
            {
                if (hits[i].collider.CompareTag("Sensor") && !sensor.sensorState)
                {
                    sensor.sensorState = true;
                }


                if (hits[i].collider.gameObject.tag == "Mirror")
                {
                    //Part of the max length of the laser is used:
                    laserLength -= hits[i].distance;
                    //Get the reflect direction from the hit normal:
                    laserDirection = Vector2.Reflect(laserDirection, hits[i].normal);
                    //Assign a point on the linerenderer where the ray hit:
                    linePoints[i + 1] = transform.InverseTransformPoint(hits[i].point);
                    //New raycast for evey bounce, except for the last hit
                    //possible (to avoid running out of array):
                    if (i != hits.Length - 1)
                    {
                        hits[i + 1] = Physics2D.Raycast(hits[i].point, laserDirection, laserLength, layerDetection);
                    }
                    Debug.DrawRay(hits[i].point, laserDirection, Color.blue);
                }
                else
                {
                    //Hit a collider that is non non-reflective
                    linePoints[i + 1] = transform.InverseTransformPoint(hits[i].point);
                    //Make sure the lineRenderer has enough points to draw the entire laser:
                    lineRenderer.positionCount = i + 2;
                    break;
                }
            }
            else
            {
                //End of laser did not hit anything
                linePoints[i + 1] = (Vector2)linePoints[i] + laserDirection * laserLength;
                //Make sure the lineRenderer has enough points to draw the entire laser:
                lineRenderer.positionCount = i + 2;
                break;
            }
        }

        //Assign the found point to the line renderer:
        lineRenderer.SetPositions(linePoints);
    }
}
