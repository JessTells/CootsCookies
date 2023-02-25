using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{

    [SerializeField] string ingredientName;

    [SerializeField] float cupsUnit;
    
    private Transform spawnRef;

    private void Start()
    {
        spawnRef = GameObject.Find("IngredientSpawnPoint").transform;
    }

    public void PutInBowl()
    {
        Destroy(gameObject);
    }



    public string GetIngredientName()
    {
        return ingredientName;
    }

    public float GetIngredientUnit()
    {
        return cupsUnit;
    }

    public void Slice()
    {
        Debug.Log("Thingy");
        cupsUnit /= 2;
        gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x * 0.9f, gameObject.transform.localScale.y * 0.9f, gameObject.transform.localScale.z * 0.9f);
        Instantiate(gameObject, spawnRef);
        Instantiate(gameObject, spawnRef);
        Destroy(gameObject);
    }


}
