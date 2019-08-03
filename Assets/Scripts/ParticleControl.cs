﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleControl : MonoBehaviour
{
    [SerializeField] private ParticleSystem subEmitter;

    [SerializeField] private Sail sail;

    [SerializeField] private float createTime;

    private float createTimeCounter;

    void Update()
    {
        createTimeCounter += Time.deltaTime;

        if(createTimeCounter > createTime)
        {
            if(sail.CurrentSpeed > 1)
                Instantiate(subEmitter, this.transform.position, Quaternion.identity);
            createTimeCounter = 0;
        }
    }
}