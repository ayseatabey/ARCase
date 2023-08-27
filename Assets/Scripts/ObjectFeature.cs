using System;
using UnityEngine;

[Serializable]
public class ObjectFeature
{

    #region Draggable Object Spec

    public GameObject MovingPrefab;
    public Transform OperationTrigger;
    public AudioClip successSound;

    #endregion



    public OperationType operation;

    public enum OperationType
    {
        CreateNewObject,
        ChangeLook
    }
}
