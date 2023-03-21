using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreStack : MonoBehaviour
{
    public event EventHandler<OnSpawnedArgs> OnSpawned;
    public class OnSpawnedArgs : EventArgs 
    {
        public Transform transformArg;
    }

    public struct StackObject
    {
        public Transform transform;
        public bool isHasItem;
    }

    [SerializeField] private Color gizmosColor;

    public Vector3 sizeOfStore;
    public Vector3 offsetOfStore;
    public Transform spawnContainer;
    public Transform spawnPointTemplete;
    public int countOfItemsMax
    {
        get { return (int)(sizeOfStore.x * sizeOfStore.y * sizeOfStore.z); }
        private set {}
    }

    public int countOfItems;
    private StackObject[,,] stackObjectsArray;
    
    private void Start() {
        stackObjectsArray = new StackObject[(int)sizeOfStore.x, (int)sizeOfStore.y, (int)sizeOfStore.z];

        StackInit();
    }

    private void StackInit() {
        for (int i = 0; i < sizeOfStore.x; i++)
        {
            for (int j = 0; j < sizeOfStore.y; j++)
            {
                for (int v = 0; v < sizeOfStore.z; v++)
                {
                    stackObjectsArray[i,j,v].transform = Instantiate(spawnPointTemplete, spawnContainer);
                    stackObjectsArray[i,j,v].isHasItem = false;

                    Vector3 newPosition = new Vector3(i * offsetOfStore.x, j * offsetOfStore.y, v * offsetOfStore.z);
                    
                    stackObjectsArray[i,j,v].transform.localPosition = newPosition;
                }
            }
        }
    }

    public bool TryAddItemToStack() {
        if (TryGetSpawnPoint(out Transform spawnPointTransform))
        {
            countOfItems++;

            OnSpawned?.Invoke(this, new OnSpawnedArgs {
                transformArg = spawnPointTransform
            });

            return true;
        }
        else
        {
            return false;
        }
    }

    public bool TryGetSpawnPoint(out Transform spawnPointTransform) {
        for (int j = 0; j < sizeOfStore.y; j++)
        {    
            for (int i = 0; i < sizeOfStore.x; i++)
            {
                for (int v = 0; v < sizeOfStore.z; v++)
                {
                    if (!stackObjectsArray[i,j,v].isHasItem)
                    {
                        spawnPointTransform = stackObjectsArray[i,j,v].transform;

                        stackObjectsArray[i,j,v].isHasItem = true;

                        return true;
                    }
                }
            }
        }

        spawnPointTransform = null;
        return false;
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
                    Gizmos.DrawSphere(newPosition, 0.3f);
                }
            }
        }
    }
}
