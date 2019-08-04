using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wreck : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //showSaveCrewEvent
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.transform.tag == "Player")
        {
            UIManager.Instance.showSaveCrewEvent();

            Destroy(this.gameObject);
        }
    }
}
