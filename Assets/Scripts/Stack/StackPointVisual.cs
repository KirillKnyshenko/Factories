using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class StackPointVisual : MonoBehaviour
{
    [SerializeField] private StackPoint stackPoint;
    [SerializeField] private GameObject prefab;
    private void Awake() {
        stackPoint.OnItemSpawn += StackPoint_OnItemSpawn;
        stackPoint.OnItemDeleted += StackPoint_OnItemDeleted;
        stackPoint.OnItemGive += StackPoint_OnItemGive;
        stackPoint.OnItemSet += StackPoint_OnItemSet;
    }
    
    private void StackPoint_OnItemSpawn(object sender, System.EventArgs e) {
        prefab = Instantiate(stackPoint.ItemSO.prefab, transform);
    }

    private void StackPoint_OnItemDeleted(object sender, System.EventArgs e) {
        Destroy(prefab);
    }

    private void StackPoint_OnItemGive(object sender, StackPoint.OnItemGiveArgs e) {
        e.GivenStackPoint.SetStackObject(prefab);

        prefab = null;
    }

    private void StackPoint_OnItemSet(object sender, StackPoint.OnItemSetArgs e) {
        prefab = e.SetGameObject;

        prefab.transform.parent = transform;

        prefab.transform.DOLocalMove(Vector3.zero, 0.5f);
        prefab.transform.DOLocalRotate(Vector3.zero, 0.5f);
        prefab.transform.DOScale(Vector3.one, 0.5f);
    }
}
