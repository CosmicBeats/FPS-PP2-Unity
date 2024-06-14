using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour ,IDamage
{
    public bool disable = false;

    [SerializeField] CharacterController controller;
    public int maxHP;
    public int MaxArmor;

    [SerializeField] int speed;
    [SerializeField] int sprintMod;
    [SerializeField] int jumpMax;
    [SerializeField] int jumpSpeed;
    [SerializeField] int gravity;
    [SerializeField] GameObject gunModel;
    [SerializeField] int shootDamage;
    [SerializeField] int shootDistance;
    [SerializeField] float shootRate;


    public GunStats gunStats;
    AudioSource gunAudioSource;
    ParticleSystem gunParticle;
    [SerializeField] Animator gunAnimator;




    public List<GunStats> gunList = new List<GunStats>();

    public int currentArmor;
    Vector3 moveDir;
    Vector3 playerVelocity;
    public int selectedGun;
    int jumpCount;
    public int currentHP;
    bool isShooting;

    //audio
    [SerializeField] AudioSource aud;
    [SerializeField] AudioClip[] audJump;
    [Range(0,1)] [SerializeField] float audJumpVol;

    
    [SerializeField] AudioClip[] audDmg;
    [Range(0, 1)][SerializeField] float audDmgVol;

    [SerializeField] AudioClip[] audStep;
    [Range(0, 1)][SerializeField] float audStepVol;

    [Range(2, 4)][SerializeField] int audSprintSpeed;

    bool playingSteps;
    bool isSprinting;


    // Start is called before the first frame update
    void Start()
    {
        //currentArmor =MaxArmor;
        //currentHP = maxHP;
        //updatePlayerUI();

        SpawnPlayer();
    }

    void OnEnable()
    {
        if (gunList.Count > 0 && selectedGun >= 0 && selectedGun < gunList.Count)
        {
            gunList[selectedGun].isReloading = false;

        }
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * shootDistance, Color.red);
        if (!disable)
        {
            Movement();
            SelectGun();

            Reloading();
        }
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

        if (Input.GetButton("Shoot") && gunList.Count > 0 && gunList[selectedGun].currentAmmo > 0 && !isShooting && !gunList[selectedGun].isReloading)
        {
            StartCoroutine(Shoot());
        }

        if (Input.GetButtonDown("Jump") && jumpCount < jumpMax)
        {
            //jump audio
            aud.PlayOneShot(audJump[Random.Range(0, audJump.Length)], audJumpVol);
            jumpCount++;
            playerVelocity.y = jumpSpeed;
        }
        playerVelocity.y -= gravity * Time.deltaTime;

        controller.Move(playerVelocity * Time.deltaTime);

        //footstep audio
        if(controller.isGrounded && moveDir.normalized.magnitude > 0.3f && !playingSteps) 
        {
            StartCoroutine(playSteps());
        }

    }
    void Sprint()
    {
        if (Input.GetButtonDown("Sprint"))
        {
            speed *= sprintMod;
            isSprinting = true; 
        }
        else if (Input.GetButtonUp("Sprint"))
        {
            speed /= sprintMod;
            isSprinting = false;
        }

    }

    IEnumerator playSteps() 
    {
        playingSteps = true;

        aud.PlayOneShot(audStep[Random.Range(0, audStep.Length)], audStepVol);

        if (!isSprinting)
        {
            yield return new WaitForSeconds(0.5f);
        }
        else 
        {
            yield return new WaitForSeconds(0.3f);
        }

        

        playingSteps = false;
    }

    IEnumerator Shoot()
    {
        isShooting = true;

        RaycastHit hit;

        gunList[selectedGun].currentAmmo--;
        GameManager.instance.CurrentAmmoText.text = gunList[selectedGun].currentAmmo.ToString("F0");
        GameManager.instance.MaxAmmoText.text = gunList[selectedGun].maxAmmo.ToString("F0");

        gunParticle.Play();

        gunAudioSource.Play();


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

    public void Reloading()
    {
        if (gunList.Count > 0 && selectedGun >= 0 && selectedGun < gunList.Count)
        {
            if (gunList[selectedGun].totalAmmo > 0)
            {
                if (gunList[selectedGun].isReloading || isShooting)
                {
                    return;
                }
                else if ((gunList[selectedGun].currentAmmo <= 0 && Input.GetButtonDown("Reload")) || Input.GetButtonDown("Reload"))
                {
                    StartCoroutine(Reload());
                }
            }
        }
    }

    public void TakeDamage(int amount)
    {
        aud.PlayOneShot(audDmg[Random.Range(0, audDmg.Length)], audDmgVol);

        if (currentArmor > 0)
        {

            currentHP -= amount/2;
            currentArmor -= 1;
        }
        else
        {
            currentHP -= amount;
        }
            updatePlayerUI();
            StartCoroutine(FlashScreenDamage());

        if (currentHP <= 0)
        {
            GameManager.instance.StateLose();
            GameManager.instance.loseAnimation.SetTrigger("Lose Trigger");
            
        }
    }

    public void GetGunStats(GunStats gunStats)
    {
        gunStats.isReloading = false;

        gunList.Add(gunStats);


        selectedGun = gunList.Count - 1;

        shootDamage = gunStats.shootDamage;
        shootDistance = gunStats.shootDistance;
        shootRate = gunStats.shootRate;
        gunParticle = gunStats.gunParticle;
        gunModel.GetComponent<MeshFilter>().sharedMesh = gunStats.gunModel.GetComponent<MeshFilter>().sharedMesh;
        gunModel.GetComponent<MeshRenderer>().sharedMaterial = gunStats.gunModel.GetComponent<MeshRenderer>().sharedMaterial;
        UpdateGunSound();

        ChangeGun();


    }

    void SelectGun()
    {

        if (gunList.Count == 0 || selectedGun < 0 || selectedGun >= gunList.Count)
        {
            return;
        }

        if (gunList[selectedGun].isReloading)
        {
            return;
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0 && selectedGun < gunList.Count - 1)
        {
            selectedGun++;

            UpdateGunSound();

            ChangeGun();
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0 && selectedGun > 0)
        {
            selectedGun--;

            UpdateGunSound();

            ChangeGun();
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
        GameManager.instance.playerArmorBar.fillAmount = (float)currentArmor / MaxArmor;
    }

    void UpdateGunSound()
    {
        if (gunList.Count > 0 && selectedGun >= 0 && selectedGun < gunList.Count)
        {
            gunAudioSource.clip = gunList[selectedGun].shootSound;

        }
    }

    void ChangeGun()
    {
        if (gunList.Count > 0 && selectedGun >= 0 && selectedGun < gunList.Count)
        {
            GunStats currentGun = gunList[selectedGun];

            shootDamage = currentGun.shootDamage;
            shootDistance = currentGun.shootDistance;
            shootRate = currentGun.shootRate;

            gunParticle = currentGun.gunParticle;

            gunModel.GetComponent<MeshFilter>().sharedMesh = currentGun.gunModel.GetComponent<MeshFilter>().sharedMesh;
            gunModel.GetComponent<MeshRenderer>().sharedMaterial = currentGun.gunModel.GetComponent<MeshRenderer>().sharedMaterial;

            GameManager.instance.CurrentAmmoText.text = gunList[selectedGun].currentAmmo.ToString("F0");
            GameManager.instance.MaxAmmoText.text = gunList[selectedGun].maxAmmo.ToString("F0");
            GameManager.instance.TotalAmmoText.text = gunList[selectedGun].totalAmmo.ToString("F0");
        }
    }

    IEnumerator Reload() 
    {
        if (gunList.Count > 0 && selectedGun >= 0 && selectedGun < gunList.Count)
        {
            gunList[selectedGun].isReloading = true;

            gunAnimator.SetBool("Reloading", true);

            yield return new WaitForSeconds(gunList[selectedGun].reloadTime - .25f);


            gunList[selectedGun].isReloading = false;

            gunAnimator.SetBool("Reloading", false);
            yield return new WaitForSeconds(.25f);

            if (gunList[selectedGun].totalAmmo >= gunList[selectedGun].maxAmmo)
            {
                if (gunList[selectedGun].currentAmmo > 0)
                {
                    gunList[selectedGun].totalAmmo = gunList[selectedGun].totalAmmo - (gunList[selectedGun].maxAmmo - gunList[selectedGun].currentAmmo);
                    GameManager.instance.TotalAmmoText.text = gunList[selectedGun].totalAmmo.ToString("F0");
                }
                else if (gunList[selectedGun].currentAmmo <=0)
                {
                    gunList[selectedGun].totalAmmo -= gunList[selectedGun].maxAmmo;
                    GameManager.instance.TotalAmmoText.text = gunList[selectedGun].totalAmmo.ToString("F0");
                }
                gunList[selectedGun].currentAmmo = gunList[selectedGun].maxAmmo;
            }else
            {
                gunList[selectedGun].currentAmmo = gunList[selectedGun].totalAmmo;
                gunList[selectedGun].totalAmmo = 0;
                GameManager.instance.TotalAmmoText.text = gunList[selectedGun].totalAmmo.ToString("F0");
            }

            GameManager.instance.CurrentAmmoText.text = gunList[selectedGun].maxAmmo.ToString("F0");
        }
            GameManager.instance.CurrentAmmoText.text = gunList[selectedGun].currentAmmo.ToString("F0");
    }

    public void SpawnPlayer()
    {
        currentHP = maxHP;
        currentArmor = 0;
        gunAudioSource = gameObject.AddComponent<AudioSource>(); 

        UpdateGunSound();

        updatePlayerUI();
        controller.enabled = false;
        transform.position = GameManager.instance.playerSpawnPos.transform.position;
        controller.enabled = true;
    }
}
