using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackPointVisual : MonoBehaviour
{
    [SerializeField] private StackPoint stackPoint;
    [SerializeField] private GameObject prefab;
    private void Awake() {
        stackPoint.OnItemSpawn += StackPoint_OnItemSpawn;
        stackPoint.OnItemDeleted += StackPoint_OnItemDeleted;
    }

    private void StackPoint_OnItemSpawn(object sender, System.EventArgs e) {
        if (prefab != null)
        {
            Destroy(prefab);
        }

        prefab = Instantiate(stackPoint.GetItemSO().prefab, transform);
    }

    private void StackPoint_OnItemDeleted(object sender, System.EventArgs e) {
        Destroy(prefab);
    }
}
