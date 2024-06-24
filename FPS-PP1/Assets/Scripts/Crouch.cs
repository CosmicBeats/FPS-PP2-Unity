using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] CharacterController PlayerHeight;
    [SerializeField] float Startspeed;
    [SerializeField] float ChrouchMod;
    [SerializeField] float normalHeight, crouchHeight;
    bool isCrouching;
  
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            PlayerHeight.height = crouchHeight;
            Startspeed /= ChrouchMod;
            isCrouching = true;

        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            PlayerHeight.height = normalHeight;
            Startspeed *= ChrouchMod;
            isCrouching = false;
        }
       
    }
}
