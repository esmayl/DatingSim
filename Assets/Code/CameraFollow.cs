using UnityEngine;
using System.Collections;
using System;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float distance = 5.0f;

    public float yMinLimit = 10f;
    public float yMaxLimit = 80f;

    PlayerMovement player;

    float x = 0.0f;
    float y = 0.0f;

    RaycastHit hitInfo;

    void Start()
    {
        player = target.GetComponent<PlayerMovement>();
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
        hitInfo = new RaycastHit();
    }
    

    void Update()
    {
        if (target)
        {

                x += Input.GetAxis("Mouse X") * distance;
                y -= Input.GetAxis("Mouse Y");

                y = ClampAngle(y, yMinLimit, yMaxLimit);

                Quaternion rotation = Quaternion.Euler(y, x, 0);

                Vector3 negDistance = new Vector3(0.5f, -1, -distance);

                Vector3 position = rotation * negDistance + target.position;

                transform.rotation = rotation;
                transform.position = position;


                //Vector3 t = player.transform.position;
                //t += Vector3.up * 2.5f;
                //t = new Vector3(Mathf.Round(t.x), Mathf.Round(t.y), Mathf.Round(t.z));

                //transform.LookAt(t);
        }
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }

}
