using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;

public class rotation : MonoBehaviour
{
    [SerializeField] float upspeed = 0;
    [SerializeField] Transform thisss;
    private void Start()
    {
        StartCoroutine(delete());
       transform.rotation = Quaternion.Euler(0, 0, 90);
        this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x - 15.41001f, this.gameObject.transform.position.x + 4.464163f - 8, this.gameObject.transform.position.x + 1.942468f - 20.72f);
    }

    private void Update()
    {
        transform.position += this.gameObject.transform.position * upspeed * Time.deltaTime;
        transform.Rotate(new Vector3(0, 0, transform.rotation.x + 5));
    }
    IEnumerator delete()
    {
        yield return new WaitForSeconds(.5f);
        Destroy(this.gameObject);
    }
}
//-4.464163y 8
//15.41001x
//-1.942468z -20.72 