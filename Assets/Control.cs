using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] GameObject peluru;

    public List<GameObject> targetlist = new List<GameObject>();

    public GameObject target;

    public int nolist = 0;


    public float Sensitivity;

    public bool invertY;

    public float speed = 3f;
    Vector3 offset;

    void Start()
    {
        offset = new Vector3(0, 0, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        CamMove();

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        this.transform.Translate(new Vector3(h, 0, v) * Time.deltaTime * speed);


        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject NewPeluru = Instantiate(peluru, this.transform.position + offset, Quaternion.identity);

            Peluru script = NewPeluru.GetComponent<Peluru>();

            if (script != null && target != null)
            {
                script.setTarget(target);
            }
            Debug.Log("Bullet Instatiated");
        }

        if (targetlist.Count > 0 && targetlist != null)
        {
            Vector3 targetpos = target.transform.position - this.transform.position;
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            nolist++;
            if (nolist > targetlist.Count)
            {
                nolist = 0;
            }
            pindahtarget();
        }
    }

    public void CamMove()
    {
        float mouseX = Input.GetAxis("Mouse X") * Sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * Sensitivity * (invertY ? 1 : -1);

        this.transform.Rotate(Vector3.up, mouseX, Space.World);
        this.transform.Rotate(Vector3.right, mouseY, Space.World);
    }

    public void pindahtarget()
    {
        if (targetlist.Count > 0)
        {
            target = targetlist[nolist];
        }
        else
        {
            target = null;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Target"))
        {
            targetlist.Add(other.gameObject);
            pindahtarget();

        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Target"))
        {
            nolist = 0;
            targetlist.Remove(other.gameObject);
            pindahtarget();
        }
    }
}

