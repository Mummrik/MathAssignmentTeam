using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leg : MonoBehaviour
{
    public bool drawGizmos = true;
    public float maxReach = 2;
    public Vector3 startPos;
    public Vector3 endPoint;

    public GameObject joint1;
    public GameObject joint2;

    private void Start()
    {
        endPoint = transform.position + startPos;
    }

    // Update is called once per frame
    void Update()
    {
        float c = maxReach * 0.5f;
        float b = Vector3.Distance(transform.position, GetHalfPoint(transform.position, endPoint));
        float a = Mathf.Sqrt(Mathf.Pow(c, 2) - Mathf.Pow(b, 2));

        joint2.transform.position = GetHalfPoint(transform.position, endPoint) + transform.up * a;

        joint1.transform.LookAt(joint2.transform.position);
        joint2.transform.LookAt(endPoint);
        transform.LookAt(endPoint);
    }

    private Vector3 GetHalfPoint(Vector3 a, Vector3 b)
    {
        return (a + b) * 0.5f;
    }

    private void OnDrawGizmos()
    {
        if (!drawGizmos)
        {
            return;
        }
        Vector3 origin = joint1.transform.position;

        // origin sphere
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(origin, .1f);

        // end sphere
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(endPoint, .1f);

        //Line between origin and endpoint
        Gizmos.color = Color.red;
        Gizmos.DrawLine(origin, endPoint);

        float c = maxReach * 0.5f;
        float b = Vector3.Distance(origin, GetHalfPoint(origin, endPoint));
        float a = Mathf.Sqrt(Mathf.Pow(c, 2) - Mathf.Pow(b, 2));

        Vector3 halfpoint = GetHalfPoint(origin, endPoint) + transform.up * a;

        // halfpoint sphere
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(GetHalfPoint(origin, endPoint), halfpoint);

        //Segment debug
        Gizmos.color = Color.green;
        Gizmos.DrawLine(origin, halfpoint);
        Gizmos.DrawLine(endPoint, halfpoint);
    }
}
