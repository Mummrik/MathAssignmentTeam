using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : MonoBehaviour
{

    [SerializeField]
    private float moveSpeed = 2;

    [SerializeField]
    private Leg[] legs;

    void Update()
    {
        Vector2 direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (direction != Vector2.zero)
        {
            transform.position += (transform.forward * direction.y) * moveSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y + direction.x, 0);

            foreach (var leg in legs)
            {
                MoveLeg(leg, direction);
            }
        }

        if (Input.GetAxis("UpDown") != 0)
        {
            transform.position += new Vector3(0, Input.GetAxis("UpDown"), 0) * 2 * Time.deltaTime;
        }

        Camera.main.transform.LookAt(transform.position);
    }

    void MoveLeg(Leg leg, Vector2 direction)
    {
        float dist = Vector3.Distance(leg.joint1.transform.position, leg.targetPosition);
        if (direction.y != 0)
        {
            if (dist > 1.4f && dist < 1.5f)
            {
                leg.targetPosition += (transform.forward * 1.25f) * direction.y;
            }
            else if (dist > 2f)
            {
                leg.targetPosition = leg.transform.position + leg.startPos;
            }
        }
    }
}