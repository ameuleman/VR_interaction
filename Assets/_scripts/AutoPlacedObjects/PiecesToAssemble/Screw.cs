using UnityEngine;

/// <summary>
/// Class to represent a screw.
/// A screw can be controlled by the user and is automatically placed at the right position
/// when it collides with a GameObject with an empty ScrewPlace component.
/// </summary>
public class Screw : AutoPlacedObject
{
    [Tooltip("Object that has to be placed before putting screws")]
    public PieceToLink pieceToLink;

    private void OnTriggerEnter(Collider other)
    {
        //Placed only if the pieceToLink is ready
        ScrewPlace screwPlace = other.GetComponent<ScrewPlace>();
        if (screwPlace && pieceToLink.getIsReady() && screwPlace.isEmpty)
        {
            screwPlace.isEmpty = false;
            this.place(other.transform);
        }
    }
}
