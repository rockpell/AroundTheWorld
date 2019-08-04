using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pirate : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float selfDestroyTime;
    private float curTime;
    [SerializeField] private float detectRange;
    [SerializeField] private Transform shootPosition;

    private float decisionDegree;
    private float currentDegree;

    private GameObject playerShip;
    
    void Start()
    {
    }
    
    void Update()
    {
        selfDestroy();
        if(playerShip == null)
        {
            playerShip = GameManager.Instance.ShipBody.gameObject;
        }
        else
            moveShip();
    }

    private void rotateShip()
    {
        Vector3 _direction = playerShip.transform.position - this.transform.position;
        decisionDegree = this.transform.rotation.eulerAngles.z;
        //변경해야 할 각도(현재 각도에서 얼마나 더해주면 되는지)
        currentDegree = Quaternion.FromToRotation(transform.up, _direction).eulerAngles.z;
        
        if(currentDegree < 180)
        {
            decisionDegree += 1;
            this.transform.rotation = Quaternion.Euler(0, 0, decisionDegree);
        }
        else if(currentDegree > 180)
        {
            decisionDegree -= 1;
            this.transform.rotation = Quaternion.Euler(0, 0, decisionDegree);
        }
    }

    private void moveShip()
    {
        rotateShip();
        if((playerShip.transform.position - this.transform.position).magnitude <= detectRange)
        {
            this.transform.position += transform.up * speed * Time.deltaTime;
        }
    }

    private void selfDestroy()
    {
        curTime += Time.deltaTime;
        if (curTime > selfDestroyTime)
            Destroy(this.gameObject);
    }

    private void shipPlunder()
    {
        if(GameManager.Instance.Food >= 30)
        {
            Debug.Log("식량 30내놔");
            Destroy(this.gameObject);
        }
        else
        {
            Debug.Log("난파");
            Destroy(this.gameObject);
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            shipPlunder();
        }
    }
}
