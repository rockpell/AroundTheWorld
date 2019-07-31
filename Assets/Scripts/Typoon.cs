using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Typoon : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Vector3 origin;
    [SerializeField] private Vector3 destination;

    [SerializeField] private Vector3 direction;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        move();
    }

    public void initialize(Vector3 origin, Vector3 destination, float speed)
    {
        Debug.Log("Init");
        this.speed = speed;
        this.origin = origin;
        this.destination = destination;

        direction = destination - origin;

        this.transform.position = origin;
    }
    private void move()
    {
        Debug.Log("Mov");
        if (direction != Vector3.zero)
        {
            this.gameObject.transform.position += direction.normalized * speed * Time.deltaTime;
        }
        if(direction.magnitude < (this.gameObject.transform.position - origin).magnitude)
        {
            Destroy(this.gameObject);
        }
    }
}
