using System.Collections;
using UnityEngine;
using UnityEngine.AI;


public class EnemyAI : MonoBehaviour, IDamage
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Renderer model;
    [SerializeField] Transform shootPos;

    [SerializeField] int Hp;

    [SerializeField] GameObject bullet;
    [SerializeField] float shootRate;

    bool isShooting;
    bool playerInRange;
    Color temp;

    

    // Start is called before the first frame update
    void Start()
    {
        
        GameManager.instance.UpdateGameGoalWin();
        GameManager.instance.AddEnemy(gameObject);
        temp = model.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        //Uncomment below for "Wave zomibies" and comment out the other line.
        //agent.SetDestination(GameManager.instance.player.transform.position);
        if (playerInRange)
        {
            agent.SetDestination(GameManager.instance.player.transform.position);
            if (!isShooting)
            {
                StartCoroutine(shoot());
            }
        }
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
        }
    }

    IEnumerator shoot()
    {
        isShooting = true;
        Instantiate(bullet, shootPos.position, transform.rotation);

        yield return new WaitForSeconds(shootRate);
        isShooting = false;
    }

    public void TakeDamage(int amount)
    {
        Hp -= amount;


       
        agent.SetDestination(GameManager.instance.player.transform.position);

        StartCoroutine(flashRed());
        if (Hp <= 0)
        {
            GameManager.instance.triggerScript.localEnemyCount--;
            GameManager.instance.triggerScript.UpdateEnemyCountText();
            GameManager.instance.RemoveEnemy(gameObject);
            Destroy(gameObject);
        }
    }

    IEnumerator flashRed()
    {

        model.material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        model.material.color = temp;
    }
}



