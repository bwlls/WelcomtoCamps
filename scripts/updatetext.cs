using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class updatetext : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI Textcost;
    void Start()
    {
        Textcost.text = "100                                              100                               125                     click space to go back to game                    \r\n\r\n\r\n\r\n100                                             200                                  100                                 125";
            if (harnesslevel.difficultySet == 4)
        {
        Textcost.text = "100                                             150                               200                      click space to go back to game                     \r\n\r\n\r\n\r\n175                                             400                                  150                                 200";
        }
    }
}
