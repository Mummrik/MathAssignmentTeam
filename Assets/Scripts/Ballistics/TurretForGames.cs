using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretForGames : MonoBehaviour
{
    public GameObject target;
    public float bulletSpeed = 100f;
    public float turretHorizontalLimit = 90;
    public float gravityScalar = 0.5f;
    public float verticalClampMin = -10f;
    public float verticalClampMax = 90f;
    public float lerpSpeed = 1f;


    public GameObject turretBarrel;
    public GameObject turretBase;
    
    private Vector3 targetsPos;
  
    void Start()
    {
    }

    void Update()
    {
        Quaternion turretBarrelAngle;



        //turretBase.transform.localRotation = Quaternion.Lerp(turretBase.transform.localRotation, TurretSolver().baseRotation, Time.deltaTime * lerpSpeed);
        transform.rotation = Quaternion.Lerp(transform.rotation, TurretSolver().baseRotation, Time.deltaTime * lerpSpeed);
        //transform.rotation = TurretSolver().baseRotation;

        //float nagel = Mathf.Lerp(turretBarrel.transform.localRotation, TurretSolver().barrelElevation, Time.deltaTime * lerpSpeed);
        turretBarrelAngle = TurretSolver().barrelElevation;
        //Debug.Log("nagel" + nagel);

        turretBarrel.transform.localRotation = turretBarrelAngle;
    }

    (Quaternion baseRotation, Quaternion barrelElevation) TurretSolver() {
        targetsPos = target.transform.position;
        float distance = Vector3.Distance(transform.position, targetsPos);
        Vector3 targetsVelocity = target.GetComponent<Rigidbody>().velocity;

        float time = distance / bulletSpeed;
        float bulletGravity = Mathf.Pow(time, 2f) * Physics.gravity.y * 100 * gravityScalar;
        //Debug.Log(bulletGravity);


        Vector3 distanceOffset = bulletSpeed / distance * targetsVelocity;
        
        Vector3 GravitySolver = distanceOffset + targetsPos;
       
        GravitySolver.y = GravitySolver.y + bulletGravity;
        //Debug.Log(GravitySolver);

        //Vector3 PosWithGravity = transform.InverseTransformDirection(targetsPos - transform.position);
        //Vector3 PosWithGravity = transform.InverseTransformDirection(GravitySolver - transform.position);
        Vector3 PosWithGravity = GravitySolver - transform.position;

        float yawFromYVector = Quaternion.LookRotation(PosWithGravity).eulerAngles.y;
        //Debug.Log(yawFromYVector);

        float ClampedTurretBaseAngle = Mathf.Clamp(yawFromYVector, -turretHorizontalLimit, turretHorizontalLimit);

        Quaternion baseRotation = Quaternion.Euler(0, ClampedTurretBaseAngle, 0);
        //Debug.Log("baserotation: " + baseRotation.eulerAngles);

        //Vector3 UnrotatedVectorXY = UnrotateVector(PosWithGravity, baseRotation);

        //float ClampedBarrelAngle = Mathf.Clamp(Mathf.Atan2(PosWithGravity.z, PosWithGravity.y) * Mathf.Rad2Deg, verticalClampMin, verticalClampMax);

        //float ClampedBarrelAngle = Mathf.Clamp(pitchFromXVector, verticalClampMin, verticalClampMax);
        //Debug.Log("clampbarrelangle: " + ClampedBarrelAngle);

       //Debug.Log(transform.parent.forward);

        float ClampedBarrelAngle = Mathf.Atan2(PosWithGravity.z, PosWithGravity.y) * Mathf.Rad2Deg;
        
        if (Vector3.Dot(transform.parent.forward, targetsPos) < 0) {
            ClampedBarrelAngle = -Mathf.Atan2(-PosWithGravity.z,-PosWithGravity.y) * Mathf.Rad2Deg;
            Debug.Log(Vector3.Dot(transform.parent.forward, targetsPos));
        }
        

        Quaternion barrelElevation = Quaternion.Euler(ClampedBarrelAngle, 0f, 0f);
        //Debug.Log(barrelElevation.eulerAngles);

        return (baseRotation, barrelElevation);
    }

    Vector3 UnrotateVector(Vector3 Position, Quaternion Rotator) {
        
        
        //Position - Quaternion.Inverse(Rotator).eulerAngles;


        return Position - Quaternion.Inverse(Rotator).eulerAngles;
    }

    (float x, float y, float z) RotFromXVector(Vector3 InVector) {
        float x, y, z;

        Quaternion rot = Quaternion.LookRotation(InVector);

        x = rot.eulerAngles.x;
        y = rot.eulerAngles.y;
        z = rot.eulerAngles.z;

        return (x, y, z);
    }

}
