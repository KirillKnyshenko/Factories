using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private ItemSO itemSO;

    public ItemSO GetItemSO() {
        return itemSO;
    }

    public void SpawnItemSO(ItemSO itemSOToSpawn, Transform parent) {

        Instantiate(itemSOToSpawn, parent);
    }
}
