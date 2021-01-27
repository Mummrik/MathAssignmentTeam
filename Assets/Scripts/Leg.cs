using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leg : MonoBehaviour
{
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
        Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(endPoint, .1f);
    }
}
