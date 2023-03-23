using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackCore : MonoBehaviour
{
    [SerializeField] protected Color gizmosColor;
    [SerializeField] protected float sizeOfGizmos = 0.3f;
    [SerializeField] protected Vector3 sizeOfStore;
    [SerializeField] protected Vector3 offsetOfStore;
    [SerializeField] protected Transform spawnContainer;
    [SerializeField] protected StackPoint stackPointTemplate;
    [SerializeField] protected List<StackPoint> stackPoints;
    protected void StackInit() {
        for (int i = 0; i < sizeOfStore.x; i++)
        {
            for (int j = 0; j < sizeOfStore.y; j++)
            {
                for (int v = 0; v < sizeOfStore.z; v++)
                {
                    stackPoints.Add(Instantiate(stackPointTemplate, spawnContainer).GetComponent<StackPoint>());

                    stackPoints[stackPoints.Count - 1].transform.localPosition = new Vector3(i, j, v);
                }
            }
        }
    }   

    private void OnDrawGizmos() {
        for (int i = 0; i < sizeOfStore.x; i++)
        {
            for (int j = 0; j < sizeOfStore.y; j++)
            {
                for (int v = 0; v < sizeOfStore.z; v++)
                {
                    Vector3 newPosition = spawnContainer.position + new Vector3(i * offsetOfStore.x, j * offsetOfStore.y, v * offsetOfStore.z);

                    Gizmos.color = gizmosColor;
                    Gizmos.DrawSphere(newPosition, sizeOfGizmos);
                }
            }
        }
    }
}
