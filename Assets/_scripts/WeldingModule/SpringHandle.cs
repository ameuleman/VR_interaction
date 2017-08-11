using UnityEngine;
using Valve.VR.InteractionSystem;

/// <summary>
/// SpringHandle is a class that enables an VR-controlled object linked to 
/// another one's RigidBody through a SpringJoint to behave properly.
/// This script enables to break the joint and to rebuild it when necessary.
/// </summary>
[RequireComponent(typeof(Throwable))]
[RequireComponent(typeof(SpringJoint))]
public class SpringHandle : MonoBehaviour {

    [Tooltip("The position where to place the handle when detached from hand")]
    public Transform FakeHandle;

    /// <summary>
    /// Joint properties
    /// </summary>
    private SpringJoint _joint;
    private float Damper = 100;
    private float Spring = 800;
    private float BreakForce = 2;

    /// <summary>
    /// To know if the handle is currently controlled
    /// </summary>
    private bool _notGrabed = true;

    /// <summary>
    /// To rebuild the SpringJoint and connect it to the right RigidBody
    /// </summary>
    private Rigidbody _connectedBody;

    /// <summary>
    /// Initialise the SpringJoint and add listeners when the object is beeing controlled
    /// </summary>
    private void Start () {
        _joint = GetComponent<SpringJoint>();
        _joint.spring = 0;
        _joint.damper = Damper;
        if (!_joint.connectedBody)
        {
            _joint.connectedBody = FakeHandle.GetComponent<Rigidbody>();
        }

        _connectedBody = _joint.connectedBody;

        // listeners when the object is beeing picked up or detached from hand.
        Throwable throwable = GetComponent<Throwable>();
        throwable.onDetachFromHand.AddListener(whenDetach);
        throwable.onPickUp.AddListener(resetSpring);
        
	}

    private void Update()
    {
        // Place the handle at the right position if it isn't grabed
        if (_notGrabed)
        {
            transform.SetPositionAndRotation(FakeHandle.transform.position, FakeHandle.transform.rotation);
        }
    }

    /// <summary>
    /// Called when the user throw the object
    /// Enables to place the handle at the right position without beeing disturbed by the joint
    /// </summary>
    private void whenDetach()
    {
        rebuildJoint();
        _joint.spring = 0;
        _notGrabed = true;
    }

    /// <summary>
    /// Called when the user grab the object
    /// Make the spring joint move the connected body
    /// </summary>
    private void resetSpring()
    {
        _notGrabed = false;

        rebuildJoint();
        _joint.spring = Spring;
        _joint.breakForce = BreakForce;
    }

    /// <summary>
    /// Create another instance of SpringJoint if necessary and set its parameters
    /// </summary>
    private void rebuildJoint()
    {
        if (!_joint)
        {
            transform.SetPositionAndRotation(FakeHandle.transform.position, FakeHandle.transform.rotation);

            _joint = gameObject.AddComponent<SpringJoint>();
            _joint.damper = Damper;
            _joint.connectedBody = _connectedBody;
        }
    }
}