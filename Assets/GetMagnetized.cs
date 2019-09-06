using UnityEngine;

public class GetMagnetized : MonoBehaviour
{
    public string Tag = "Magnet"; //objects to be puuled or repelled
    public float charge = 3; //power the object has
    float absCharge; //magnitude of power
    float ChargeSign; //sign of power
    public float moveSpeed = 5; //speeds to rotate and move
    public float rotSpeed = 5;
    public float RotThreshold = 1; //Thresholds to stop moving
    public float DisThreshold = 1;
    float Distance; //distance between the object and the target 
    float OneOverDistanceSquare; //element used to calculate magnetic power
    Collider target; //the source object that to be pulled or repelled
    Vector3 TargetDirection;
    void OnDrawGizmos()
    {
        if (target) 
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, target.transform.position); //draws the path between the target and the current object
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + transform.forward.normalized * absCharge);  //draws the forward vector of the object
        }
    }

    void Start()
    {
        absCharge = Mathf.Abs(charge); //absolute value of charge
        ChargeSign = Mathf.Sign(charge); //sign of charge too decide repel or attraction
        GetComponent<BoxCollider>().size *= absCharge; //gravitational field is linear to charge value as the size of the collider that creates the field
    }

    private void OnTriggerStay(Collider other) 
    {
        if (other.gameObject.tag == Tag)//when a target object is in collision with the charged object
        {
            Distance = Vector3.Distance(transform.position, other.gameObject.transform.position); //distance between objects
            target = other; //target is used for moving and rotation methods as a global variable 
            if (Distance != 0) //if the objects are not in the same spot
            {
                OneOverDistanceSquare = 1 / (Distance * Distance); //global variable to calculate attraction power
                TargetDirection = Vector3.Scale((target.transform.position - transform.position), new Vector3(1, 0, 1)).normalized; //the direction when the current object looks at the target object
                Rotate();
                if (Distance > DisThreshold)
                {
                    Move();
                }
            }
        }
    }

    private void Move() //object is moved by the charge value times target direction
    {
        transform.position = Vector3.Lerp(transform.position, transform.position + TargetDirection * charge, moveSpeed * OneOverDistanceSquare * Time.deltaTime);
    }

    void Rotate() //object is rotated around the y axis to look at or having the target in the back according to the object's charge sign
    {
        Vector3 forward = Vector3.Scale(transform.forward, new Vector3(1, 0, 1)); //forward vector of current object
        Vector3 forwardUp = TargetDirection; //target rotation 
        Quaternion newRotation = Quaternion.FromToRotation(forward, forwardUp * ChargeSign); //rotation calculated from current forward direction to target object's direction
        if (newRotation.eulerAngles.magnitude > RotThreshold) //If the needed rotation bigger than the threshold object rotates
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, transform.rotation * newRotation, rotSpeed * OneOverDistanceSquare * absCharge * Time.deltaTime); //rotation done from current to target rotation
        }
    }
}