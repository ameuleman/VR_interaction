using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

/// <summary>
/// class representing a magnet that will be automatically placed on the surface of
/// the collider of a GameObject with a MagnetPlace component if they collide.
/// </summary>
public class Magnet : AutoPlacedObject
{
    void OnCollisionEnter(Collision collision)
    {
        Collider other = collision.collider;
        MagnetPlace magnetPlace = other.GetComponentInParent<MagnetPlace>();
        if (magnetPlace)
        {
            magnetPlace.NewMagnetPlaced = true;

            // The new position is the point on collision and the new rotation is given by the normal vector of the collider
            ContactPoint contact = collision.contacts[0];
            this.place(contact.point, Quaternion.FromToRotation(Vector3.up, contact.normal));
        }
    }
}
