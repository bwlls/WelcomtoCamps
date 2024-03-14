using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyrandomcolor : MonoBehaviour
{
    [SerializeField] Renderer head;
    [SerializeField] Renderer body;
    [SerializeField] int speed = 2;
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + 8, this.gameObject.transform.position.z);
        head.material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        body.material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        StartCoroutine(delete());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }
    IEnumerator delete()
    {
        yield return new WaitForSeconds(4);
        Destroy(this.gameObject);
    }
}
