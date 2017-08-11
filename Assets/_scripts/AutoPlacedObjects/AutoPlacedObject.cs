using Valve.VR.InteractionSystem;
using UnityEngine;

/// <summary>
/// AutoPlacedObject is the base class for vr-handled objects 
/// that will be placed automatically at a given position. 
/// </summary>
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]

public abstract class AutoPlacedObject : MonoBehaviour {
    [Tooltip("The object that the user will actually move using controllers. " +
    "It must be attached to this Rigidbody through a FixedJoint in order to drive it. " +
    "It must have a Throwable, BoxCollider (that can be trigger) " +
    "and an InteractableHoverEvents component. " +
    "This object must have no MeshRenderer so that it is hidden.")]
    public GameObject handlingHelper;

    /// <summary>
    /// Place smoothly the object at the given transform
    /// The user lose control on it when this function is called
    /// </summary>
    /// <param name="newTransform"></param>
    protected void place(Transform newTransform)
    {
        place(newTransform.position, newTransform.rotation);
    }

    /// <summary>
    /// Place smoothly the object at the given position and rotation
    /// The user lose control on it when this function is called
    /// </summary>
    /// <param name="newPosition"></param>
    /// <param name="newRotation"></param>
    protected void place(Vector3 newPosition, Quaternion newRotation)
    {
        // Prevent RigidBody from messing with iTween actions
        this.GetComponent<Collider>().isTrigger = true;
        this.GetComponent<Rigidbody>().useGravity = false;
        this.GetComponent<Rigidbody>().isKinematic = true;

        // Disable Controls by disabling the collider 
        handlingHelper.GetComponent<Throwable>().enabled = false;
        handlingHelper.GetComponent<VelocityEstimator>().enabled = false;
        handlingHelper.GetComponent<InteractableHoverEvents>().enabled = false;
        handlingHelper.GetComponent<Interactable>().enabled = false;
        handlingHelper.GetComponent<BoxCollider>().enabled = false;
        handlingHelper.GetComponent<Rigidbody>().useGravity = false;
        handlingHelper.GetComponent<Rigidbody>().isKinematic = true;

        // Move the object smoothly using iTween
        iTween.MoveTo(gameObject, newPosition, 1);
        iTween.RotateTo(gameObject, newRotation.eulerAngles, 1);
    }
}
