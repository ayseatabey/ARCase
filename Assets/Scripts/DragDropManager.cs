using System.Collections.Generic;
using UnityEngine;

public class DragDropManager : MonoBehaviour
{



    public const string DragObjectLayerName = "DragObject";
    public static DragDropManager Instance;
   
    public int DraggingLayerMask { get { return ~_draggingPlane.layer; } }


    private GameObject _draggingPlane;
    public List<ObjectFeature> draggableObject;





    public Camera ViewCamera;

    [Range(0, 10)]
    public float MatchDistance = 0.5f;

    public AudioSource audioSource;

    private void Awake()
    {

        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(this);

        if (ViewCamera == null)
        {
            ViewCamera = Camera.main;
        }

        InitDraggingPlane();

        audioSource = GetComponent<AudioSource>();


    }



    void Start()
    {
      
    }


    /// <summary>
    /// Created a plane for trigger OnDrag Interface on DraggableUIObject
    /// </summary>
    /// <param name="draggingPlane"></param>

    private void InitDraggingPlane()
    {
        _draggingPlane = new GameObject("DraggingPlane");
        _draggingPlane.AddComponent<BoxCollider>();
        BoxCollider boxCollider = _draggingPlane.GetComponent<BoxCollider>();
       _draggingPlane.layer = LayerMask.NameToLayer(DragObjectLayerName);
        boxCollider.isTrigger = true;
        boxCollider.size = new Vector3(200, 200, 1);
        _draggingPlane.transform.parent = ViewCamera.transform;
        _draggingPlane.transform.localRotation = Quaternion.Euler(0, 0, 0);
        _draggingPlane.transform.localPosition = new Vector3(0, 0, 9);

    }



    //Set objectFeatures on _objectFeature in DraggableUIObject

    public ObjectFeature GetObjectFeature(Transform start)
    {
        return draggableObject.Find(drag => (drag.OperationTrigger == start));
    }

    public void UpdateDraggingPlanePos(Vector3 desPos)
    {
        _draggingPlane.transform.position = desPos;
    }
}
