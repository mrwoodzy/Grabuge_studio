using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float sprintMultiplier = 1.5f;
    private Rigidbody rb;
    private PlayerController controller;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        controller = GetComponent<PlayerController>();
    }

    void FixedUpdate()
    {
        // Convertit l'input en direction sur XZ
        Vector3 moveDirection = new Vector3(controller.moveInput.x, 0f, controller.moveInput.y).normalized;        

        // Applique le sprint si activé
        float currentSpeed = controller.isSprinting ? moveSpeed * sprintMultiplier : moveSpeed;
        Vector3 moveVelocity = moveDirection * currentSpeed;

        // Applique la vitesse
        rb.velocity = new Vector3(moveVelocity.x, rb.velocity.y, moveVelocity.z);
    }
}