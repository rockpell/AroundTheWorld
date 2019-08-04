using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPort : MonoBehaviour
{
    private bool isEndGame = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (!isEndGame && coll.transform.tag == "Player")
        {
            isEndGame = true;

            GameManager.Instance.NowGameEnding = GameEnding.ARRIVE;
        }
    }
}
