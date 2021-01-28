using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Leg : MonoBehaviour
{
    public bool drawGizmos = true;
    public float maxReach = 2;
    public Vector3 startPos;
    public Vector3 targetPosition;

    public GameObject joint1;
    public GameObject joint2;

    private Vector3 endPoint;
    private Vector3 halfPoint;

    private void Start()
    {
        targetPosition = transform.position + startPos;
    }

    // Update is called once per frame
    void Update()
    {
        endPoint = targetPosition;
        float distance = Vector3.Distance(transform.position, targetPosition);
        if (distance > maxReach)
        {
            endPoint = transform.position + (transform.forward * Mathf.Abs(maxReach - 0.001f));
        }

        halfPoint = GetHalfPoint(transform.position, endPoint);

        float c = maxReach * 0.5f;
        float b = Vector3.Distance(transform.position, halfPoint);
        float a = Mathf.Sqrt(Mathf.Pow(c, 2) - Mathf.Pow(b, 2));

        joint2.transform.position = halfPoint + transform.up * a;

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
        
        if (!EditorApplication.isPlaying)
        {
            endPoint = origin + startPos;
        }

        // origin sphere
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(origin, .1f);

        // end sphere
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(endPoint, .1f);

        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(targetPosition, .07f);

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

        if (!EditorApplication.isPlaying)
        {
            joint1.transform.LookAt(halfpoint);
            joint2.transform.LookAt(endPoint);
            transform.LookAt(endPoint);
        }

    }
}
