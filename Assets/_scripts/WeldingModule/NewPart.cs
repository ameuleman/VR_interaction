using UnityEngine;
using Valve.VR.InteractionSystem;

/// <summary>
/// NewPart is a class to represent the part to put at the DrawerPlace.
/// When it collides with a collider with "DrawerPlace" tag, it is placed automatically 
/// at the required position and it is no longer interactsible.
/// </summary>
public class NewPart : Drawer
{
    [Tooltip("Required position of the NewPart")]
    public Transform FinalPosition;

    private void OnTriggerEnter(Collider other)
    {
        // The NewPart is placed if the Locker is unlocked and free 
        // and when it collides with a collider with "DrawerPlace" tag
        if (other.gameObject.tag == "DrawerPlace" && Locker.Unlocked && Locker.Free)
        {
            _wellPlaced = true;

            this.GetComponent<Collider>().isTrigger = true;
            this.GetComponent<Rigidbody>().useGravity = false;
            this.GetComponent<Rigidbody>().isKinematic = true;

            unlockMovements();
            lockControl();

            Locker.Changed = true;

            // Move the object smoothly using iTween
            iTween.RotateTo(gameObject, FinalPosition.rotation * Vector3.forward, 3);
            iTween.MoveTo(gameObject, FinalPosition.position, 3);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Fall if it leaves a collider with tag "ObjectSupport" before being placed
        if (other.gameObject.tag == "ObjectSupport" && !_wellPlaced)
        {
            fall();
        }
    }

    private void Start()
    {
        previousEvent = Handle.GetComponent<InteractableHoverEvents>().onHandHoverBegin;
    }
}
