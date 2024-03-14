using UnityEngine;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.PlayerLoop;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using System;

public class shop : MonoBehaviour
{
    [SerializeField] GameObject people;
    [SerializeField] GameObject inviseShop;
    [SerializeField] GameObject cam;
    [SerializeField] GameObject invisemaingame;
    [SerializeField] float dif = 0;
    public static bool shopOC = true;
    [SerializeField] TextMeshProUGUI Energies;
    [SerializeField] AudioSource aud;


    private void Start()
    {
        Instantiate(people);
        invisemaingame.SetActive(true);
        inviseShop.SetActive(false);
        shopOC = true;
        cam.transform.rotation = Quaternion.Euler(13, 0, 0);
        RenderSettings.ambientIntensity = 1f;
    }

    private void Update()
    {

        Energies.text = "" + Spawnclick.energy;


        if (Input.GetKeyDown("space"))
            {

            if (shopOC && dif == 0)
            {
                aud.Play();
                GameObject[] enemy = GameObject.FindGameObjectsWithTag("Enemy");
                if (enemy.Length == 0)
                {
                    shopOC = false;
                    cam.transform.Rotate((int)10.2538452, (int)162.148712, (int)359.652924);
                    Camera.main.orthographic = true;
                    invisemaingame.SetActive(false);
                    inviseShop.SetActive(true);
                    wlkrandom.walk = true;
                }
            }
                    if (!shopOC && dif == 1)
                    {
                        invisemaingame.SetActive(true);
                        cam.transform.rotation = Quaternion.Euler(13, 0, 0);
                        Camera.main.orthographic = false;
                        wlkrandom.walk = false;
                        shopOC = true;
                        inviseShop.SetActive(false);
            }
        }
    }
}