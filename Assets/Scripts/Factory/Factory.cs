using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour
{
    public event EventHandler OnPlayerAskItem;
    [SerializeField] private float spawnTimer;
    [SerializeField] private float spawnTimerMax = 5f;
    [SerializeField] private float getDelayTimer;
    [SerializeField] private float getDelayTimerMax = 5f;
    private bool canDelay;
    [SerializeField] private StoreStack storeOutput;
    [SerializeField] private StoreStack storesInput;
    [SerializeField] private ItemSO itemToInput;
    [SerializeField] private ItemSO itemToOutput;
    public ItemSO ItemToOutput => itemToOutput;

    private void Update() {
        spawnTimer -= Time.deltaTime;

        if (spawnTimer < 0f)
        {
            if (storesInput != null)
            {
                // Factory reqiers inputs material to store
                if (storesInput.HaveAnyObject(out StackPoint StackPoint))
                {
                    
                    StackPoint.DeleteStackObject();

                    SpawnProduct();

                    spawnTimer = spawnTimerMax;
                    if (getDelayTimer < 0f)
                    {
                        getDelayTimer = getDelayTimerMax;
                    }
                } 
            }
            else
            {
                SpawnProduct();
                spawnTimer = spawnTimerMax;
            }   
        }

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

    private void SpawnProduct() {
        if (storeOutput.HaveFreePoint(out StackPoint stackPoint))
        {
            // Stack has free point

            storeOutput.CreateItemToStack(stackPoint);     

            // Item was spawned, stop foreach

            return;  
        } 
        else
        {
            //Item did not spawned
        }
    }

    public bool TryGetProduct(StoreStack inventoryStoreStack) {
        if (canDelay)
        {
            // Factory is ready to delay product
            if (storeOutput.HaveAnyObject(out StackPoint stackPoint))
            {
                // Factory has any output object
                if (inventoryStoreStack.HaveFreePoint(out StackPoint inventoryStackPoint))
                {  
                    // Inventory have free point
                
                    storeOutput.HandoverItem(stackPoint, inventoryStackPoint);

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
        if (other.TryGetComponent<Player>(out Player player))
        {
            foreach (var inventoryStore in player.InventoryStores)
            {
                if (itemToInput == inventoryStore.ItemSOToStore)
                {
                    if (player.TryGetProduct(inventoryStore, storesInput)) {
                        return;
                    } 
                }
            }
        }
    }
}