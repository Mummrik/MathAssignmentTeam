using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IkSolver : MonoBehaviour
{
    public float maxLegLength = 2;
    public Vector3 targetPosition = new Vector3(0, 0, 1);

    private void OnDrawGizmos()
    {
        Vector3 origin = transform.position;
        Vector3 endpoint = origin + GetEndpoint(maxLegLength);
        Vector3 halfpoint = GetHalfPoint(origin, endpoint);

        float c = maxLegLength * 0.5f;
        float b = Vector3.Distance(origin, halfpoint);
        float a = Mathf.Sqrt(Mathf.Pow(c, 2) - Mathf.Pow(b, 2));
        Vector3 halfpointUp = (origin + halfpoint + (Vector3.up * a)).normalized;

        Gizmos.DrawSphere(origin, .1f);
        Gizmos.DrawSphere(halfpointUp, .1f);
        Gizmos.DrawSphere(halfpoint, .1f);
        Gizmos.DrawSphere(endpoint, .1f);

        Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(origin + targetPosition, .05f);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(origin, endpoint);
        Gizmos.DrawLine(origin + (Vector3.up * a), endpoint + (Vector3.up * a)); // debug
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(halfpoint, halfpointUp);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(origin, halfpointUp);
        Gizmos.DrawLine(endpoint, halfpointUp);
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
