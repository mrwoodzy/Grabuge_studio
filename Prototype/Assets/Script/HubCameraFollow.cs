using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubCameraFollow : MonoBehaviour
{
    public Transform target;
    public float followSpeed = 5f;

    private Vector3 initialOffset;

    [Header("Camera Limits")]
    public Vector2 minPosition;
    public Vector2 maxPosition;

    void Start()
    {
        if (target != null)
        {
            initialOffset = transform.position - target.position;
        }
    }

    void FixedUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + initialOffset;

        // Appliquer les limites sur X et Z
        desiredPosition.x = Mathf.Clamp(desiredPosition.x, minPosition.x, maxPosition.x);
        desiredPosition.z = Mathf.Clamp(desiredPosition.z, minPosition.y, maxPosition.y);

        // Mouvement fluide synchronisé à la physique
        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.fixedDeltaTime);
    }
}
