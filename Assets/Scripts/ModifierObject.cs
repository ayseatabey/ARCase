using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifierObject : MonoBehaviour
{

    #region Modified Object Spec.

    public Vector3 Destination;
    public ModifiedObject refObject;
    public Texture target;

    #endregion
  
    public Type type;
    public enum Type
    {
        Flag,
        Cup
    }
    private void OnEnable()
    {
        Destination = refObject.transform.position;
    }
}
