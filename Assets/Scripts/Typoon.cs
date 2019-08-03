using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Typoon : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Vector3 origin;
    [SerializeField] private Vector3 destination;

    [SerializeField] private Vector3 direction;

    [SerializeField] private float rotationSpeed;
    private float currentRotation;
    void Start()
    {
        currentRotation = this.transform.rotation.eulerAngles.z;
    }

    // Update is called once per frame
    void Update()
    {
        move();
        rotateBody();
    }

    public void initialize(Vector3 origin, Vector3 destination, float speed)
    {
        this.speed = speed;
        this.origin = origin;
        this.destination = destination;

        direction = destination - origin;

        this.transform.position = origin;
    }
    private void move()
    {
        if (direction != Vector3.zero)
        {
            this.gameObject.transform.position += direction.normalized * speed * Time.deltaTime;
        }
        if(direction.magnitude < (this.gameObject.transform.position - origin).magnitude)
        {
            Destroy(this.gameObject);
        }
    }
    private void rotateBody()
    {
        currentRotation += rotationSpeed * 0.016f;
        this.transform.rotation = Quaternion.Euler(0, 0, currentRotation);
    }
}
