using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackPoint : MonoBehaviour
{
    public event EventHandler OnItemSpawn;
    public event EventHandler OnItemDeleted;
    private ItemSO itemSO;
    public ItemSO GetItemSO() => itemSO;

    public void SetItemSO(ItemSO itemSO) {
        this.itemSO = itemSO;

        OnItemSpawn?.Invoke(this, EventArgs.Empty);
    }

    public void DeleteItemSO() {
        this.itemSO = null;

        OnItemDeleted?.Invoke(this, EventArgs.Empty);
    }
}
