using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvenScript : MonoBehaviour
{
    [SerializeField] Transform InOvenPos;
    [SerializeField] Transform TakeOutPos;

    [SerializeField] bool isMixer;

    private BowlScript bowlInMix;
    private BakingThings TrayInOven;
    private GrabObject grab;

    private bool isInOven;

    private void Start()
    {
        isInOven = false;
    }

    private void Update()
    {
        if (isInOven)
        {
            if (isMixer)
            {
                if (TrayInOven == null)
                {
                    bowlInMix.IsMixing(Time.deltaTime);
                }
                

            }
            else
            {
                TrayInOven.IsBaking(Time.deltaTime);
            }
            
        }
    }

    public void PutInOven(BowlScript thingInOven, GrabObject grab)
    {
        if (this.bowlInMix == null)
        {
            isInOven = true;
            this.bowlInMix = thingInOven;
            this.grab = grab;
        }
        
    }

    public void PutInOven(BakingThings thingInOven, GrabObject grab)
    {
        if (TrayInOven == null)
        {
            isInOven = true;
            TrayInOven = thingInOven;
            this.grab = grab;
        }

    }

    public Transform GetOvenPos()
    {
        return InOvenPos;
    }

    public Transform GetOutPos()
    {
        return TakeOutPos;
    }

    public void TakeOutOven()
    {
        
        bowlInMix = null;
        grab.ResetInOven();
        isInOven = false;
        TrayInOven = null;
        grab = null;

    }

    public bool IsAMixer()
    {
        return isMixer;
    }


}
