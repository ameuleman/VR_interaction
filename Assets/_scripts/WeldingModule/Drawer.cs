using UnityEngine;
using UnityEngine.Events;
using Valve.VR.InteractionSystem;

/// <summary>
/// Drawer is the base class representing parts that needs to slide.
/// RemovableDrawer and NewPart are its children.
/// </summary>
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public abstract class Drawer : MonoBehaviour {

    [Tooltip("The object that the user will actually move using controllers. " +
    "It must be attached to this Rigidbody through a SpringJoint in order to drive it. " +
    "It is advised to add a SpringHandle component to make sure everything is correctly handled. " +
    "It must have a Throwable, BoxCollider (that can be a trigger) " +
    "and an InteractableHoverEvents component. " +
    "This object can have no MeshRenderer so that the user uses a fixed fake handle.")]
    public GameObject Handle;

    [Tooltip("Locker to know if the drawer can be removed or put")]
    public Locker Locker;

    /// <summary>
    /// To know if the drawer has fallen. In that case, 
    /// the drawer cannot be controlled anymore.
    /// </summary>
    protected bool _hasFallen = false;

    /// <summary>
    /// To know if the drawer is in a proper position.
    /// </summary>
    protected bool _wellPlaced = false;

    /// <summary>
    /// To store the event of the handle to get it back when unlockControls() is called.
    /// </summary>
    protected UnityEvent previousEvent;

    /// <summary>
    /// Prevent the user from controlling the drawer.
    /// Usefull to avoid problems with iTween actions.
    /// </summary>
    public void lockControl()
    {
        // Disable events
        Handle.GetComponent<InteractableHoverEvents>().onHandHoverBegin = new UnityEvent();

        // Disable Controls by disabling the collider 
        Handle.GetComponent<Throwable>().enabled = false;
        Handle.GetComponent<VelocityEstimator>().enabled = false;
        Handle.GetComponent<SpringJoint>().spring = 0;
        Handle.GetComponent<Interactable>().enabled = false;
        Handle.GetComponent<BoxCollider>().enabled = false;

        // Prevent RigidBody from messing with iTween actions
        Handle.GetComponent<Rigidbody>().useGravity = false;
        Handle.GetComponent<Rigidbody>().isKinematic = true;
    }

    /// <summary>
    /// Give back controls to the user if the drawer hasn't fallen.
    /// </summary>
    public void unlockControl()
    {
        if (!_hasFallen)
        {
            Handle.GetComponent<InteractableHoverEvents>().onHandHoverBegin = previousEvent;

            Handle.GetComponent<Throwable>().enabled = true;
            Handle.GetComponent<VelocityEstimator>().enabled = true;
            Handle.GetComponent<SpringJoint>().spring = 2000;
            Handle.GetComponent<Interactable>().enabled = true;
            Handle.GetComponent<BoxCollider>().enabled = true;
            Handle.GetComponent<Rigidbody>().useGravity = true;
            Handle.GetComponent<Rigidbody>().isKinematic = false;
        }
    }

    /// <summary>
    /// Retrieve constraints, this grounds the drawer.
    /// </summary>
    protected void unlockMovements()
    {
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
    }

    /// <summary>
    /// This function must be called when the drawer is no longer held by another object.
    /// This prevents the drawer from sliding forever by grounding it.
    /// </summary>
    protected void fall()
    {
        //Prevent the user from controlling it
        lockControl();
        Handle.GetComponent<SpringJoint>().breakForce = 0;
        //Ground the drawer
        unlockMovements();

        //Display something to understand what happened
        _hasFallen = true;
        Debug.Log("The drawer has fallen");
    }
}
