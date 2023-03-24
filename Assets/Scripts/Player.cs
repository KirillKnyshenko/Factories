using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;
    [SerializeField] private  float movementSpeed;
    [SerializeField] private VariableJoystick variableJoystick;
    [SerializeField] private List<StoreStack> inventoryStores;
    public List<StoreStack> InventoryStores => inventoryStores;
    [SerializeField] private float getDelayTimer;
    [SerializeField] private float getDelayTimerMax = 5f;
    private bool canDelay;

    private void Start() {
        Instance = this;
    }

    private void Update() {
        Movement();

        if (!canDelay)
        {
            getDelayTimer -= Time.deltaTime;

            if (getDelayTimer < 0f)
            {
                getDelayTimer = getDelayTimerMax;

                canDelay = true;
            }
        }
    }

    private void Movement() {
        Vector3 direction = Vector3.forward * variableJoystick.Vertical + Vector3.right * variableJoystick.Horizontal;
        
        float moveSpeed = movementSpeed * Time.deltaTime;
        float playerHight = 2f;
        float playerRadius = 0.5f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHight, playerRadius, direction, moveSpeed, 0, QueryTriggerInteraction.Ignore);
       
        if (!canMove)
        {
            Vector3 directionX = new Vector3(direction.x, 0, 0);
            
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHight, playerRadius, directionX, moveSpeed, 0, QueryTriggerInteraction.Ignore);
            if (canMove)
            {
                direction = directionX;
            }
            else
            {
                Vector3 directionZ = new Vector3(0, 0, direction.z);

                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHight, playerRadius, directionZ, moveSpeed, 0, QueryTriggerInteraction.Ignore);

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

    public bool TryGetProduct(StoreStack inventoryStoreStack, StoreStack storeStack) {
        if (canDelay)
        {
            // Player is ready to delay product
            if (inventoryStoreStack.HaveAnyObject(out StackPoint inventoryStackPoint))
            {
                // Player has any output object
                if (storeStack.HaveFreePoint(out StackPoint storeStackPoint))
                {  
                    // Store have free point
                
                    inventoryStoreStack.HandoverItem(inventoryStackPoint, storeStackPoint);

                    canDelay = false;

                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        return false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent<Factory>(out Factory factory))
        {
            foreach (var inventoryStore in inventoryStores)
            {
                if (inventoryStore.ItemSOToStore == factory.ItemToOutput)
                {
                    if (factory.TryGetProduct(inventoryStore)) {
                        return;
                    } 
                }
            }
        }
    }
}
