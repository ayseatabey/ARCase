using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
//using DG.Tweening;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
public class DraggableUIObject : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    #region Raycast

    private RaycastHit _hit;
    private Ray _ray;
    public Camera _viewCamera;
    #endregion

    #region AR Raycast
    public ARRaycastManager arRaycastManager;
    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();
    private int _layerMask;
    #endregion


    private GameObject _movingObj;
    private Vector3 _startPos;

  
    private ObjectFeature _objectFeature;

   
    public AudioSource source;
    private Vector2 touchPosition = default;


    private void Start()
    {
        _objectFeature = DragDropManager.Instance.GetObjectFeature(transform);
        _layerMask = DragDropManager.Instance.DraggingLayerMask;

    }

    /// <summary>
    /// For modified object check  distance between modifier object to modified object 
    /// </summary>
    /// <param name="IsItCloseEnough"></param>
    private bool IsItCloseEnough(Vector3 pos)
    {
        return Vector3.Distance(pos, GetTargetPoint()) <= DragDropManager.Instance.MatchDistance;
    }



    //Instantiate new object when run OnBeginDrag

    public void OnBeginDrag(PointerEventData eventData)
    {
         if (_movingObj == null)
        {
            _ray = _viewCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(_ray, out _hit, Mathf.Infinity))
            {
                _movingObj = Instantiate(_objectFeature.MovingPrefab, _hit.point, _objectFeature.MovingPrefab.transform.rotation);
                _startPos = _hit.point;
            }
        }
    }


    //drag action 

    public void OnDrag(PointerEventData eventData)
    {
        if (_movingObj != null)
        {
            _ray = _viewCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(_ray, out _hit, Mathf.Infinity,_layerMask))
            {
                _movingObj.transform.position = _hit.point;
                _movingObj.GetComponent<Outline>().enabled = true;


            }
        }

    }


  //when user success get own object sound
    void GetSound()
    {

        source.clip = _objectFeature.successSound;
        source.Play();

    }

    //For modified object get position
    private Vector3 GetTargetPoint()
    {

        return _movingObj.transform.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Touch touch = Input.GetTouch(0);

        touchPosition = touch.position;
        _ray = _viewCamera.ScreenPointToRay(Input.mousePosition);
        if (arRaycastManager.Raycast(touchPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
        {


            Pose hitPose = hits[0].pose;


            switch (_objectFeature.operation)
            {
                //create new object in AR Field
                case ObjectFeature.OperationType.CreateNewObject:
                    if (_movingObj != null)
                    {
                       


                            _movingObj.transform.position = _hit.point;
                        _movingObj.GetComponent<Outline>().enabled = false;


                        GetSound();
                        _movingObj = null;
                        Destroy(gameObject);
                
                    }

                    break;
                    //modified a object already we have AR Field
                case ObjectFeature.OperationType.ChangeLook:
                    {

                      
                         if (_movingObj != null)
                    {
                        _movingObj.transform.position = hitPose.position;

                        foreach (ModifiedObject modifiedObject in FindObjectsOfType<ModifiedObject>())
                        {
                                ModifierObject modifierObject = _movingObj.GetComponent<ModifierObject>();
                             if (modifiedObject.type == modifierObject.refObject.type && IsItCloseEnough(modifiedObject.transform.position))
                            {
                                    modifiedObject.gameObject.GetComponentInChildren<Renderer>().material.SetTexture("_MainTex", modifierObject.target);

                                    GetSound();
                                    Destroy(_movingObj);
                                    _movingObj = null;
                                 
                            }
                            else
                                Destroy(_movingObj);

                        }





                    }

                    else
                    {
                        Destroy(_movingObj);
                    }

                    }
                    break;
                default:
                    break;
            }



        }



        else
            Destroy(_movingObj);


    }

   


}