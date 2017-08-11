using UnityEngine;
using Valve.VR.InteractionSystem;

/// <summary>
/// RemovableDrawer is a class to handle a drawer that can be removed,
/// i.e. that translates along an axis before being unlocked when it exit DrawerPlace collider.
/// An object with a RmovableDrawer component must collide with an object with tag "ObjectSupport" 
/// before being fully removed to make sure it doesn't fall.
/// </summary>
public class RemovableDrawer : Drawer
{
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "DrawerPlace")
        {
            // Unfreeze position constraints when leaving the machine
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

            // empty the drawer so that the newPart can be placed
            Locker.Free = true;

            // Must enter a collider with tag "ObjectSupport" before leaving, otherwise it fallse
            if (!_wellPlaced)
            {
                fall();
            }
        }
        else if(other.gameObject.tag == "ObjectSupport" && Locker.Free)
        {
            // Is not well placed if it leaves a collider with tag "ObjectSupport" 
            _wellPlaced = false;

            if (Locker.Free)
            {
                // Falls in case it leaves a collider with tag "ObjectSupport" after leacing the DrawerPlace
                fall();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Is in a correct position if it collides with an object with tag "ObjectSupport"
        if (other.gameObject.tag == "ObjectSupport")
        {
            _wellPlaced = true;
        }
    }

    private void Start()
    {
        // Prevent the drawer from being controller at the beginning: the Locker must be unlocked
        previousEvent = Handle.GetComponent<InteractableHoverEvents>().onHandHoverBegin;
        lockControl();
    }
}