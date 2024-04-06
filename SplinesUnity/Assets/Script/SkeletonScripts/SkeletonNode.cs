using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.LowLevel;

public class SkeletonNode : MonoBehaviour
{
    //should be the interface for the Base, holds higherarchy position, the head and shoulders would be at the same hierarchy position in relation to the body
    public Matrix4x4 localTrans, globalTrans;
    public int parentIndex; //if base node -1, else parent index
    public Vector3 showPos, showScale;
    public Quaternion showRotation;
    public Joint qJoint;

    private void Start()
    {
        if(parentIndex == -1)
        {
            localTrans.SetTRS(Vector3.zero, Quaternion.identity, Vector3.one);
        }
        globalTrans = localTrans;
    }

    private void FixedUpdate()
    {
        //globalTrans = SetGlobal();

        //showPos = globalTrans.GetPosition();
        //showRotation = globalTrans.rotation;
        //showScale = globalTrans.lossyScale;
        if(globalTrans.ValidTRS())
        {
            transform.position = globalTrans.GetPosition();
            transform.rotation = globalTrans.rotation;
            transform.localScale = globalTrans.lossyScale;
        }
    }

    public Matrix4x4 SetGlobal()
    {
        if(parentIndex != -1) 
            return localTrans * SkeletonBase.instance.allNodes[parentIndex].globalTrans;
        return localTrans;
    }
    /*
    private Matrix4x4 SetGlobal()
    {
        if (parentIndex != -1)
        {
            qJoint.setLocal();
            Vector3 pos = localTrans.GetPosition();
            Debug.Log(gameObject.name + ", " + localTrans.GetPosition() + ", " + localTrans.rotation + ", " + localTrans.lossyScale);

            Matrix4x4 tempGlobal = new Matrix4x4(localTrans.GetColumn(0), localTrans.GetColumn(1), localTrans.GetColumn(2), localTrans.GetColumn(3));
            
            bool foundRoot = false;
            SkeletonNode nextNode = SkeletonBase.instance.allNodes[parentIndex];
            if(nextNode.qJoint) 
            { 
                nextNode.qJoint.setLocal();
            }
            while (!foundRoot)
            {
                if(nextNode.parentIndex == -1)
                {
                    foundRoot = true;
                }
                else
                {
                    tempGlobal *= nextNode.localTrans;
                    nextNode = SkeletonBase.instance.allNodes[nextNode.parentIndex];
                    if (nextNode.qJoint)
                    {
                        nextNode.qJoint.setLocal();
                    }
                }
            }

            Debug.Log(gameObject.name + ", " + localTrans.GetPosition() + ", " + localTrans.rotation + ", " + localTrans.lossyScale);

            return tempGlobal;
        }
        return localTrans;
    }
    */
}
