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
    [SerializeField] private List<StoreStack> storesOutput;
    [SerializeField] private List<StoreStack> storesInput;
    [SerializeField] private ItemSO itemToInput;
    [SerializeField] private ItemSO itemToOutput;
    public ItemSO ItemToOutput => itemToOutput;
    private Vector3 spawnPoint;

    private void Update() {
        spawnTimer -= Time.deltaTime;

        if (spawnTimer < 0f)
        {
            SpawnProduct();

            spawnTimer = spawnTimerMax;
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
        foreach (StoreStack storeOutput in storesOutput)
        {
            if (storeOutput.HaveFreePoint(out StackPoint stackPoint))
            {
                // Stack has free point

                storeOutput.AddItemToStack(stackPoint);     

                // Item was spawned, stop foreach

                return;  
            } 
            else
            {
                //Item did not spawned
            }
        }
    }

    public void GetProduct(ItemSO getItem) {
        foreach (StoreStack storeOutput in storesOutput)
        {
            if (storeOutput.ItemSOToStore == getItem)
            {
                if (canDelay)
                {
                    if (storeOutput.HaveAnyObject(out StackPoint stackPoint))
                    {
                        // Item get successfully
                        Debug.Log("Item get successfully");
                        
                        storeOutput.RemoveItemFromStack(stackPoint);

                        canDelay = false;
                    }
                }
                else
                {
                    // Item did not get successfully
                    Debug.Log("Item did not get successfully");
                }
            } 
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.TryGetComponent<Player>(out Player player))
        {
            GetProduct(itemToOutput);
        }
    }
}