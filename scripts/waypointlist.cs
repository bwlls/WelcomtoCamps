using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MoveToWaypoints : MonoBehaviour
{
    //movement
    [SerializeField] List<Transform> waypoints;
    [SerializeField] List<Transform> waypoints2;
    private int currentWaypointIndex2 = 0;
    [SerializeField] float speed = 5.0f;
    [SerializeField] float speed2 = 8.0f;
    private int currentWaypointIndex = 0;
    bool walktrue = false;
    bool walktrue2 = false;
    //spawn/color
    [SerializeField] Renderer head;
    [SerializeField] Renderer body;
    [SerializeField] GameObject Prefab;
    [SerializeField] GameObject Prefab2;
    //fade
    [SerializeField] float fadeDuration = 2;
    [SerializeField] Image image;
    [SerializeField] GameObject image2;
    //sound
    [SerializeField] AudioSource audio2;
    [SerializeField] AudioSource audio3;
    float timeaudio = 0;
    private void Start()
    {
        walktrue = false;
        StartCoroutine(waypointdelay());
        if (speed != 6)
        {
            head.material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
            body.material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        }
        if(speed == 10)
        {
            walktrue = true;
        }
    }
    void Update()
    {

        if (walktrue)
        {
            if (waypoints.Count == 0) return;
            Transform targetWaypoint = waypoints[currentWaypointIndex];
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, step);
            if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.002f)
            {
                Instantiate(Prefab, targetWaypoint.position, Quaternion.identity);
                currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Count;
                walktrue = false;
                if (speed == 6)
                {
                    StartCoroutine(audiodelay());
                }
                if (speed == 10)
                {
                    StartCoroutine(fade());
                }
                StartCoroutine(waypointdelay2());
            }
        }
        if (speed != 10)
        {
            if (walktrue2)
            {
                if (waypoints2.Count == 0) return;
                Transform targetWaypoint2 = waypoints2[currentWaypointIndex2];
                float step = speed2 * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, targetWaypoint2.position, step);
                if (Vector3.Distance(transform.position, targetWaypoint2.position) < 0.002f)
                {
                    Instantiate(Prefab2, targetWaypoint2.position, Quaternion.identity);
                    currentWaypointIndex2 = (currentWaypointIndex2 + 1) % waypoints2.Count;
                    walktrue2 = false;
                    StartCoroutine(spawncamp());
                }
            }
        }
    }
    IEnumerator waypointdelay2()
    {
        yield return new WaitForSeconds(5);
        walktrue2 = true;
    }

    IEnumerator waypointdelay()
    {
        yield return new WaitForSeconds(49);
        walktrue = true;
        if (speed == 6)
        {
            StartCoroutine(audiodelay());
        }
    }
    IEnumerator spawncamp()
    {
        yield return new WaitForSeconds(2);

        var allObjects = Resources.FindObjectsOfTypeAll<GameObject>();
        foreach (var obj in allObjects)
        {
            if (obj.tag == "getitem" && !obj.activeInHierarchy)
            {
                obj.SetActive(true);
                
            }
        }
    }
    IEnumerator fade()
    {
        image2.SetActive(true);
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            float A = t / fadeDuration;
            image.color = new Color(0, 0, 0, A);
            yield return null;
        }
        image.color = new Color(0, 0, 0, 1);


        SceneManager.LoadScene("astart");
    }
    IEnumerator audiodelay()
    {
        yield return new WaitForSeconds(timeaudio);
        if(timeaudio == 0)
        {
            audio2.Play();
            timeaudio = 4;
        }else
        if(timeaudio == 4) 
        {
            audio3.Play();
        }
    }    
}