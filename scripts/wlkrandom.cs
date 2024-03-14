using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SocialPlatforms;
using UnityEngine.UIElements;

public class wlkrandom : MonoBehaviour
{
    //color
    [SerializeField] Renderer head;

    [SerializeField] Renderer body;
    //movement

    [SerializeField] GameObject posthis;

    Vector3 TargetPosition;
    [SerializeField] NavMeshAgent agent;

    private float timeToChangeDirection;

    public static bool walk = false;


    //sowrd
    private bool sword = false;
    [SerializeField] GameObject swordactive;
    //shield
    private bool shield = false;
    [SerializeField] GameObject shieldactive;
    //bow
    private bool bow = false;
    [SerializeField] GameObject bowactive;
    [SerializeField] GameObject arrowprhab;
    bool bowshoot = true;
    bool checkifem = false;

    //start
    GameObject parent;
    [SerializeField] Rigidbody avtavate;
    //chagncolor
    bool Changecolor = false;
    [SerializeField] int heath = 150;
    //fight enemy

    public bool fight = false;
    //enemy tag
    string weaponTag;
    int trytime;
    GameObject[] weapons;

    public static bool FightEnemy = false;

    //sound
    [SerializeField] AudioSource bowS;
    [SerializeField] AudioSource shieldS;
    [SerializeField] AudioSource swordS;
    [SerializeField] AudioSource bottleS;
    [SerializeField] AudioSource enemyS;
    [SerializeField] AudioSource enemyS2;

    //walkT
    bool walkT;

    // Start is called before the first frame update
    private void Awake()
    {
        agent.enabled = false;
        TargetPosition = Vector3.zero;

        head.material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        body.material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        walk = false;
        fight = false;
        FightEnemy = false;
        posthis.transform.position = new Vector3(Random.Range(-7, 2), -.7f, Random.Range(-4, 2));
        if (shop.shopOC == false)
        {
            avtavate.constraints = RigidbodyConstraints.FreezeAll;
        }
        walkT = false;
    }
    private void Update()
    {
        if (this.gameObject.transform.position.y < -2)
        {
            posthis.transform.position = new Vector3(Random.Range(-7, 2), 0.7f, Random.Range(-4, 2));
        }
        if (walk == false)
        {
            if (shop.shopOC)
            {
                //stop void
                walk = true;
                avtavate.constraints = RigidbodyConstraints.None | RigidbodyConstraints.FreezeRotation;
                parent = GameObject.Find("beginscene");
                this.gameObject.transform.parent = parent.transform;
                TargetPosition = new Vector3(Random.Range(-7, 2), 0, Random.Range(-7, 2));
                agent.enabled = true;
                agent.destination = TargetPosition;
                walkT = false;
                StartCoroutine(WALK());
            }
        }
        if (shop.shopOC == false)
        {
            agent.enabled = false;
            walkT = false;
            avtavate.constraints = RigidbodyConstraints.FreezeAll;
            walk = false;
            TargetPosition = Vector3.zero;
        }

        if (walk == true)
        {
            FindClosestTarget();
        }
        //arrow
        if (bow)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            if (enemies.Length > 0)
            {
                checkifem = true;
            }
            else
            {
                checkifem = false;
            }

            if (checkifem == true && bowshoot && bow)
            {
                bowshoot = false;
                StartCoroutine(ReloadArrow());
            }

            if (checkifem == true && !bowshoot && bow)
            {
                GameObject[] arrows = GameObject.FindGameObjectsWithTag("Damage");
                if (arrows.Length < 1)
                {
                    GameObject arrow = Instantiate(arrowprhab, bowactive.transform.position, Quaternion.identity);

                    arrow.transform.LookAt(TargetPosition);
                    arrow.GetComponent<Rigidbody>().velocity = arrow.transform.forward * 10f;
                }
            }
        }

