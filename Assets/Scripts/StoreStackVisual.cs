using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreStackVisual : MonoBehaviour
{
    [SerializeField] private StoreStack storeStack;
    [SerializeField] private Factory factory;
    private void Start()
    {
        storeStack.OnSpawned += StoreStack_OnSpawned;
    }

    private void StoreStack_OnSpawned(object sender, StoreStack.OnSpawnedArgs e) {
        UpdateStackVisual(e.transformArg);
    }

    private void UpdateStackVisual(Transform parent) {
        Instantiate(factory.itemToOutput.prefab, parent);
    }
}
