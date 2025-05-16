using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubCameraFollow : MonoBehaviour
{
    public Transform target;
    public float followSpeed = 5f;

    private Vector3 initialOffset;

    [Header("Camera Limits")]
    public float minX;
    public float maxX;
    public float minZ;
    public float maxZ;

    [Header("Fixed Rotation")]
    public Vector3 fixedRotationEulerAngles = new Vector3(45f, 0f, 0f);

    void Start()
    {
        if (target != null)
        {
            initialOffset = transform.position - target.position;
        }

        // Appliquer la rotation fixe au démarrage
        transform.rotation = Quaternion.Euler(fixedRotationEulerAngles);
    }

    void FixedUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + initialOffset;

        // Appliquer les limites
        desiredPosition.x = Mathf.Clamp(desiredPosition.x, minX, maxX);
        desiredPosition.z = Mathf.Clamp(desiredPosition.z, minZ, maxZ);

        // Suivi fluide
        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.fixedDeltaTime);

        // Ré-appliquer la rotation fixe à chaque frame pour qu’elle reste figée
        transform.rotation = Quaternion.Euler(fixedRotationEulerAngles);
    }
}
