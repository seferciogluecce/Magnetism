using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetMagnetized : MonoBehaviour
{

    public string Tag = "Magnet";
    public float speed = 5;
    public float rotSpeed = 5;
    public float charge = 3;
    public float threshold = 1; 
    public float thresholdDis = 1; 
     Rigidbody rg;
    Collider target;
    float Distance;
    void OnDrawGizmos()
    {
        //Vector3 forwardUp = other.transform.position - transform.position;
        //Vector3 forward = transform.forward;
        // Draws a blue line from this transform to the target
        if (target) { 
        Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, target.transform.position);
        Gizmos.color = Color.red;

        Gizmos.DrawLine(transform.position, transform.position + transform.forward.normalized *2);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rg = this.GetComponent<Rigidbody>();
        GetComponent<BoxCollider>().size *= Mathf.Abs(charge);// new Vector3(  ); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == Tag)
        {
            Debug.Log("Collided to " + Tag);

            //   transform.Translate(other.gameObject.transform.position,);
            // rg.AddForce((transform.position - other.transform.position ) * speed * Time.deltaTime *  1 / Vector3.Distance(transform.position, other.gameObject.transform.position) * charge);
            //    transform.position = Vector3.Lerp(transform.position, other.gameObject.transform.position, speed * Time.deltaTime * 1 / Vector3.Distance(transform.position, other.gameObject.transform.position));
            //   transform.position = Vector3.MoveTowards(transform.position, other.gameObject.transform.position, speed * Time.deltaTime * 1/Vector3.Distance(transform.position, other.gameObject.transform.position) );
            // transform.LookAt(other.gameObject.transform );
            //  Quaternion target = Quaternion.LookRotation(transform.position - other.transform.position);
            //   Quaternion.Slerp(transform.rotation, other.gameObject.transform.rotation, rotSpeed * Time.deltaTime);
            // Quaternion.Lerp(transform.rotation, target, rotSpeed * Time.deltaTime);

            // Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(lTargetDir), Time.time * rotSpeed);
             Distance = Vector3.Distance(transform.position, other.gameObject.transform.position);
            target = other;
            //Rotate(other);

            if (Distance > thresholdDis)
            {
                Move(other);
            }

        }
    }


    private void Move(Collider other)
    {

         transform.Translate(  Vector3.Scale( ((other.transform.position -transform.position ) * speed * (1 /  (Distance*Distance)) * charge * Time.deltaTime ),new Vector3(1,0,1))   );

    }


    void Rotate(Collider other)
    {
       
            Vector3 forwardUp = Vector3.Scale( other.transform.position - transform.position, new Vector3(1, 0, 1)) * Mathf.Sign(charge);
            Vector3 forward = Vector3.Scale(transform.forward, new Vector3(1, 0, 1));
        Quaternion newRotation = Quaternion.FromToRotation(forward, forwardUp);
        Debug.Log(name + " " + newRotation.eulerAngles);
        Debug.Log(newRotation.eulerAngles.magnitude );
        if (newRotation.eulerAngles.magnitude > 10 || newRotation.eulerAngles.magnitude<350)
        {

            //  transform.rotation = Quaternion.Lerp(transform.rotation, transform.rotation * newRotation, rotSpeed * Time.deltaTime * (1 / (Distance * Distance)));// * charge);
            //transform.Rotate(Quaternion.Lerp(transform.rotation, transform.rotation * newRotation, rotSpeed * Time.deltaTime * (1 / (Distance * Distance))).eulerAngles);
            transform.Rotate(newRotation.eulerAngles*( (newRotation.eulerAngles.magnitude>=180)? -1:1 ) * rotSpeed * Time.deltaTime * (1 / (Distance * Distance)));

        }
        }
}
;////