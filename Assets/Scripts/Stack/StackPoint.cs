using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackPoint : MonoBehaviour
{
    public event EventHandler OnItemSpawn;
    public event EventHandler OnItemDeleted;
    public event EventHandler<OnItemGiveArgs> OnItemGive;
    public class OnItemGiveArgs : EventArgs 
    {
        public StackPoint GivenStackPoint;
    }
    public event EventHandler<OnItemSetArgs> OnItemSet;
    public class OnItemSetArgs : EventArgs 
    {
        public GameObject SetGameObject;
    }

    [SerializeField] private StoreStack storeStack;
    public StoreStack GetStoreStack() => storeStack;
    private ItemSO itemSO;
    public ItemSO ItemSO => itemSO;
    public bool isHaveItem;

    private void Awake() {
        itemSO = storeStack.ItemSOToStore;
    }

    public void CreateStackObject() {
        isHaveItem = true;

        OnItemSpawn?.Invoke(this, EventArgs.Empty);
    }

    public void DeleteStackObject() {
        isHaveItem = false;

        OnItemDeleted?.Invoke(this, EventArgs.Empty);
    }

    public void GiveStackObject(StackPoint newStackPoint) {
        isHaveItem = false;

        OnItemGive?.Invoke(this, new OnItemGiveArgs {
            GivenStackPoint = newStackPoint
        });
    }

    public void SetStackObject(GameObject prefab) {
        isHaveItem = true;

        OnItemSet?.Invoke(this, new OnItemSetArgs {
            SetGameObject = prefab
        });
    }
}
