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
    [SerializeField] protected Vector3 scaleOfStackPoints;
    [SerializeField] protected Transform spawnContainer;
    [SerializeField] protected StackPoint stackPointTemplate;
    [SerializeField] protected List<StackPoint> stackPoints;
    protected void StackInit() {
        for (int j = 0; j < sizeOfStore.y; j++)
        {
            for (int v = 0; v < sizeOfStore.z; v++)
            {
                for (int i = 0; i < sizeOfStore.x; i++)
                {
                    
                    stackPoints.Add(Instantiate(stackPointTemplate, spawnContainer).GetComponent<StackPoint>());

                    Vector3 pointLocalPoition = new Vector3(i * offsetOfStore.x, j * offsetOfStore.y, v * offsetOfStore.z);

                    stackPoints[stackPoints.Count - 1].transform.localPosition = pointLocalPoition;

                    stackPoints[stackPoints.Count - 1].transform.localScale = scaleOfStackPoints;
                }
            }
        }
    }   

    public bool HaveFreePoint(out StackPoint stackPointArg) {
        foreach (StackPoint stackPoint in stackPoints)
        {
            if (!stackPoint.isHaveItem)
            {
                stackPointArg = stackPoint;
                return true;
            }
        }

        stackPointArg = null;
        return false;
    }

    public void HandoverItem(StackPoint oldStackPoint, StackPoint newStackPoint) {
        oldStackPoint.GiveStackObject(newStackPoint);
    }

    public bool HaveAnyObject(out StackPoint stackPointArg) {
        for (int i = stackPoints.Count - 1; i >= 0 ; i--)
        {
            if (stackPoints[i].isHaveItem)
            {
                stackPointArg = stackPoints[i];
                return true;
            }
        }

        stackPointArg = null;
        return false;
    }

    public void CreateItemToStack(StackPoint stackPointArg) {
        stackPointArg.CreateStackObject();
    }

    private void RemoveItemFromStack(StackPoint stackPointArg) {
        stackPointArg.DeleteStackObject();
    }

    private void OnDrawGizmos() {
        for (int j = 0; j < sizeOfStore.y; j++)
        {
            for (int v = 0; v < sizeOfStore.z; v++)
            {
                for (int i = 0; i < sizeOfStore.x; i++)
                {
                    Vector3 pointLocalPoition = spawnContainer.position + new Vector3(i * offsetOfStore.x, j * offsetOfStore.y, v * offsetOfStore.z);

                    Gizmos.color = gizmosColor;
                    Gizmos.DrawSphere(pointLocalPoition, sizeOfGizmos);
                }
            }
        }
    }
}
