using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class add_force : MonoBehaviour
{
    Rigidbody rb;
    public float m_Thrust = 20f;
    public float rotationStrength = 5f;
    public float dropChance = 0.5f;
    private float impulseDirection;
    public Vector3 forceDirection;
    public float forceMagnitude = 0.55f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            rb.AddForce(transform.up * m_Thrust, ForceMode.Impulse);

            Vector3 randomRotation = new Vector3(
                Random.Range(-rotationStrength, rotationStrength),
                Random.Range(-rotationStrength, rotationStrength),
                Random.Range(-rotationStrength, rotationStrength)
            );

            rb.AddTorque(randomRotation, ForceMode.Acceleration);

        }
        if (Input.GetKeyDown(KeyCode.R)) {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;                                           //ставим четко НА РЕБРО, ГДЕ ЦЕНТР МАСС
            Vector3 chipAtRib = new Vector3(45, rb.rotation.eulerAngles.y, -90); // Углы поворота по осям X, Y и Z
            transform.rotation = Quaternion.Euler(chipAtRib);

            //Invoke("applyImpulse_debug", 1.0f);
        }
    }
    private void applyImpulse_debug() {

        impulseDirection = Random.Range(0f, 1f) > dropChance ? 1f : -1f;
        forceDirection = new Vector3(0f, impulseDirection, 0f);


        


        Vector3 forceApplicationPoint = rb.position + new Vector3(-0.4f, rb.rotation.eulerAngles.y + 90, -0.4f);
        


        // Применяем импульс к фишке
        rb.AddForceAtPosition(forceDirection.normalized * forceMagnitude, forceApplicationPoint, ForceMode.Impulse);
        //rb.ApplyForceAtPoint(forcePoint, worldNormal, forceMagnitude, applyToNormal);


        //возвращаем центр масс в центр
        rb.centerOfMass = Vector3.zero;
    }
}
