using UnityEngine;

public class CarController : MonoBehaviour
{
    [Header("Wheel Colliders")]
    public WheelCollider frontLeft;
    public WheelCollider frontRight;
    public WheelCollider rearLeft;
    public WheelCollider rearRight;

    [Header("Wheel Transforms (Visual)")]
    public Transform frontLeftTransform;
    public Transform frontRightTransform;
    public Transform rearLeftTransform;
    public Transform rearRightTransform;

    [Header("Car Settings")]
    public float maxMotorTorque = 3000f;
    public float maxSteerAngle = 30f;
    public float brakeTorque = 3000f;

    private float motorInput;
    private float steerInput;
    private float brakeInput;

    void Update()
    {
        // Input
        motorInput = Input.GetAxis("Vertical");
        steerInput = Input.GetAxis("Horizontal");
        brakeInput = Input.GetKey(KeyCode.Space) ? 1f : 0f;

        // Update wheel meshes to match physics
        UpdateWheelVisual(frontLeft, frontLeftTransform);
        UpdateWheelVisual(frontRight, frontRightTransform);
        UpdateWheelVisual(rearLeft, rearLeftTransform);
        UpdateWheelVisual(rearRight, rearRightTransform);
    }

    void FixedUpdate()
    {
        // Apply torque to rear wheels (RWD)
        rearLeft.motorTorque = motorInput * maxMotorTorque;
        rearRight.motorTorque = motorInput * maxMotorTorque;

        // Apply braking
        float currentBrake = brakeInput * brakeTorque;
        rearLeft.brakeTorque = currentBrake;
        rearRight.brakeTorque = currentBrake;
        frontLeft.brakeTorque = currentBrake;
        frontRight.brakeTorque = currentBrake;

        // Apply steering to front wheels
        float currentSteer = steerInput * maxSteerAngle;
        frontLeft.steerAngle = currentSteer;
        frontRight.steerAngle = currentSteer;
    }

    void UpdateWheelVisual(WheelCollider collider, Transform visualWheel)
    {
        Vector3 pos;
        Quaternion rot;
        collider.GetWorldPose(out pos, out rot);
        visualWheel.position = pos;
        visualWheel.rotation = rot;
    }
}
