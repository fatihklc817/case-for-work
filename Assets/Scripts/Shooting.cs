using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{

    
    [SerializeField] private GameObject bulletPrefab;        
    [SerializeField] private Transform bulletSpawnPoint;   
    [SerializeField] private Transform bulletParent;                     //to instantiate bullets in the Parent
    [SerializeField] float baseFiringRate = 0.5f;     

    public GameObject bullett;                                             //to access bulletControlller's public variables
    BulletController bullettController;


    private Transform cameraTransform;

    public bool isFiring;
    private float lastShootTime = 0f;                                                    //to add fireRate 
    public float counter = 0f;                                                           // to count bullets




     //UI buttons bool variables

    bool isActiveBigBullet = false;                                 
    bool isActiveRedBullet = false;
    public bool isActiveExplosiveBullet = false;

    private bool isShotgun=false;

    
    



    // Start is called before the first frame update
    void Start()
    {
        bullettController = bullett.GetComponent<BulletController>();
        cameraTransform = Camera.main.transform;
        
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > lastShootTime + baseFiringRate)                              //adding fire rate
        {
            fire();
            lastShootTime = Time.time;
            

        }

       
    }

    void fire()
    {
        if (isFiring)                   //if isFring true and is shotgun true, make randomly 4 to 10 bullets and take their random positions by GetRandomDirection() method
        {
            if (isShotgun)
            {
                for (int i = 0; i < Random.Range(4f, 10f); i++)
                {


                    RaycastHit hit;
                    GameObject bullet = GameObject.Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity, bulletParent);
                    if (isActiveRedBullet)                                          //change the color if it s active (change its value by calling the method from ui)
                    {
                        Renderer render = bullet.GetComponent<Renderer>();
                        render.material.color = Color.red;
                    }

                    counter++;       


                    BulletController bulletController = bullet.GetComponent<BulletController>();
                    if (Physics.Raycast(cameraTransform.position, GetRandomDirection(), out hit, Mathf.Infinity))           //with raycast get the target position and set the target for bulletController Script
                    {

                        bulletController.target = hit.point;


                    }
                    else                                                                                                       // if it doesnt hit anything get the position of 100f forward
                    {

                        bulletController.target = cameraTransform.position + cameraTransform.forward * 100f; 

                    }
                }
            }
            else
            {
                                                                                                                                            //if isShotgun is false, Machine gun settings
                    RaycastHit hit;
                    GameObject bullet = GameObject.Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity, bulletParent);
                    if (isActiveRedBullet)
                    {
                        Renderer render = bullet.GetComponent<Renderer>();
                        render.material.color = Color.red;
                    }

                    counter++;


                    BulletController bulletController = bullet.GetComponent<BulletController>();
                    if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, Mathf.Infinity))
                    {

                        bulletController.target = hit.point;


                    }
                    else
                    {

                        bulletController.target = cameraTransform.position + cameraTransform.forward * 100f;

                    }
                
            }
        }
        else if(!isFiring){                   //reset counter
            counter = 0;
        }
    }



    public void BigBullet()                                                                 //Enable Big bullets by UI buttons
    {
        
        if (!isActiveBigBullet)
        {
            bulletPrefab.transform.localScale = new Vector3(0.50f, 0.50f, 0.50f);
            isActiveBigBullet = true;
        }
        else if(isActiveBigBullet)
        {
            bulletPrefab.transform.localScale = new Vector3(0.14f, 0.14f, 0.14f);
            isActiveBigBullet=false;
        }
    }


    public void RedBullet()                                                                 // change isActive redBullet bool to true By UI buttons
    {

        if (!isActiveRedBullet)
        {
            
            isActiveRedBullet = true;
        }
        else if (isActiveRedBullet)
        {
            
            isActiveRedBullet = false;
        }
    }

    public void Explosive()                                                 // change isActiveExpolosiveBullet value to true and Synchronize isExploActive bool (which is from bulletController) with local bool by UI buttons
    {
        if (!isActiveExplosiveBullet)
        {

            isActiveExplosiveBullet = true;
           
            bullettController.isExploActive = isActiveExplosiveBullet;
            
        }
        else if (isActiveExplosiveBullet)
        {

            isActiveExplosiveBullet = false;
            bullettController.isExploActive = isActiveExplosiveBullet;

        }
    }


    public void MakeShotgun()                                                    //Change isShotgun bool value by UI buttons
    {

        if (!isShotgun)
        {

            isShotgun= true;
        }
        else if (isShotgun)
        {

            isShotgun= false;
        }
    }




                                                                                                //random direction for shotgun type
    private Vector3 GetRandomDirection()
    {
        Vector3 targetPos = cameraTransform.position + cameraTransform.forward * 100f;
        targetPos = new Vector3(
            targetPos.x + Random.Range(-5f, 5f),
            targetPos.y + Random.Range(-5f, 5f),
            targetPos.z + Random.Range(-5f, 5f)
            );
        Vector3 direction = targetPos - cameraTransform.position;
        return direction.normalized;
    }

}
