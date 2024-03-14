using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Numerics;
using System.Security.Cryptography;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;
using static UnityEngine.EventSystems.EventTrigger;
using static UnityEngine.GraphicsBuffer;
using Quaternion = UnityEngine.Quaternion;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] float attackRange = 2f;
    [SerializeField] float attackCooldown = 3f;

    private Transform currentTarget;
    private NavMeshAgent agent;
    private float lastAttackTime;

    private bool aitakeD = false;
    [SerializeField] float aiheath = 100;

    //meterial
    [SerializeField] LODGroup lodGroup;
    private int currentLODIndex = 0;
    //this
    [SerializeField] GameObject thisobj;
    //energy
    [SerializeField] GameObject energyprehab;

    [SerializeField] float difenergy = 1;
    UnityEngine.Vector3 EnemyTargetPosition;

    //fighton
    void Start()
    {
        this.gameObject.transform.position = new UnityEngine.Vector3(Random.Range(-9, 2), 0, Random.Range(-9, 2));

        FindNextTarget();

        agent = GetComponent<NavMeshAgent>();
        aitakeD = false;
    }

    void Update()
    {
        FindNextTarget();
        if (EnemyTargetPosition != null)
        {
            if (UnityEngine.Vector3.Distance(transform.position, EnemyTargetPosition) <= attackRange)
            {
                if (Time.time - lastAttackTime >= attackCooldown)
                {
                    lastAttackTime = Time.time;
                }

            }
            else
            {


                agent.destination = EnemyTargetPosition;
                if (currentTarget != null)
                {
                    UnityEngine.Vector3 lookDirection = currentTarget.position - transform.position;

                    lookDirection.y = 0;
                    transform.rotation = Quaternion.LookRotation(lookDirection);
                }
            }
        }
    }

    void FindNextTarget()
    {

        GameObject[] players = GameObject.FindGameObjectsWithTag("player");
        float closestDistanceE = Mathf.Infinity;
        if(players.Length == 0) 
        {
            SceneManager.LoadScene("zEndScreen");
        }
        foreach (GameObject player in players)
        {
            float distance = UnityEngine.Vector3.Distance(transform.position, player.transform.position);
            if (distance < closestDistanceE)
            {
                closestDistanceE = distance;
                EnemyTargetPosition = player.transform.position;
            }

        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "player")
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("player");

        GameObject closestPlayer = null;
        float closestDistance = Mathf.Infinity;


        foreach (GameObject player in players)
        {
            float distance = UnityEngine.Vector3.Distance(transform.position, player.transform.position);
            if (distance < closestDistance)
            {
                closestPlayer = player;
                closestDistance = distance;
            }
        }
            if (closestPlayer != null)
            {
                wlkrandom component = closestPlayer.GetComponent<wlkrandom>();
                bool some = component.fight;

                if (some)
                {
                    aitakeD = true;
                    StartCoroutine(AiD());

                }
            }
        }
            if (other.gameObject.tag == "Damage")
            {
                StartCoroutine(AiD());
                aitakeD = true;
            }
        }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "player")
        {
            aitakeD = false;
            StopCoroutine(AiD());
            currentLODIndex = 2 % lodGroup.lodCount;
            lodGroup.ForceLOD(currentLODIndex);

        }
    }
            IEnumerator AiD()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("player");
        if (players.Length > 0)
        {
            if (aitakeD)
            {
                currentLODIndex = 3 % lodGroup.lodCount;

                lodGroup.ForceLOD(currentLODIndex);

                aiheath = aiheath - 20;

                if (aiheath < 1)
                {
                    if (difenergy == 1)
                    {
                        Spawnclick.energy = Spawnclick.energy + 50;
                        Instantiate(energyprehab, new UnityEngine.Vector3(transform.position.x, transform.position.y + 1, transform.position.z), energyprehab.transform.rotation);
                        Instantiate(energyprehab, new UnityEngine.Vector3(transform.position.x + 1, transform.position.y + 1, transform.position.z), energyprehab.transform.rotation);
                    }
                    else
                      if (difenergy == 2)
                    {
                        Spawnclick.energy = Spawnclick.energy + 75;
                        Instantiate(energyprehab, new UnityEngine.Vector3(transform.position.x + 1, transform.position.y + 1, transform.position.z), energyprehab.transform.rotation);
                        Instantiate(energyprehab, new UnityEngine.Vector3(transform.position.x + 1, transform.position.y + 1, transform.position.z), energyprehab.transform.rotation);
                        Instantiate(energyprehab, new UnityEngine.Vector3(transform.position.x + 1, transform.position.y + 1, transform.position.z), energyprehab.transform.rotation);
                    }
                    else
                      if (difenergy == 3)
                    {
                        Spawnclick.energy = Spawnclick.energy + 100;
                        Instantiate(energyprehab, new UnityEngine.Vector3(transform.position.x + 1, transform.position.y + 1, transform.position.z), energyprehab.transform.rotation);
                        Instantiate(energyprehab, new UnityEngine.Vector3(transform.position.x + 1, transform.position.y + 1, transform.position.z), energyprehab.transform.rotation);
                        Instantiate(energyprehab, new UnityEngine.Vector3(transform.position.x + 1, transform.position.y + 1, transform.position.z), energyprehab.transform.rotation);
                        Instantiate(energyprehab, new UnityEngine.Vector3(transform.position.x + 1, transform.position.y + 1, transform.position.z), energyprehab.transform.rotation);
                    }
                    else
                      if (difenergy == 4)
                    {
                        Spawnclick.energy = Spawnclick.energy + 125;
                        Instantiate(energyprehab, new UnityEngine.Vector3(transform.position.x + 1, transform.position.y + 1, transform.position.z), energyprehab.transform.rotation);
                        Instantiate(energyprehab, new UnityEngine.Vector3(transform.position.x + 1, transform.position.y + 1, transform.position.z), energyprehab.transform.rotation);
                        Instantiate(energyprehab, new UnityEngine.Vector3(transform.position.x + 1, transform.position.y + 1, transform.position.z), energyprehab.transform.rotation);
                        Instantiate(energyprehab, new UnityEngine.Vector3(transform.position.x + 1, transform.position.y + 1, transform.position.z), energyprehab.transform.rotation);
                        Instantiate(energyprehab, new UnityEngine.Vector3(transform.position.x + 1, transform.position.y + 1, transform.position.z), energyprehab.transform.rotation);
                        Instantiate(energyprehab, new UnityEngine.Vector3(transform.position.x + 1, transform.position.y + 1, transform.position.z), energyprehab.transform.rotation);
                    }
                    else
                      if (difenergy == 5)
                    {
                        Spawnclick.energy = Spawnclick.energy + 150;
                        Instantiate(energyprehab, new UnityEngine.Vector3(transform.position.x + 1, transform.position.y + 1, transform.position.z), energyprehab.transform.rotation);
                        Instantiate(energyprehab, new UnityEngine.Vector3(transform.position.x + 1, transform.position.y + 1, transform.position.z), energyprehab.transform.rotation);
                        Instantiate(energyprehab, new UnityEngine.Vector3(transform.position.x + 1, transform.position.y + 1, transform.position.z), energyprehab.transform.rotation);
                        Instantiate(energyprehab, new UnityEngine.Vector3(transform.position.x + 1, transform.position.y + 1, transform.position.z), energyprehab.transform.rotation);
                        Instantiate(energyprehab, new UnityEngine.Vector3(transform.position.x + 1, transform.position.y + 1, transform.position.z), energyprehab.transform.rotation);
                        Instantiate(energyprehab, new UnityEngine.Vector3(transform.position.x + 1, transform.position.y + 1, transform.position.z), energyprehab.transform.rotation);
                        Instantiate(energyprehab, new UnityEngine.Vector3(transform.position.x + 1, transform.position.y + 1, transform.position.z), energyprehab.transform.rotation);

                    }
                    else
                      if (difenergy == 6)
                    {
                        Spawnclick.energy = Spawnclick.energy + 200;
                        Instantiate(energyprehab, new UnityEngine.Vector3(transform.position.x + 1, transform.position.y + 1, transform.position.z), energyprehab.transform.rotation);
                        Instantiate(energyprehab, new UnityEngine.Vector3(transform.position.x + 1, transform.position.y + 1, transform.position.z), energyprehab.transform.rotation);
                        Instantiate(energyprehab, new UnityEngine.Vector3(transform.position.x + 1, transform.position.y + 1, transform.position.z), energyprehab.transform.rotation);
                        Instantiate(energyprehab, new UnityEngine.Vector3(transform.position.x + 1, transform.position.y + 1, transform.position.z), energyprehab.transform.rotation);
                        Instantiate(energyprehab, new UnityEngine.Vector3(transform.position.x + 1, transform.position.y + 1, transform.position.z), energyprehab.transform.rotation);
                        Instantiate(energyprehab, new UnityEngine.Vector3(transform.position.x + 1, transform.position.y + 1, transform.position.z), energyprehab.transform.rotation);
                        Instantiate(energyprehab, new UnityEngine.Vector3(transform.position.x + 1, transform.position.y + 1, transform.position.z), energyprehab.transform.rotation);
                        Instantiate(energyprehab, new UnityEngine.Vector3(transform.position.x + 1, transform.position.y + 1, transform.position.z), energyprehab.transform.rotation);
                    }
                    Destroy(this.gameObject);
                }
                yield return new WaitForSeconds(1);
                currentLODIndex = 2 % lodGroup.lodCount;

                lodGroup.ForceLOD(currentLODIndex);
                yield return new WaitForSeconds(1);
                aitakeD = true;
                StartCoroutine(AiD());
            }
        }
    }
}


    
