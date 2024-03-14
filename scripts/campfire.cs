using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class campfire : MonoBehaviour
{
   public static  float campemission = 1;
    public static ParticleSystem Campfire;
    // Start is called before the first frame update
    void Start()
    {
        campemission = 1;
        this.gameObject.transform.position = new Vector3(0, 0.2f, 0);
        Campfire = GetComponent<ParticleSystem>();
        Campfire.Stop();
        var particleDistance = Campfire.emission;
        particleDistance.rateOverTime = 1;
        Campfire.Play();
    }
    private void Update()
    {
    }
}
