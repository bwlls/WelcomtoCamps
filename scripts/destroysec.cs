using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroysec : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(deletarrow()); 
    }

    IEnumerator deletarrow()
    {
        yield return new WaitForSeconds(3);
        Destroy(this.gameObject);
    }
}
