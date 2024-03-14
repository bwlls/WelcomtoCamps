using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class harnesslevel : MonoBehaviour
{
    public static float difficultySet;
    void Start()
    {
        var objects = Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name == "hardness").ToList();
        if (objects.Count > 1)
        {
        Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "astart")
        {
            difficultySet = -1;
        }
    }
}
