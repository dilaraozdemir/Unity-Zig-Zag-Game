using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// UI elementlerini kullanmak için ekledik.
using UnityEngine.UI;
public class CoroutineTutorial : MonoBehaviour
{
    public float moveSpeed = 3;

    IEnumerator move(Vector3 targetPosition, float moveSpeed)
    {
        while (targetPosition != transform.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime); //moveSpeed*Time.deltaTime = daha smooth bir hareket saðlayacak. 
            yield return null;
        }

    }

    IEnumerator current;
    void Update()
    {
        // Space'e bastýðýmýzda random olarak 
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //current bir ienumarator
            if (current != null)
            {
                    StopCoroutine(current);
            }
            current = move(Random.insideUnitCircle * 3, 3);
            StartCoroutine(current);
        }
    }
}