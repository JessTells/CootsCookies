using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoldUseObjects : MonoBehaviour
{
    [SerializeField] Image canGrab;
    [SerializeField] Image noGrab;

    public GameObject playerRef;
    public Transform playerCameraTransform;
    public LayerMask pickUpLayerMask;

    private GrabObject GrabObject;
    private OvenScript OvenScript;

    private bool canSLice;
    private float sliceDelay;

    


    private void Start()
    {
        GrabObject = null;
        canSLice = true;
        sliceDelay = 0;
        
    }
    // THIS CODE DOES NOT REPRESENT ME AS A PERSON
    // Update is called once per frame
    void Update()
    {
        
        float pickUpDistance = 0.25f;
        if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit raycastHit, pickUpDistance, pickUpLayerMask))
        {
            canGrab.enabled = true;
            noGrab.enabled = false;

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (raycastHit.transform.TryGetComponent(out GrabObject))
                {
                    GrabObject.Grab(playerRef.GetComponent<ThirdPersonMovement>().GetObjHoldTransform());

                    Debug.Log(GrabObject);
                }
                else if (raycastHit.transform.TryGetComponent(out OvenScript))
                {
                    Debug.Log("use oven");
                    OvenScript.TakeOutOven();

                }else if (raycastHit.transform.TryGetComponent(out SinkScript sink))
                {
                    sink.resetDishes();
                }
            }






        }
        else
        {
            canGrab.enabled = false;
            noGrab.enabled =  true;
        }

        if (Input.GetKeyUp(KeyCode.Mouse0) && GrabObject != null)
        {
            GrabObject.Drop();
        }

        if (Input.GetKeyDown(KeyCode.Mouse1) && GrabObject != null)
        {
            if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit raycastHit2, pickUpDistance, pickUpLayerMask))
            {
                if (raycastHit.transform.TryGetComponent(out Plate plate))
                {
                    if (GrabObject.CompareTag("BakingThing"))
                    {
                        if (!GrabObject.GetComponent<BakingThings>().isEmpty())
                        {
                            Debug.Log("plate");

                            plate.nextLevel();
                        }
                        else
                        {
                            Debug.Log("palte is empyuy");
                        }
                    }
                    
                }
            }
                if (!gameObject.CompareTag("Knife"))
            {
                Debug.Log("Mouse down");
                GrabObject.AddIngredient();
            }
            else
            {
                if (canSLice)
                {
                    GrabObject.AddIngredient();
                    canSLice = false;
                }
                else
                {
                    
                    if (sliceDelay == 3)
                    {
                        canSLice = true;
                        sliceDelay = 0;
                    }
                    else
                    {
                        sliceDelay += Time.deltaTime;
                    }
                }
            }
            
        }
    }
}
