using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class nextDay : MonoBehaviour
{
    [SerializeField] GameObject[] aniamls;
    public static float day = 0;
    float Leveldif = 0;
    int scale = 1;
    public static int energyfromtent = 0;
    [SerializeField] AudioSource aud;

    [SerializeField] TextMeshProUGUI daycount;
    private void Start()
    {
        day = 0;
        energyfromtent = 0;
        daycount.text = "day:" + day;
    }
    public void Nextdays()
    {
        
        GameObject[] enemy = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemy.Length < 1)
        {
            aud.Play();
            Spawnclick.energy = Spawnclick.energy + energyfromtent;
            day++;
            daycount.text = "day:" + day;
            if (day == 4)
            {
                Leveldif = 1;
                scale = 1;
            }
            if (day == 8)
            {
                Leveldif = 2;
                scale = 1;
            }
            if (day == 10)
            {
                Leveldif = 3;
                scale = 1;
            }
            if (day == 14)
            {
                Leveldif = 4;
                scale = 1;
            }

            if (day == 16)
            {
                Leveldif = 5;
                scale = 1;
            }
            if (Leveldif == 0)
            {
                for (int i = 0; i < scale; i++)
                {
                    Instantiate(aniamls[0]);
                }
                scale++;
                scale++;
            }
            else if (Leveldif == 1)
            {
                for (int i = 0; i < scale; i++)
                {
                    Instantiate(aniamls[1]);
                }
                scale++;
                scale++;
                scale++;
                scale++;
                scale++;
                scale++;
            }
            else if (Leveldif == 2)
            {
                for (int i = 0; i < scale; i++)
                {
                    Instantiate(aniamls[2]);
                    Instantiate(aniamls[1]);
                    Instantiate(aniamls[1]);
                    Instantiate(aniamls[1]);
                    Instantiate(aniamls[1]);
                    Instantiate(aniamls[1]);
                    Instantiate(aniamls[2]);
                    Instantiate(aniamls[2]);
                }
                scale++;
            }
            else if (Leveldif == 3)
            {
                for (int i = 0; i < scale; i++)
                {
                    Instantiate(aniamls[2]);
                    Instantiate(aniamls[2]);
                    Instantiate(aniamls[3]);
                    Instantiate(aniamls[3]);
                    Instantiate(aniamls[4]);
                    Instantiate(aniamls[3]);
                    Instantiate(aniamls[2]);
                    Instantiate(aniamls[1]);
                }
                scale++;
            }
            if (Spawnclick.fire == true)
            {
                campfire.Campfire.Stop();
                campfire.campemission = campfire.campemission - .1f;
                if (campfire.campemission < 0)
                {
                    campfire.campemission = 0;
                    Spawnclick.energy = Spawnclick.energy - 50;
                }
                else if(campfire.campemission < 1.4 && campfire.campemission < 0)
                {
                    Spawnclick.energy = Spawnclick.energy + 50;
                }
                else if (campfire.campemission > 1.4)
                {
                    Spawnclick.energy = Spawnclick.energy + 500;
                }
                var particleDistance = campfire.Campfire.emission;
                particleDistance.rateOverTime = campfire.campemission;
                campfire.Campfire.Play();
            }
            else if (Leveldif == 4)
            {
                for (int i = 0; i < scale; i++)
                {
                    Instantiate(aniamls[3]);
                    Instantiate(aniamls[3]);
                    Instantiate(aniamls[3]);
                    Instantiate(aniamls[3]);
                    Instantiate(aniamls[4]);
                    Instantiate(aniamls[3]);
                    Instantiate(aniamls[4]);
                    Instantiate(aniamls[3]);
                }
                scale++;
            }
            else if (Leveldif == 5)
            {
                for (int i = 0; i < scale; i++)
                {
                    Instantiate(aniamls[2]);
                    Instantiate(aniamls[1]);
                    Instantiate(aniamls[1]);
                    Instantiate(aniamls[1]);
                    Instantiate(aniamls[1]);
                    Instantiate(aniamls[1]);
                    Instantiate(aniamls[2]);
                    Instantiate(aniamls[2]);
                }
                scale++;
                scale++;
                scale++;
            }

            if (Spawnclick.fire == true)
            {
                campfire.Campfire.Stop();
                campfire.campemission = campfire.campemission - .1f;
                if (campfire.campemission < 0)
                {
                    Spawnclick.energy -= 100;
                    campfire.campemission = 0;
                }
                else
                {
                    Spawnclick.energy += 50;
                }
                var particleDistance = campfire.Campfire.emission;
                particleDistance.rateOverTime = campfire.campemission;
                campfire.Campfire.Play();
            }
        }
    }
}
