using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMove : MonoBehaviour
{
    GameObject MovePoint;
    List<GameObject> listPoint = new List<GameObject>();

    public float speed = 3f;
    // Start is called before the first frame update


    void Awake()
    {
        MovePoint = GameObject.Find("MovePoint");
         foreach (Transform child in MovePoint.transform)
        {
            listPoint.Add(child.gameObject);
        }

        foreach (GameObject x in listPoint)
        {
        }
    }

    // Update is called once per frame
    void Update()
    {
         if (listPoint.Count > 0)
        {
            Vector3 targetPos = new Vector3(listPoint[0].transform.position.x,
                                            this.transform.position.y,
                                            listPoint[0].transform.position.z);


            this.transform.position = Vector3.MoveTowards(this.transform.position, targetPos, speed * Time.deltaTime);


            Vector3 direction = targetPos - this.transform.position;
            if (direction != Vector3.zero) 
            {
                this.transform.rotation = Quaternion.LookRotation(direction);
            }

            if (Vector3.Distance(this.transform.position, targetPos) < 0.1f)
        {
            GameObject reached = listPoint[0];
            listPoint.RemoveAt(0);
            listPoint.Add(reached); 
        }
        }
    }
}
