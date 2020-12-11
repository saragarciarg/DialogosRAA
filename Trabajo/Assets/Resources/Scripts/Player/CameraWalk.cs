using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraWalk : MonoBehaviour
{
    public int speed = 15, turnspeed = 10;
    private float inputX , inputZ = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        inputX = Input.GetAxis("Horizontal");
        inputZ = Input.GetAxis("Vertical");
        if (inputX != 0)
            Rotate();
        if (inputZ != 0)
            Move();
    }

    private void Move(){
        transform.position += transform.forward * inputZ * speed *
        Time.deltaTime;
}
    private void Rotate(){
        transform.Rotate(new Vector3(0f, inputX * turnspeed *
        Time.deltaTime));
    }
}
