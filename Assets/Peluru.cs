using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peluru : MonoBehaviour
{

    public float BulletSpeed = 10f;
    public float rotationSpeed = 2f;

     Vector3 targetPos;

    private Transform target;
     Quaternion targetRotation;

    Rigidbody targetRb;

    public Vector3 hitForce = new Vector3(0, 4, 20);

    // Start is called before the first frame update
    public void setTarget(GameObject newTarget)
    {
        if (newTarget != null)
        {
            target = newTarget.transform;
            targetPos = target.position; 
        }
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, targetPos, BulletSpeed * Time.deltaTime);
        this.transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Target"))
        {
            targetRb = collision.gameObject.GetComponent<Rigidbody>();
            targetRb.AddForce(transform.TransformDirection(hitForce), ForceMode.Impulse);
        }

        Destroy(gameObject);
    }

}

