using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Spawnclick : MonoBehaviour
{
    //sword
    GameObject parent;
    //tent
    [SerializeField] GameObject[] tent;
    public static MeshRenderer tentMeshRen;
    //difvar
    [SerializeField] GameObject Spawn;
    [SerializeField] float dif = 0;
    [SerializeField]float alr = 0;
    public static bool fire = false;
    public static int energy = 300;
    [SerializeField] AudioSource aud;

    [SerializeField] TextMeshProUGUI Texts;

    [SerializeField] GameObject lightning;
    private void Start()
    {
        fire = false;
        if (harnesslevel.difficultySet == 1f)
        {
            energy = 99999999;
        }else
        if (harnesslevel.difficultySet == 2f)
        {
            energy = 800;
        }else
        if (harnesslevel.difficultySet == 3f)
        {
            energy = 500;
        }
        else if(harnesslevel.difficultySet == 4f)
        {
            energy = 400;
        }
        else
        {
            energy = 99999;
        }
    }
    private void OnMouseDown()
    {
      aud.Play();
        //fire

        if (dif == 0 && energy >= 100)
        {
            if (harnesslevel.difficultySet == 4 && alr == 0)
            {
                energy -= 175;
                Instantiate(Spawn);
                alr++;
                Texts.text = "100                                              150                               200                     click space to go back to game                    \r\n\r\n\r\n\r\n100                                             400                                  150                                 200";
            }
            else if (alr == 0)
            {
                energy -= 100;
                Instantiate(Spawn);
                Texts.text = "75                                                100                               125                     click space to go back to game                    \r\n\r\n\r\n\r\n75                                             200                                  100                                  125";
                alr++;
            }
            else
            if (alr > 0 && energy >= 75)
            {
               if (harnesslevel.difficultySet == 4 && energy >= 100)
                {
                    energy -= 100;
                }
                else if (harnesslevel.difficultySet != 4)
                {
                    energy = energy - 75;
                }
                Debug.Log("fd");
                fire = true;
                campfire.Campfire.Stop();
                campfire.campemission = campfire.campemission + .5f;
                var particleDistance = campfire.Campfire.emission;
                particleDistance.rateOverTime = campfire.campemission;
                campfire.Campfire.Play();
            }
        }
        //tent
        else if (dif == 1)
        {
            if (energy >= 200)
            {
                tentMeshRen = GetComponent<MeshRenderer>();
                if (harnesslevel.difficultySet == 4 && energy >= 400)
                {
                    energy -= 400;
                }
                else if (harnesslevel.difficultySet != 4)
                {
                    energy -= 200;
                }
                else
                {
                    return;
                }

                if (alr == 0)
                {
                    nextDay.energyfromtent = 100;
                    tent[0].SetActive(true);
                    alr = 1;
                }
                else if (alr == 1)
                {
                    nextDay.energyfromtent = 350;
                    tent[1].SetActive(true);
                    alr = 2;
                }
                else if (alr == 2)
                {

                    nextDay.energyfromtent = 575;
                    tent[2].SetActive(true);
                    alr = 3;
                }
                else if (alr == 3)
                {

                    nextDay.energyfromtent = 800;
                    tent[3].SetActive(true);
                    alr = 4;

                }
                else if (alr == 4)
                {
                    nextDay.energyfromtent = 1025;
                    tent[4].SetActive(true);
                    alr++;
                    alr = 5;
                }
                else if (alr == 5)
                {
                    nextDay.energyfromtent = 1275;
                    tent[5].SetActive(true);
                    alr++;
                    alr = 6;
                }
                else if (alr == 6)
                {
                    nextDay.energyfromtent = 1325;
                    tent[6].SetActive(true);
                    alr = 7;
                }
                else if (alr == 7)
                {
                    nextDay.energyfromtent = 1575;
                    tent[7].SetActive(true);
                    alr = 8;
                }
                else if (alr == 8)
                {
                    nextDay.energyfromtent = 1800;
                    tent[8].SetActive(true);
                    alr = 9;
                }
                else if (alr == 9)
                {
                    energy += 200;
                    StartCoroutine(cangecolor());
                }
            }
        }
        //human
        else if (dif == 3 && alr == 1 && energy >= 100)
        {
            if (harnesslevel.difficultySet == 4 && energy >= 150)
            {
                energy -= 150;
             }
            else if (harnesslevel.difficultySet != 4)
            {
                energy -= 100;
            }
            Instantiate(Spawn);

        }
        //shield
        else if (dif == 3 && alr == 2 && energy >= 125)
        {
            if (harnesslevel.difficultySet == 4 && energy >= 200)
            {
                energy -= 200;
             }
            else if (harnesslevel.difficultySet != 4)
            {
                energy -= 125;
           }
            Instantiate(Spawn);

        }
        //water
        else if (dif == 3 && alr == 4 && energy >= 75)
        {
            if (harnesslevel.difficultySet == 4 && energy >= 100)
            {
                energy -= 100;
              }
            else if (harnesslevel.difficultySet != 4)
            {
                energy -= 75;
             }
            Instantiate(Spawn);
            //bow
        }
        else if (dif == 3 && alr == 5 && energy >= 125)
        {
            if (harnesslevel.difficultySet == 4 && energy >= 200)
            {
                energy -= 200;
            }
            else if (harnesslevel.difficultySet != 4)
            {
                energy -= 125;
            }
            Instantiate(Spawn);

        }
        //sword
        else if (dif == 3 && alr == 6 && energy >= 100)
        {
            if (harnesslevel.difficultySet == 4 && energy >= 150)
            {
                energy -= 150;
                Instantiate(lightning, new Vector3(15.7f, -4.3f, -1.8f), lightning.transform.rotation);
            }
            else if (harnesslevel.difficultySet != 4)
            {
                energy -= 100;
                Instantiate(lightning, new Vector3(15.7f, -4.3f, -1.8f), lightning.transform.rotation);

            }   
            Instantiate(Spawn);

        }
    }
    IEnumerator cangecolor()
    {
        tentMeshRen.material.color = Color.clear;
        yield return new WaitForSeconds(2);
        tentMeshRen.material.color = Color.green;
    }
}

