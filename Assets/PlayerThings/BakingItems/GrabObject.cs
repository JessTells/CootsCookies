using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabObject : MonoBehaviour
{

    private Transform objectHold;

    private bool inOven;
    private bool nearAppliance;

    private GameObject appliance;
    private Transform inOvenPos;
    private Transform outOvenPos;

    private void Start()
    {
        inOven = false;
        nearAppliance = false;
        
    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Near appliance");
        appliance = other.gameObject;
        nearAppliance = true;
    }

    public void Grab(Transform objectHold)
    {
        this.objectHold = objectHold;
        gameObject.GetComponent<BoxCollider>().enabled = false;
        gameObject.GetComponent<Rigidbody>().useGravity = false;
    }

    public void Drop()
    {
        objectHold = null;
        gameObject.GetComponent<BoxCollider>().enabled = true;
        gameObject.GetComponent<Rigidbody>().useGravity = true;

    }

    // this is really shit code it does not represent me as a person
    public void AddIngredient()
    {
        if (nearAppliance)
        {
            if (appliance.CompareTag("BakingThing"))
            {
                if (gameObject.CompareTag("Bowl"))
                {
                    appliance.GetComponent<BakingThings>().AddBowl(gameObject.GetComponent<BowlScript>());
                    Destroy(gameObject);
                }
                
                
            }else if (appliance.CompareTag("Bowl"))
            {
                appliance.GetComponent<BowlScript>().AddIngredient(gameObject.GetComponent<Ingredient>());
                gameObject.GetComponent<Ingredient>().PutInBowl();
            }
            else if (appliance.CompareTag("Oven"))
            {
                if (gameObject.CompareTag("Bowl"))
                {
                    if (appliance.GetComponent<OvenScript>().IsAMixer())
                    {
                        
                        Debug.Log("OVEN");
                        appliance.GetComponent<OvenScript>().PutInOven(gameObject.GetComponent<BowlScript>(), gameObject.GetComponent<GrabObject>());
                        inOvenPos = appliance.GetComponent<OvenScript>().GetOvenPos();
                        outOvenPos = appliance.GetComponent<OvenScript>().GetOutPos();
                        inOven = true;
                    }
                    

                }else if (gameObject.CompareTag("BakingThing"))
                {
                    if (!appliance.GetComponent<OvenScript>().IsAMixer())
                    {

                        Debug.Log("THING");
                        appliance.GetComponent<OvenScript>().PutInOven(gameObject.GetComponent<BakingThings>(), gameObject.GetComponent<GrabObject>());
                        inOvenPos = appliance.GetComponent<OvenScript>().GetOvenPos();
                        outOvenPos = appliance.GetComponent<OvenScript>().GetOutPos();
                        inOven = true;
                    }
                    
                }
                
            }else if (appliance.CompareTag("Ingredient") && !gameObject.CompareTag("Ingredient"))
            {
                if (appliance != null)
                {
                    appliance.GetComponent<Ingredient>().Slice();
                }
                
                
            }
            
        }
        
    }

    private void Update()
    {
        if(objectHold != null && !inOven)
        {
            Vector3 newPosition = Vector3.Lerp(transform.position, objectHold.position, Time.deltaTime * 100f);
            transform.rotation = Quaternion.Euler(-90f, objectHold.transform.rotation.eulerAngles.y, objectHold.transform.eulerAngles.z);
            gameObject.GetComponent<Rigidbody>().MovePosition(newPosition);
            
            
        }else if (inOven)
        {
            
            gameObject.transform.position = inOvenPos.position;
        }

        
    }

    public void ResetInOven()
    {
        gameObject.transform.position = outOvenPos.position;
        gameObject.GetComponent<BoxCollider>().enabled = true;
        gameObject.GetComponent<Rigidbody>().useGravity = true;
        inOven = false;
    }
}
