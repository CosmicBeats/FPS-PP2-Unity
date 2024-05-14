using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

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
        //UNcommit when game goal is up
        GameManager.instance.UpdateGameGoalWin(1);
         temp = model.material.color;
    }

    // Update is called once per frame
    void Update()
    {
       
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

    public void takeDamage(int amount)
    {
        Hp -= amount;
       
        agent.SetDestination(GameManager.instance.player.transform.position);

        StartCoroutine(flashRed());
        if (Hp <= 0)
        {
            //UNcommit when game goal is up
            GameManager.instance.UpdateGameGoalWin(-1);
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
