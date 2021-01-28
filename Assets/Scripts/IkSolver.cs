using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IkSolver : MonoBehaviour
{
    public float maxReach = 2;
    public Vector3 targetPosition = new Vector3(0, 0, 1);

    private void OnDrawGizmos()
    {
        Vector3 origin = transform.position;
        Vector3 endpoint = targetPosition;
        if (Vector3.Distance(origin, targetPosition) > maxReach)
        {
            endpoint = origin + (transform.forward * maxReach);
        }

        // origin sphere
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(origin, .1f);

        // end sphere
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(endpoint, .1f);

        // target sphere
        Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(origin + targetPosition, .05f);

        //Line between origin and endpoint
        Gizmos.color = Color.red;
        Gizmos.DrawLine(origin, endpoint);

        float c = maxReach * 0.5f;
        float b = Vector3.Distance(origin, GetHalfPoint(origin, endpoint));
        float a = Mathf.Sqrt(Mathf.Pow(c, 2) - Mathf.Pow(b, 2));

        Vector3 halfpoint = GetHalfPoint(origin, endpoint) + transform.up * a;

        // halfpoint sphere
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(halfpoint, .1f);
        Gizmos.DrawLine(GetHalfPoint(origin, endpoint), halfpoint);

        //Segment debug
        Gizmos.color = Color.green;
        Gizmos.DrawLine(origin, halfpoint);
        Gizmos.DrawLine(endpoint, halfpoint);

        transform.LookAt(endpoint);
    }

    private Vector3 GetHalfPoint(Vector3 a, Vector3 b)
    {
        return (a + b) * 0.5f;
    }

    private Vector3 GetEndpoint(float maxLength)
    {
        return new Vector3
            (
            targetPosition.x > maxLength ? maxLength : targetPosition.x,
            targetPosition.y > maxLength ? maxLength : targetPosition.y,
            targetPosition.z > maxLength ? maxLength : targetPosition.z
            );
    }
}
