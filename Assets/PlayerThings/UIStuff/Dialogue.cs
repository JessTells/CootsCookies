using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Dialogue : MonoBehaviour
{
    public GameObject gameObjectRef;

    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;
    public float animIndexChange;

    public string levelToOpen;


    private int index;

    private Animator animatorRef;

    // Start is called before the first frame update
    void Start()
    {
        animatorRef = gameObjectRef.GetComponent<Animator>();
        textComponent.text = string.Empty;
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach(char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if (index == animIndexChange)
        {
            animatorRef.SetTrigger("isEat");
        }
        if(index < lines.Length - 1)
        {
            ++index;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
            SceneManager.LoadScene(levelToOpen);
        }
    }

}
