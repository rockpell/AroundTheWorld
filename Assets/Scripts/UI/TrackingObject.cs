using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingObject : MonoBehaviour
{
    [SerializeField] GameObject trackingObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(trackingObject != null)
        {
            transform.position = trackingObject.transform.position;
        }
    }
}
