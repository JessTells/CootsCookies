using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BakingThings : MonoBehaviour
{
    [SerializeField] GameObject chocCookie;
    [SerializeField] GameObject raisinCookie;
    [SerializeField] GameObject redVelCookie;
    [SerializeField] GameObject badCookie;

    [SerializeField] int whichCookie;

    private IDictionary<string, float> ingredientList;

    private float bakingTime;
    private float bakingGoal;
    private float mixTime;
    private bool cookieChange;

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

    private void Start()
    {
        chocCookie.SetActive(false);
        raisinCookie.SetActive(false);
        redVelCookie.SetActive(false);
        badCookie.SetActive(false);
        ingredientList = new Dictionary<string, float>();

        bakingGoal = 60;
        bakingTime = 0;
        cookieChange = false;
    }

    

    public void AddBowl(BowlScript otherBowl)
    {
        Debug.Log("Bowl Added");
        IDictionary<string, float> otherBowlIngredients = otherBowl.GetIngredientList();
        mixTime = otherBowl.GetMixTime();

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

        switch (whichCookie)
        {
            case 0:
                chocCookie.SetActive(true);
                break;
            case 1:
                raisinCookie.SetActive(true);
                break;
            case 2:
                redVelCookie.SetActive(true);
                break;
            case 3:
                badCookie.SetActive(true);
                break;
            default:
                badCookie.SetActive(true);
                break;
        }

    }

    public void IsBaking(float time)
    {
        bakingTime += time;
        
    }

    private void ChangeCookie(int cookieNum)
    {
        Debug.Log("Cookie num " + cookieNum);
        switch (cookieNum)
        {
            case 1:
                chocCookie.SetActive(true);
                break;
            case 2:
                raisinCookie.SetActive(true);
                break;
            case 3:
                redVelCookie.SetActive(true);
                break;
            case 4:
                badCookie.SetActive(true);
                break;
            default:
                badCookie.SetActive(true);
                break;
        }
    }


    // There was supposed to be grading but I never got it to work

    public float FindRecipe()
    {
        float[] compArr = new float[] { 1.0f, 1.0f, 1.0f };
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
            Debug.Log(max);
            if (Mathf.Abs(1 - compArr[i]) < max)
            {
                max = i;
            }
        }

        switch (max)
        {
            case 0:
                Debug.Log("MAX: " + compArr[max]);
                return 10f + (compArr[max]);
            case 1:
                return 20f + (compArr[max] );
            case 2:
                return 30f + (compArr[max] );
            default:
                return 49f;
        }

    }

    public bool isEmpty()
    {
        return (ingredientList.Count == 0);
    }
}
