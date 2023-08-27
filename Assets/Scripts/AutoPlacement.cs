using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.XR.ARSubsystems;

public class AutoPlacement : MonoBehaviour
{

    #region Prefabs
    [SerializeField]
    private GameObject verticalPrefab;

    [SerializeField]
    private GameObject horizontalPrefab;


    private GameObject placedObject;

    #endregion
    #region ARFoundation

    [SerializeField]
    private ARPlaneManager arPlaneManager;

    private List<ARPlane> arPlanes;

    #endregion
    #region bool variables

    bool isHorizontal = true;
    bool isVertical = true;

    #endregion
  
    void OnEnable()
    {
        arPlanes = new List<ARPlane>();

        arPlaneManager = FindObjectOfType<ARPlaneManager>();
        arPlaneManager.planesChanged += PlaneChanged;

    }
    private void OnDisable()
    {
        arPlaneManager.planesChanged -= PlaneChanged;
    }



    #region Horizontal and Vertical Plane Detect
    private void PlaneChanged(ARPlanesChangedEventArgs args)
    {


        if (args.added != null && args.added.Count > 0)

        {
            arPlanes.AddRange(args.added);
        }

        foreach (ARPlane plane in arPlanes.Where(plane => plane.extents.x * plane.extents.y >= 0.1f))

        {


            if (plane.alignment.IsVertical() && isVertical)
            {

                placedObject = Instantiate(verticalPrefab, plane.transform.position, Quaternion.identity);
                isVertical = false;
           
            }
            else if (plane.alignment.IsHorizontal() && isHorizontal)
            {

                placedObject = Instantiate(horizontalPrefab, plane.transform.position, Quaternion.identity);
                isHorizontal = false;
            }
        }

    }
    #endregion
}
