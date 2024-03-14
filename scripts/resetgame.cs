using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class resetgame : MonoBehaviour
{
    [SerializeField] string map;
    // Start is called before the first frame update
    public void Reset()
    {
    SceneManager.LoadScene(map);
    }
}
