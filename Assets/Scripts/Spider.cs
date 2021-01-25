using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : MonoBehaviour
{
    public GameObject UpperLegJoint;
    public GameObject LowerLegJoint;
    public GameObject LegEndpoint;

    public GameObject UpperLegMesh;
    public GameObject LowerLegMesh;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    Vector3 end;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        float distance = Vector3.Distance(UpperLegJoint.transform.position, LegEndpoint.transform.position);
        //Vector3 end = LegEndpoint.transform.position;
        if (distance <= 2)
        {
            end = LegEndpoint.transform.position;
        }
        Gizmos.DrawLine(UpperLegJoint.transform.position, end);

        Gizmos.color = Color.yellow;

        Vector3 halfPoint = GetHalfPointOnLine(UpperLegJoint.transform.position, end);

        Vector3 cross = Vector3.Cross(UpperLegJoint.transform.position, end);
        Vector3 halfPointUp = halfPoint + Vector3.up;
        //Vector3 halfPointUp = halfPoint + cross.normalized;

        float c = Vector3.Distance(UpperLegJoint.transform.position, halfPointUp);
        float b = Vector3.Distance(UpperLegJoint.transform.position, halfPoint);
        float a = Mathf.Sqrt(Mathf.Pow(c, 2) - Mathf.Pow(b, 2));

        Gizmos.DrawLine(halfPoint, Vector3.up * a);

        LowerLegJoint.transform.position = Vector3.up * a;

        UpperLegJoint.transform.LookAt(LowerLegJoint.transform);
        LowerLegJoint.transform.LookAt(end);
    }

    private Vector3 GetHalfPointOnLine(Vector3 startOrigin, Vector3 endOrigin)
    {
        return (startOrigin + endOrigin) * 0.5f;
    }
}