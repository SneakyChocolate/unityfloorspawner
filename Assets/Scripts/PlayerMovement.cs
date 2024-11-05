using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movespeed = 12;
    // Start is called before the first frame update
    public CharacterController characterController;
    private Vector3 velocity;

    // Update is called once per frame
    void Update()
    {
        var f = Time.deltaTime;
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        characterController.Move(move * movespeed * f);

        if (characterController.isGrounded) {
            if (Input.GetKey(KeyCode.Space)) {
                characterController.Move(new Vector3(0,1,0));
            }
        }
        else {
            characterController.Move(new Vector3(0,-0.1f,0));
        }
    }
}
