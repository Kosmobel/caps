using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chip_control : MonoBehaviour
{
    private Rigidbody rb;
    private bool hasTouchedTable = false;

    public float dropChance = 0.5f;
    private float impulseDirection;
    private bool applyAtNormal;

    public Vector3 forceDirection; 
    public float forceMagnitude = 0.55f; 
    public float timeToChangeCenter = 0.78f;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        
        if (!hasTouchedTable && collision.gameObject.CompareTag("Table"))
        {
            hasTouchedTable = true;



            Invoke("ChangeCenterOfMass", timeToChangeCenter);
            

            //rb.constraints = RigidbodyConstraints.FreezeAll;
        }
    }


    void ChangeCenterOfMass()
    {
        applyAtNormal = Random.Range(0f, 1f) > dropChance;  //�������� �������/����������� � �����
        Physics.IgnoreLayerCollision(3, 3, true);           //�������� ��������

        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;                                   
        Vector3 chipAtRib = new Vector3(45, rb.rotation.eulerAngles.y, -90); // ���� �������� �� ���� X, Y � Z
        transform.rotation = Quaternion.Euler(chipAtRib);                    //������ ����� �� �����, ��� ����� ����


        Vector3 localNormal = transform.right;
        Vector3 worldNormal = transform.TransformDirection(localNormal);     //������������ ������� � �����


        Vector3 localCenterOfMass = rb.centerOfMass;
        Vector3 oppositeLocalPoint = -localCenterOfMass;                     //������� ������������ ��������������� ����� ��� ������ ����
        Vector3 forcePoint = transform.TransformPoint(oppositeLocalPoint);   //��������� �� ��������� ���������

        Vector3 forceDirection = applyAtNormal ? worldNormal : -worldNormal;
        rb.AddForceAtPosition(forceDirection * forceMagnitude, forcePoint, ForceMode.Force);


        rb.centerOfMass = Vector3.zero;

    }

    //����� ��� ��������� ������� � �����
    public void ResetChipState()
    {
        hasTouchedTable = false;
        Physics.IgnoreLayerCollision(3, 3, false);
    }

}
