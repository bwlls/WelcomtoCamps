using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class nodelet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var objects = Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name == "nodeletmus").ToList();
        if (objects.Count > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }
    private void Update()
    {

        if (SceneManager.GetActiveScene().name == "movie")
        {
            Destroy(this.gameObject);
        }
    }
}
