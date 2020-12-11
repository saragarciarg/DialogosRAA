using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float rotationSpeed = 20f;
    public float movementSpeed = 10f;

    private float hmove = 0f;
    private float vmove = 0f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        hmove = Input.GetAxis("Horizontal");
        vmove = Input.GetAxis("Vertical");
        Rotate();
        // Move (keyboard keys)
        Move();
    }

    void Rotate()
    {
        transform.Rotate(new Vector3(vmove, -hmove, 0f) *
        Time.deltaTime * rotationSpeed);
    }

    void Move()
    {
        if (Input.GetKey(KeyCode.W))
            transform.Translate(Vector3.forward * Time.deltaTime *
            movementSpeed);
        else if (Input.GetKey(KeyCode.S))
            transform.Translate(Vector3.back * Time.deltaTime *
            movementSpeed);
        else if (Input.GetKey(KeyCode.A))
            transform.Translate(Vector3.left * Time.deltaTime *
            movementSpeed);
        else if (Input.GetKey(KeyCode.D))
            transform.Translate(Vector3.right * Time.deltaTime *
            movementSpeed);
    }
}
