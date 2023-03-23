using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreStack : StackCore
{
    [SerializeField] private ItemSO itemSOToStore;
    public ItemSO ItemSOToStore => itemSOToStore;
    
    private void Start() {
        StackInit();
    }

    public bool HaveFreePoint(out StackPoint stackPointArg) {
        foreach (StackPoint stackPoint in stackPoints)
        {
            if (stackPoint.GetItemSO() == null)
            {
                stackPointArg = stackPoint;
                return true;
            }
        }

        stackPointArg = null;
        return false;
    }
    
    public void AddItemToStack(StackPoint stackPointArg) {
        stackPointArg.SetItemSO(itemSOToStore);
    }

    public bool HaveAnyObject(out StackPoint stackPointArg) {
        for (int i = stackPoints.Count - 1; i >= 0 ; i--)
        {
            if (stackPoints[i].GetItemSO() != null)
            {
                stackPointArg = stackPoints[i];
                return true;
            }
        }

        stackPointArg = null;
        return false;
    }

    public void RemoveItemFromStack(StackPoint stackPointArg) {
        stackPointArg.DeleteItemSO();
    }

    public bool TryGetItemFromStack() {
        return false;
    }
    public bool isAnyFreePoint() {
        return false;
    }
    
    // public bool isAnyFreePoint(out Vector3Int index) {
    //     for (int j = 0; j < sizeOfStore.y; j++)
    //     {    
    //         for (int i = 0; i < sizeOfStore.x; i++)
    //         {
    //             for (int v = 0; v < sizeOfStore.z; v++)
    //             {
    //                 if (!stackObjectsArray[i,j,v].gameObject.activeInHierarchy)
    //                 {
    //                     index = new Vector3Int(i,j,v);

    //                     return true;
    //                 }
    //             }
    //         }
    //     }

    //     index = Vector3Int.zero;
    //     return false;
    // }

    // public bool TryGetItemFromStack() {
    //     for (int j = (int)sizeOfStore.y - 1; j >= 0; j--)
    //     {  
    //         for (int i = (int)sizeOfStore.x - 1; i >= 0; i--)
    //         {
    //             for (int v = (int)sizeOfStore.z - 1; v >= 0; v--)
    //             {
    //                 if (stackObjectsArray[i,j,v].gameObject.activeInHierarchy)
    //                 {
    //                     stackObjectsArray[i,j,v].gameObject.SetActive(false);

    //                     OnGiveItem?.Invoke(this, new OnGiveItemArgs() {
    //                         stackItemTransform = stackObjectsArray[i,j,v].transform,
    //                     });

    //                     return true;
    //                 }
    //             }
    //         }
    //     }

    //     return false;
    // }
    
}
