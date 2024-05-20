using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour ,IDamage
{
    

    [SerializeField] CharacterController controller;
    public int maxHP;
    [SerializeField] int speed;
    [SerializeField] int sprintMod;
    [SerializeField] int jumpMax;
    [SerializeField] int jumpSpeed;
    [SerializeField] int gravity;

    [SerializeField] int shootDamage;
    [SerializeField] int shootDistance;
    [SerializeField] float shootRate;

    Vector3 moveDir;
    Vector3 playerVelocity;
    int jumpCount;
    public int currentHP;
    bool isShooting;
   
    // Start is called before the first frame update
    void Start()
    {
       
        currentHP = maxHP;
        updatePlayerUI();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * shootDistance, Color.red);
        Movement();
    }
    void Movement()
    {
        if (controller.isGrounded)
        {
            jumpCount = 0;
            playerVelocity = Vector3.zero;
        }

        moveDir = Input.GetAxis("Horizontal") * transform.right
                        + Input.GetAxis("Vertical") * transform.forward;
        controller.Move(moveDir * speed * Time.deltaTime);

        Sprint();

        if (Input.GetButton("Shoot") && !isShooting)
        {
            StartCoroutine(Shoot());
        }

        if (Input.GetButtonDown("Jump") && jumpCount < jumpMax)
        {
            jumpCount++;
            playerVelocity.y = jumpSpeed;
        }
        playerVelocity.y -= gravity * Time.deltaTime;

        controller.Move(playerVelocity * Time.deltaTime);

    }
    void Sprint()
    {
        if (Input.GetButtonDown("Sprint"))
        {
            speed *= sprintMod;
        }
        else if (Input.GetButtonUp("Sprint"))
        {
            speed /= sprintMod;
        }

    }
    IEnumerator Shoot()
    {
        isShooting = true;

        RaycastHit hit;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, shootDistance))
        {
            IDamage dmg = hit.collider.GetComponent<IDamage>();
            if (hit.transform != transform && dmg != null)
            {
                dmg.TakeDamage(shootDamage);
            }
        }
        yield return new WaitForSeconds(shootRate);
        isShooting = false;
    }

    public void TakeDamage(int amount)
    {
        currentHP -= amount;
        updatePlayerUI();
        StartCoroutine(FlashScreenDamage());

        if (currentHP <= 0)
        {
            GameManager.instance.StateLose();
            GameManager.instance.loseAnimation.SetTrigger("Lose Trigger");
            
        }
    }

    public void Heal(int healPoints)
    {
        currentHP += healPoints;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);
        updatePlayerUI();
    }

    IEnumerator FlashScreenDamage()
    {
        GameManager.instance.playerFlashDamage.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        GameManager.instance.playerFlashDamage.SetActive(false);
    }
    public void updatePlayerUI()
    {
        GameManager.instance.playerHPBar.fillAmount = (float)currentHP / maxHP;
    }
}
