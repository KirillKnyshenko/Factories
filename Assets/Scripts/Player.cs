using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private  float movementSpeed;
    [SerializeField] private VariableJoystick variableJoystick;

    public void Update() {
        Movement();
    }

    private void Movement() {
        Vector3 direction = Vector3.forward * variableJoystick.Vertical + Vector3.right * variableJoystick.Horizontal;
        
        float moveSpeed = movementSpeed * Time.deltaTime;
        float playerHight = 2f;
        float playerRadius = 0.5f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHight, playerRadius, direction, moveSpeed);
       
        if (!canMove)
        {
            Vector3 directionX = new Vector3(direction.x, 0, 0);
            
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHight, playerRadius, directionX, moveSpeed);
            if (canMove)
            {
                direction = directionX;
            }
            else
            {
                Vector3 directionZ = new Vector3(0, 0, direction.z);

                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHight, playerRadius, directionZ, moveSpeed);

                if (canMove)
                {
                    direction = directionZ;
                }
            }
        }

        if (canMove)
        {
            transform.position += direction.normalized * movementSpeed * Time.deltaTime;
        }

        float rotationSpeed = 10f;

        transform.forward = Vector3.Slerp(transform.forward, direction, rotationSpeed * Time.deltaTime);
    }
}
