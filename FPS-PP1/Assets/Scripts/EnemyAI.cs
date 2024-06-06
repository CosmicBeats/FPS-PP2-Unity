using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;



public class EnemyAI : MonoBehaviour, IDamage
{
    //just make the enemy work
    [SerializeField] Animator anim;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Renderer model;
    [SerializeField] Renderer[] robotSoldierModelParts;

    [SerializeField] Transform shootPos;
    [SerializeField] Transform headPos;

    [SerializeField] int Hp;
    [SerializeField] int viewAngle;
    [SerializeField] int faceTargetSpeed;
    [SerializeField] int animSpeedTrans;
    [SerializeField] int roamDist;
    [SerializeField] int roamTimer;

    [SerializeField] GameObject bullet;
    [SerializeField] float shootRate;
    [SerializeField] bool doesDropKey;
    [SerializeField] GameObject keyToDrop;


    // Audio related stuff credit goes to mike.
    [SerializeField] AudioSource aud;
    
    [SerializeField] AudioClip[] audShoot;//enemy shooting audio
    [Range(0, 1)][SerializeField] float audShootVol;

    [SerializeField] AudioClip[] audHurt; //enemy taking damage audio
    [Range(0, 1)][SerializeField] float audHurtVol;

    bool isShooting;
    bool playerInRange;
    bool destChosen;

       

    Color temp;
    Color tempRobot;
    Vector3 playerDir;
    Vector3 startingPos;


    
    [SerializeField] GameObject trigger;
    float angleToPlayer;
    float stoppingDisOrig;


    // Start is called before the first frame update
    void Start()
    {
        foreach (var robotPart in robotSoldierModelParts)
        {
            tempRobot = robotPart.sharedMaterial.color;
        }
        //temp = model.material.color;

        startingPos = transform.position;
        stoppingDisOrig = agent.stoppingDistance;
    }

    // Update is called once per frame
    void Update()
    {
        //Uncomment below for "Wave zomibies" and comment out the other line.
        //agent.SetDestination(GameManager.instance.player.transform.position);
        //UNCOMMIT for can see player
          float animSpeed = agent.velocity.normalized.magnitude;
          anim.SetFloat("Speed", Mathf.Lerp(anim.GetFloat("Speed"), animSpeed, Time.deltaTime * animSpeedTrans));
        
        if (playerInRange && canSeePlayer())
        {
            //agent.SetDestination(GameManager.instance.player.transform.position);
             if (!isShooting)
             {
                 StartCoroutine(shoot());
             }
            StartCoroutine(roam());
                   
        }
        else if (!playerInRange)
        {
            StartCoroutine(roam());
        }

    }
    IEnumerator roam()
    {
        if (!destChosen && agent.remainingDistance < 0.05f)
        {
            destChosen = true;
            agent.stoppingDistance = 0;
            yield return new WaitForSeconds(roamTimer);

            Vector3 ranPos = Random.insideUnitSphere * roamDist;
            ranPos += startingPos; //alwasy ref starting pos

            NavMeshHit hit;

            NavMesh.SamplePosition(ranPos, out hit, roamDist, 1);
            agent.SetDestination(hit.position);
            destChosen = false;
        }
    }
    //head model needed 
    bool canSeePlayer()
    {
        playerDir = GameManager.instance.player.transform.position - headPos.position;
        angleToPlayer = Vector3.Angle(new Vector3(playerDir.x, playerDir.y + 1, playerDir.z), transform.forward);
        
        //if i want to poit at the raycast new Vector3(playerDir.x,playerDir.y + 1,playerDir.z), transform.forward
        Debug.DrawRay(headPos.position, playerDir);

        RaycastHit hit;
        if (Physics.Raycast(headPos.position, playerDir, out hit))
        {
            if (hit.collider.CompareTag("Player") && angleToPlayer <= viewAngle)
            {
                agent.stoppingDistance = stoppingDisOrig;
                //making the player destination
                agent.SetDestination(GameManager.instance.player.transform.position);
                //then start shooting
                if (!isShooting)
                {
                    StartCoroutine(shoot());
                }
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    faceTarget();
                }
                return true;
            }
        }
        agent.stoppingDistance = 0;
        return false;
    }
    void faceTarget()
    {
        Quaternion rot = Quaternion.LookRotation(new Vector3(playerDir.x, 0, playerDir.z));
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * faceTargetSpeed);
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            agent.stoppingDistance = 0;
        }
    }

    IEnumerator shoot()
    {
        //Credit Mike
        aud.PlayOneShot(audShoot[Random.Range(0, audShoot.Length)], audShootVol);

        isShooting = true;
        anim.SetTrigger("Shoot");
        Instantiate(bullet, shootPos.position, transform.rotation);

        yield return new WaitForSeconds(shootRate);
        isShooting = false;
    }
    public void createBullet()
    {
        Instantiate(bullet, shootPos.position, transform.rotation);
    }

    public void TakeDamage(int amount)
    {
        Hp -= amount;

        // Credit Mike
        aud.PlayOneShot(audHurt[Random.Range(0, audHurt.Length)], audHurtVol);

        agent.SetDestination(GameManager.instance.player.transform.position);

        StartCoroutine(flashRed());
        if (Hp <= 0)
        {
            if(doesDropKey)
            {
                Instantiate(keyToDrop);
            }
            GameManager.instance.UpdateGameGoalWin(-1);
            
            Destroy(gameObject);
        }
    }

    IEnumerator flashRed()
    {
        foreach (var robotPart in robotSoldierModelParts)
        {
            robotPart.sharedMaterial.color = Color.red;
        }
        yield return new WaitForSeconds(0.1f);
        foreach (var robotPart in robotSoldierModelParts)
        {
            robotPart.sharedMaterial.color = tempRobot;
        }

        //model.material.color = Color.red;
        //yield return new WaitForSeconds(0.1f);
        //model.material.color = temp;


    }
}



