using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinkScript : MonoBehaviour
{
    [SerializeField] GameObject dishesRef;
    [SerializeField] Transform spawnPoint;

    private GameObject dishesClone;
    private GameObject temp;

    private void Start()
    {
        dishesClone = Instantiate(dishesRef, spawnPoint);

        

    }

    public void resetDishes()
    {
        

        for (var i = spawnPoint.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(spawnPoint.transform.GetChild(i).gameObject);
        }

        dishesClone = Instantiate(dishesRef, spawnPoint);

    }
}
