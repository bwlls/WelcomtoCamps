using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class totorial : MonoBehaviour
{
    [SerializeField] AudioSource aud;
   [SerializeField] GameObject[] nextslide;
    float slide = 1;

    private void Start()
    {
        nextslide[0].SetActive(true);
    }
            public void nextlevel()
    {
        aud.Play();
    if (slide == 0)
        {
            nextslide[0].SetActive(true);
            nextslide[4].SetActive(false);
            slide = 1;
        }
    else if (slide == 1)
        {
            nextslide[1].SetActive(true);
            nextslide[0].SetActive(false);
            slide = 2;
        }
        else if (slide == 2)
        {
            nextslide[2].SetActive(true);
            nextslide[1].SetActive(false);
            slide = 3;
        }
        else if (slide == 3)
        {
            nextslide[3].SetActive(true);
            nextslide[2].SetActive(false);
            slide = 4;
        }
        else if (slide == 4)
        {
            nextslide[4].SetActive(true);
            nextslide[3].SetActive(false);
            slide = 5;
        }
        else if (slide == 5)
        {
            nextslide[5].SetActive(true);
            nextslide[4].SetActive(false);
            slide = 6;
        }
        else if (slide == 6)
        {
            nextslide[6].SetActive(true);
            nextslide[5].SetActive(false);
            slide = 7;
        }
        else if (slide == 7)
        {
            SceneManager.LoadScene("astart");
        }


    }
}
