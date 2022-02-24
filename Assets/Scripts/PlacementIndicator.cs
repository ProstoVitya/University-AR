using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlacementIndicator : MonoBehaviour
{
    private ARRaycastManager _rayManager;
    private GameObject _visualMarker;

    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
        _rayManager = FindObjectOfType<ARRaycastManager>();
        _visualMarker = _transform.GetChild(0).gameObject;
        _visualMarker.SetActive(false);
    }

    private void Update()
    {
        var hits = new List<ARRaycastHit>();
        _rayManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.Planes);

        if (hits.Count > 0)
        {
            _transform.SetPositionAndRotation(hits[0].pose.position, hits[0].pose.rotation);
            if (!_visualMarker.activeInHierarchy)
                _visualMarker.SetActive(true);
        }
    }
}
