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
}
