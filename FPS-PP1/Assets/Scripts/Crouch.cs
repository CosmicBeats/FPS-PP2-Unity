using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] CharacterController PlayerHeight;
    [SerializeField] float normalHeight, crouchHeight;
  
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            PlayerHeight.height = crouchHeight;

        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            PlayerHeight.height = normalHeight;
        }
    }
}
