using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject ObjectToSpawn;
    private PlacementIndicator _placementIndicator;

    private void Awake()
    {
        _placementIndicator = FindObjectOfType<PlacementIndicator>();
    }

    private void Update()
    {
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            Instantiate(ObjectToSpawn, _placementIndicator.transform.position,
                _placementIndicator.transform.rotation);
        }
    }
}
