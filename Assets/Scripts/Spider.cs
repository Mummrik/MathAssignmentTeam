using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : MonoBehaviour
{

    [SerializeField]
    private float moveSpeed = 5;

    public GameObject rightFrontLeg;
    public GameObject rightBackLeg;
    public GameObject leftFrontLeg;
    public GameObject leftBackLeg;

    void Update()
    {
        Vector2 direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (direction != Vector2.zero)
        {
            transform.position += new Vector3(direction.x, 0, direction.y) * moveSpeed * Time.deltaTime;
        }

        if (Input.GetAxis("UpDown") != 0)
        {
            transform.position += new Vector3(0, Input.GetAxis("UpDown"), 0) * 2 * Time.deltaTime;
        }

        // Testing leg movement
        //Vector3 targetPos = rightBackLeg.GetComponent<Leg>().endPoint;
        //float dist = Vector3.Distance(transform.position, targetPos);

        //if (dist > 2.5f)
        //{
        //    rightBackLeg.GetComponent<Leg>().endPoint += (Vector3.forward * 0.5f) * Input.GetAxisRaw("Vertical");
        //}
    }
}