using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public GameObject Sphere1;
    public GameObject Sphere2;

    private void OnDrawGizmos() {
        float length;
        length = Vector3.Distance(Sphere1.transform.position, Sphere2.transform.position);
        length = Mathf.Clamp(length, 0, 2);
        Debug.DrawLine(Sphere1.transform.position, Sphere1.transform.position * length);

    }
}