        if (TargetPosition != null)
        {
            FightEnemy = true;
        }
        else
        {
            FightEnemy = false;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Changecolor = false;
            StopCoroutine(changecolor());
            head.material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
            body.material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (shop.shopOC && walk == true)
        {
            if (other.gameObject.tag == "Sword" && sword == false && bow == false)
            {
                swordS.Play();
                other.gameObject.SetActive(false);
                swordactive.SetActive(true);
                fight = true;
                sword = true;
            }
            else if (other.gameObject.tag == "bow" && sword == false && bow == false)
            {
                bowS.Play();
                heath += 50;
                Destroy(other.gameObject);
                bow = true;
                bowactive.SetActive(true);
                fight = true;
            }


            if (shield == false)
            {
                if (other.gameObject.tag == "Shield" && shield == false)
                {
                    shieldS.Play();
                    Destroy(other.gameObject);
                    heath += heath + 100;
                    shield = true;
                    shieldactive.SetActive(true);
                    fight = true;
                }

            }
            if (other.gameObject.tag == "bottle")
            {
                bottleS.Play();

                Spawnclick.energy += Random.Range(50, 125);
                heath += 50;
                Destroy(other.gameObject);
            }

            if (other.gameObject.tag == "OutofBound")
            {
                agent.speed = 0;
                walkT = true;
                StartCoroutine(WALK());
                posthis.transform.position = new Vector3(Random.Range(-7, 2), 0.7f, Random.Range(-4, 2));
                agent.speed = 3.5f;
            }
            if (other.gameObject.tag == "Enemy")
            {
                Changecolor = true;
                StartCoroutine(changecolor());
            }
            if (other.gameObject.tag == "campfire")
            {
                posthis.transform.position = new Vector3(Random.Range(-7, 2), 0.7f, Random.Range(-4, 2));
            }
            if (other.gameObject.tag == "player" && !walkT)
            {
                walkT = true;
                StartCoroutine(WALK());
            }
        }
    }
    IEnumerator changecolor()
    {

        GameObject[] enemy = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemy.Length > 0)
        {
            if (Changecolor == true)
            {
                head.material.color = Color.red;
                body.material.color = Color.red;
                heath = heath - 10;
                if (heath < 1)
                {
                    Destroy(this.gameObject);
                }
                yield return new WaitForSeconds(.5f);
                if (fight == true)
                {
                    enemyS.Play();
                }
                head.material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
                body.material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
                yield return new WaitForSeconds(.5f);
                if (Changecolor == true)
                {
                    StartCoroutine(changecolor());
                }
            }
        }
    }
    IEnumerator WALK()
    {
        if (walkT)
        {

            TargetPosition = new Vector3(Random.Range(-7, 6), .7f, Random.Range(-4, 11));
            yield return new WaitForSeconds(3);
            TargetPosition = new Vector3(Random.Range(-7, 6), .7f, Random.Range(-4, 11));
            yield return new WaitForSeconds(1);
            walkT = false;
        }
    }
    IEnumerator ReloadArrow()
    {
        yield return new WaitForSeconds(.4f);
        bowS.Play();
        GameObject arrow = Instantiate(arrowprhab, bowactive.transform.position, Quaternion.identity);

        arrow.transform.LookAt(TargetPosition);
        arrow.GetComponent<Rigidbody>().velocity = arrow.transform.forward * 10f;
        yield return new WaitForSeconds(.4f);
        bowS.Play();
        GameObject arrow2 = Instantiate(arrowprhab, bowactive.transform.position, Quaternion.identity);
        arrow2.transform.LookAt(TargetPosition);
        arrow2.GetComponent<Rigidbody>().velocity = arrow.transform.forward * 10f;

        yield return new WaitForSeconds(.4f);

        yield return new WaitForSeconds(.8f);
        bowshoot = true;
    }
    void FindClosestTarget()
    {
        if (walk)
        {
            // Find the closest enemy
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            float closestDistanceE = Mathf.Infinity;
            GameObject closestEnemy = null;

            foreach (GameObject enemy in enemies)
            {
                float distance = Vector3.Distance(transform.position, enemy.transform.position);
                if (distance < closestDistanceE)
                {
                    closestDistanceE = distance;
                    closestEnemy = enemy;
                }
            }

            if (closestEnemy != null && (bow || sword || shield))
            {
                TargetPosition = closestEnemy.transform.position;
                if (!walkT)
                {
                    walkT = true;
                    StopCoroutine(WALK());
                }
            }
            else
            {
                // Find the closest weapon
                if (trytime == 0)
                {
                    if (!fight)
                    {
                        weaponTag = "Sword";
                        trytime++;
                    }
                }
                else if (trytime == 1)
                {
                    if (!shield)
                    {
                        weaponTag = "Shield";
                        trytime++;
                    }
                }
                else if (trytime == 2)
                {
                    if (!bow)
                    {
                        weaponTag = "bow";
                        trytime++;
                    }
                }
                else if (trytime == 3)
                {
                    weaponTag = "bottle";
                    trytime = 0;
                }

                weapons = GameObject.FindGameObjectsWithTag(weaponTag);
                if (weapons.Length > 0)
                {
                    float closestDistance = Mathf.Infinity;
                    GameObject closestWeapon = null;

                    foreach (GameObject weapon in weapons)
                    {
                        float distance = Vector3.Distance(transform.position, weapon.transform.position);
                        if (distance < closestDistance)
                        {
                            closestDistance = distance;
                            closestWeapon = weapon;
                        }
                    }

                    if (closestWeapon != null)
                    {
                        TargetPosition = closestWeapon.transform.position;
                        if (!walkT)
                        {
                            walkT = true;
                            StopCoroutine(WALK());
                        }
                    }
                }
                else if (weapons.Length == 0)
                {
                    //this error took over 8 hours to fix 
                    NavMeshHit hit;
                    bool isAgentCloseEnough = NavMesh.SamplePosition(TargetPosition, out hit, 1.0f, NavMesh.AllAreas);
                    if (!isAgentCloseEnough)
                    {
                        walk = false;
                        agent.destination = Vector3.zero;
                        agent.enabled = false;

                        avtavate.constraints = RigidbodyConstraints.FreezeRotation;
                        posthis.transform.position = new Vector3(Random.Range(-7, 2), 0.7f, Random.Range(-4, 2));
                        TargetPosition = new Vector3(Random.Range(-7, 2), 0, Random.Range(-4, 2));
                        agent.enabled = true;
                        walk = true;
                    }
                    else
                    {
                        float distanceToTarget = Vector3.Distance(agent.transform.position, TargetPosition);
                        float dis = 0.7f;
                        if (distanceToTarget > dis)
                        {
                            if (walkT == false)
                            {
                                walkT = true;
                                StartCoroutine(WALK());
                            }
                        }
                        else if (shop.shopOC)
                        {
                            avtavate.constraints = RigidbodyConstraints.FreezeRotation;
                            if (walkT == false)
                            {
                                walkT = true;
                                StopCoroutine(WALK());
                            }
                            else
                            if (TargetPosition != Vector3.zero)
                            {
                                TargetPosition = new Vector3(Random.Range(-7, 2), 0, Random.Range(-4, 2));
                                agent.enabled = true;
                                if (agent.isOnNavMesh)
                                {
                                    agent.destination = TargetPosition;
                                }
                                else
                                {
                                    // novalid navmesh
                                    if (walkT == false)
                                    {
                                        walkT = true;
                                        StopCoroutine(WALK());
                                    }
                                }
                            }
                        }
                        {
                            // Vector3.zero
                            if (walkT == false)
                            {
                                walkT = true;
                                StopCoroutine(WALK());
                            }
                        }
                    }
                }
            }


            if (TargetPosition != Vector3.zero)
            {
                if (agent != null && agent.isActiveAndEnabled && agent.isOnNavMesh)
                {
                    agent.destination = TargetPosition;
                }
                else
                {
                    walk = false;
                    agent.destination = Vector3.zero;
                    agent.enabled = false;

                    avtavate.constraints = RigidbodyConstraints.FreezeRotation;
                    posthis.transform.position = new Vector3(Random.Range(-7, 2), 0.7f, Random.Range(-4, 2));
                    TargetPosition = new Vector3(Random.Range(-7, 2), 0, Random.Range(-4, 2));
                    agent.enabled = true;
                    walk = true;
                }
            }
        }
    }
}
