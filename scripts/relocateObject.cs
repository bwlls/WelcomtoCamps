using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class relocateObject : MonoBehaviour
{
    [SerializeField] GameObject thisss;
    void Start()
    {
     StartCoroutine (respawn());
    }

    IEnumerator respawn()
    {
        thisss.transform.position = new Vector3(Random.Range(-7, 6), 0, Random.Range(-4, 11));
    yield return new WaitForSeconds(30);
          StartCoroutine(respawn());
    }
}
