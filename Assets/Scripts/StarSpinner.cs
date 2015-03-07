using UnityEngine;
using System.Collections;

public class StarSpinner : MonoBehaviour
{
    //Fields
    //Private
    private ProjectileScript projectile;


    // Use this for initialization
    void Start()
    {
        projectile = gameObject.GetComponent<ProjectileScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (projectile.speed > 0)
        {
            Vector3 rot = gameObject.transform.rotation.eulerAngles;
            rot.z += projectile.speed;
            gameObject.transform.rotation = Quaternion.Euler(rot);
        }
    }
}
