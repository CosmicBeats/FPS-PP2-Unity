using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour

{

    [SerializeField] int sensitivity;

    [SerializeField] int lockVirtMax;
    [SerializeField] int lockVirtMin;

    [SerializeField] bool invertY;

    float rotX;
    // Start is called before the first frame update
    void Start()
    {
        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;

        //This was literally stopping me from makinig a main menu
    }

    // Update is called once per frame
    void Update()
    {
        //get input
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * sensitivity;
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * sensitivity;


        //invert
        if (invertY)
            rotX += mouseY;
        else
            rotX -= mouseY;


        //clamp the rotX on the x axis
        rotX = Mathf.Clamp(rotX, lockVirtMin, lockVirtMax);

        //rotate the camera on the x axis
        transform.localRotation = Quaternion.Euler(rotX, 0, 0);

        //rotate the player on the y axis
        //something
        transform.parent.Rotate(Vector3.up * mouseX);
    }
}
