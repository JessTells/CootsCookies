using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GradeFood : MonoBehaviour
{
    [SerializeField] int orderNum;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BakingThing"))
        {
            float grade = other.GetComponent<BakingThings>().FindRecipe();
            

            if (orderNum == Mathf.Floor(grade))
            {
                Debug.Log("Good");
            }

            if (Mathf.Abs((grade % 10) - 1) <=  0.25f)
            {
                Debug.Log("Edible");
            }

            Debug.Log("Grade:" + grade);
            
        }
    }
}
