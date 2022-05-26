using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    [SerializeField] ParticleSystem exploEffect; 

    [SerializeField] private float speed = 50f;
    [SerializeField] private float timeToDestroy = 3f;

    public bool isExploActive; 
    public Vector3 target;                               //will set this at shooting script with raycast

   



    // Start is called before the first frame update
    void Start()
    {
        
        Destroy(gameObject, timeToDestroy);                             //destroy bullets after 3secs
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed* Time.deltaTime);                 //move bullets from starting point to target with speed

       
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "obstacles")              //if collide with obstacles destroy both obstacle and game object
        Destroy(collision.gameObject);

        Destroy(gameObject);
    }


   

    private void PlayEffect()               //Particle efects for explosion
    {
        
        
            ParticleSystem instance = Instantiate(exploEffect,transform.position, Quaternion.identity);         
            Destroy(instance.gameObject,instance.main.duration + instance.main.startLifetime.constantMax);
        
    }

    private void OnDestroy()             //Play effect if isExploActive true 
    {
        if(isExploActive)
        PlayEffect();
    }
}
