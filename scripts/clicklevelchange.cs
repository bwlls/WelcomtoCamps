using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class clicklevelchange : MonoBehaviour
{
    [SerializeField] AudioSource aud;
    public static float hardness = 0;
    [SerializeField] string scene;
    private void OnMouseUp()
    {
        aud.Play();
        //insane
        if (this.gameObject.name == "insane1" || this.gameObject.name == "insane2")
        {
            harnesslevel.difficultySet = 4f;
            hardness = 4;
            //hard
        } else
            if (this.gameObject.name == "hard1" || this.gameObject.name == "hard2")
            {
            harnesslevel.difficultySet = 3f;
        //normal
        }else if (this.gameObject.name == "normal1")
        {
            harnesslevel.difficultySet = 2f;
        //creative
        }else if (this.gameObject.name == "creative1")
        {
            harnesslevel.difficultySet = 1f;
        }

        SceneManager.LoadScene(scene);
    }
}
