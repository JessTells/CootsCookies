using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QTBaking : MonoBehaviour
{
    private float multiplier;

    // Start is called before the first frame update
    void Start()
    {
        multiplier = 1;


    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < -2f)
        {
            multiplier = -multiplier;
        }
        else if (transform.position.x > 3.5f)
        {
            multiplier = -multiplier;
        }

        Vector3 newPosition = Vector3.Lerp(transform.position, new Vector3(transform.position.x + 0.1f * multiplier, transform.position.y, transform.position.z), Time.deltaTime * 100f);
        gameObject.GetComponent<Rigidbody>().MovePosition(newPosition);
    }
}
