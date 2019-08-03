using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleLifeTime : MonoBehaviour
{
    [SerializeField] private float lifeTime;
    private float curTime;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        curTime += Time.deltaTime;
        if(curTime > lifeTime)
        {
            Destroy(this.gameObject);
        }
    }
}
