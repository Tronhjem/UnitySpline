using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class vectorTest : MonoBehaviour
{

    //public GameObject pos1;
    //public GameObject pos2;

    public Transform[] transformsArray;
  
    
    public GameObject showElement;
    private Vector3 side1;

    private Transform closetsTransform;
    private Transform secondClosetsTransform;
    private Vector3 playerPos;
    private float shortestDistance = Mathf.Infinity;
    private int index = 0;
    private int shortestIndex = 0;

    private Vector3 newPos2;

    void Start()
    {
        closetsTransform = transform;
        secondClosetsTransform = transform;
        playerPos = transform.position;

    }

    void Update()
    {

        shortestDistance = Mathf.Infinity;

        index = 0;
        shortestIndex = 0;

        //foreach (Transform t in transformsArray)
        //{
        //    float distance = Vector3.Distance(transform.position, t.position);

        //    if (distance < shortestDistance)
        //    {

        //        shortestDistance = distance;
        //        closetsTransform = t;
        //        shortestIndex = index;

        //    }
        //    index++;
        //}




        //if (shortestIndex == 0)
        //{
        //    newPos2 = transformsArray[1].position;
        //}
        //else if (shortestIndex == transformsArray.Length - 1)
        //{
        //    newPos2 = transformsArray[shortestIndex - 1].position;
        //}
        //else
        //{
        //    float disOneBelow = Vector3.Distance(transform.position, transformsArray[shortestIndex - 1].position);
        //    float disOneAbove = Vector3.Distance(transform.position, transformsArray[shortestIndex + 1].position);

        //    if (disOneAbove < disOneBelow)
        //    {
        //        newPos2 = transformsArray[shortestIndex + 1].position;

        //    }
        //    else
        //    {
        //        newPos2 = transformsArray[shortestIndex - 1].position;
        //    }


        //}


        //newPos2 = transformsArray[1].position;
        //playerPos = transform.position;

        //side1 = newPos2 - newPos1;
        //playerPos = transform.position;


        Vector3 anotherObject = (transformsArray[1].position - transformsArray[0].position).normalized;

        Vector3 posTofirst = transform.position - transformsArray[0].position;

        Vector3 product = anotherObject * Vector3.Dot(posTofirst, anotherObject) + transformsArray[0].position;


        showElement.transform.position = product;
        Debug.Log(Vector3.Dot(posTofirst, anotherObject), gameObject);

        Debug.DrawLine(new Vector3(0,0,0), anotherObject, Color.white);
        Debug.DrawLine(new Vector3(0,0,0), anotherObject * Vector3.Dot(posTofirst, anotherObject), Color.blue);

        Debug.DrawLine(transform.position, transformsArray[0].position, Color.white);

        Debug.DrawLine(posTofirst, new Vector3(0, 0, 0), Color.red);
        
    }
}
