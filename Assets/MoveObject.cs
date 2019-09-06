using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public int Speed = 44;

    void Update()
    {
        MoveWithArrows();
    }

    void Translate(float x, float y, float z) //point transformation
    {
        this.transform.position += new Vector3(x, y, z);
    }

    private void MoveWithArrows()//point transformation
    {
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            Translate(0, 0, -Speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            Translate(0, 0, Speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            Translate(Speed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            Translate(-Speed * Time.deltaTime, 0, 0);
        }
    }
}
