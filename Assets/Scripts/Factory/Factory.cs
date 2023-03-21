using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour
{
    [SerializeField] private float spawnTimer;
    [SerializeField] private float spawnTimerMax = 5f;
    [SerializeField] private List<StoreStack> storesOutput;
    [SerializeField] private List<StoreStack> storesInput;
    [SerializeField] private ItemSO itemToInput;
    public ItemSO itemToOutput;
    private Vector3 spawnPoint;
    private void Start() {
        
    }

    private void Update() {
        spawnTimer -= Time.deltaTime;
         
        if (spawnTimer < 0f)
        {
            SpawnProduct(itemToOutput);

            spawnTimer = spawnTimerMax;
        }

    }

    private void SpawnProduct(ItemSO spawnItem) {
        foreach (StoreStack storeOutput in storesOutput)
        {
            if (storeOutput.TryAddItemToStack())
            {
                // Item spawned successfully

                return;
            } 
            else
            {
                //Item did not spawned
            }
        }
    }
}