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
        //If the projectile is moving
        if (projectile.speed > 0)
        {
            //Rotates the star based on the speed its moving at
            Vector3 rot = gameObject.transform.rotation.eulerAngles;
            rot.z += projectile.speed;
            gameObject.transform.rotation = Quaternion.Euler(rot);
        }
    }
}
