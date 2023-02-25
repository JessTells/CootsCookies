using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlScript : MonoBehaviour
{
    [SerializeField] GameObject doughRef;

    private IDictionary<string, float> ingredientList;

    private float bakingTime;
    private float bakingGoal;
    private float mixTime;
    private float mixGoal;
    private bool hasThing;


    private IDictionary<string, float> ChocChipRec = new Dictionary<string, float>
    {
        {"Flour", 2.25f},
        {"Baking Soda", 0.125f},
        {"Salt",  0.125f},
        {"Butter", 2f},
        {"Sugar", 0.75f},
        {"Brown Sugar", 0.75f},
        {"Vanilla Extract", 0.125f},
        {"Egg", 2},
        {"Chocolate", 0.75f}


    };

    private IDictionary<string, float> RaisinCookie = new Dictionary<string, float>
    {
        {"Flour", 1.75f},
        {"Baking Soda", 0.125f},
        {"Salt",  0.125f},
        {"Milk", 1f},
        {"Sugar", 0.75f},
        {"Egg", 2f},
        {"Vanilla Extract", 0.125f},
        {"Raisin", 1.5f}


    };

    private IDictionary<string, float> RedVelvet = new Dictionary<string, float>
    {
        {"Flour", 3f},
        {"Baking Soda", 0.125f},
        {"Egg", 2f},
        {"Salt",  0.75f},
        {"Butter",  0.5f},
        {"Milk", 1.5f},
        {"Sugar", 2.5f},
        {"Brown Sugar", 1f},
        {"Food Coloring", 1f},
        {"Vanilla Extract", 0.125f},
        {"Cheese", 1.5f},
        {"Chocolate", 0.125f}

    };

    public float GetMixTime()
    {
        return mixTime;
    }

    private void Start()
    {
        doughRef.SetActive(false);

        ingredientList = new Dictionary<string, float>();
        mixGoal = 10;
        mixTime = 0;
        bakingGoal = 60;
        bakingTime = 0;
    }

    public void AddIngredient(Ingredient ingredient)
    {
        float holdNum;
        string ingredientName = ingredient.GetIngredientName();

        if (!hasThing)
        {
            hasThing = true;
            doughRef.SetActive(true);
        }
        
        

        if (ingredientList.TryGetValue(ingredientName, out holdNum))
        {
            ingredientList[ingredientName] = holdNum + ingredient.GetIngredientUnit();
        }
        else
        {
            ingredientList.Add(ingredientName, ingredient.GetIngredientUnit());
        }
        

    }

    public void AddBowl(BowlScript otherBowl)
    {
        IDictionary<string, float> otherBowlIngredients = otherBowl.GetIngredientList();

        foreach (KeyValuePair<string, float> otherIngredientInfo in otherBowlIngredients)
        {
            if (ingredientList.TryGetValue(otherIngredientInfo.Key, out float num))
            {
                ingredientList[otherIngredientInfo.Key] = ingredientList[otherIngredientInfo.Key] + num;
            }
            else
            {
                ingredientList.Add(otherIngredientInfo);
            }
        }

    }

    public IDictionary<string, float> GetIngredientList()
    {
        return ingredientList;
    }

    public void IsMixing(float time)
    {
        mixTime += time;
    }

    public void IsBaking(float time)
    {
        bakingTime += time;
        Debug.Log(bakingTime);
    }

    public float FindRecipe()
    {
        float[] compArr = new float[] {1.0f, 1.0f, 1.0f};
        int max;
        foreach (KeyValuePair<string, float> ingredientInfo in ingredientList)
        {
            if (ChocChipRec.TryGetValue(ingredientInfo.Key, out float num1))
            {
                Debug.Log("COMPARISON: " + ingredientInfo.Key + " " + num1);
                compArr[0] /= (ingredientList[ingredientInfo.Key] / num1);
            }

            if (RaisinCookie.TryGetValue(ingredientInfo.Key, out float num2))
            {
                compArr[1] /= (ingredientList[ingredientInfo.Key] / num2);
            }

            if (RedVelvet.TryGetValue(ingredientInfo.Key, out float num3))
            {
                compArr[2] /= (ingredientList[ingredientInfo.Key] / num3);
            }
        }

        max = 100;
        for (int i = 0; i < compArr.Length; ++i)
        {
            if (Mathf.Abs(1 - compArr[i]) < max)
            {
                max = i;
            }
        }

        switch (max)
        {
            case 0:
                Debug.Log("MAX: " + compArr[max]);
                return 10f + (compArr[max] * (bakingTime/bakingGoal) * (mixTime / mixGoal));
            case 1:
                return 20f + (compArr[max] * (bakingTime / bakingGoal) * (mixTime / mixGoal));
            case 2:
                return 30f + (compArr[max] * (bakingTime / bakingGoal) * (mixTime / mixGoal));
            default:
                return 49f;
        }
        
    }


    private void Update()
    {
        foreach(KeyValuePair<string, float> thing in ingredientList)
        {
            Debug.Log("" + thing.Key + " " + thing.Value);
        }
    }
}
