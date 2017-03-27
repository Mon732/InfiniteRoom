using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraMimic : MonoBehaviour
{
    public Transform mirror1;
    public Transform mirror2;
    
    public Transform cam1;
    public Transform cam2;

    public bool debugDraw = false;

    // Use this for initialization
    void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!mirror1 || !mirror2 || !cam1 || !cam2)
        {
            return;
        }

        Vector3 pos = mirror1.InverseTransformPoint(cam1.position);
        Quaternion rot = (Quaternion.Inverse(mirror1.rotation) * mirror2.rotation);

        Vector3 rotV = rot.eulerAngles;
        rot = Quaternion.Euler(rotV.x, rotV.z, rotV.y);

        cam2.position = mirror2.TransformPoint(pos);

        rot = (cam1.rotation * rot);
        rotV = rot.eulerAngles;

        rot = Quaternion.Euler(-rotV.x, rotV.y, -rotV.z);
        Debug.Log(rotV);

        cam2.rotation = rot;

        if (debugDraw)
        {
            Debug.DrawLine(mirror1.position, cam1.position, Color.green);
            Debug.DrawLine(mirror2.position, cam2.position, Color.green);

            Debug.DrawRay(cam1.position, cam1.forward * 5, Color.cyan);
            Debug.DrawRay(cam2.position, cam2.forward * 5, Color.cyan);

            Debug.DrawRay(cam1.position, cam1.up * 5, Color.yellow);
            Debug.DrawRay(cam2.position, cam2.up * 5, Color.yellow);

            Debug.DrawRay(mirror1.position, mirror1.up * 5, Color.magenta);
            Debug.DrawRay(mirror2.position, mirror2.up * 5, Color.magenta);
        }
    }

    Quaternion planeToRot(Quaternion rotation)
    {
        return Quaternion.Euler(90, 0, 180) * rotation;
    }
}
