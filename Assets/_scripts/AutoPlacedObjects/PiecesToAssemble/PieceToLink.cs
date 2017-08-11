using UnityEngine;

/// <summary>
/// Class to handle a piece to put at a certain position given by a tag.
/// </summary>
public class PieceToLink : AutoPlacedObject
{
    [Tooltip("the tag of the destination GameObject")]
    public string PlaceTag;

    private bool _isReady = false;

    /// <summary>
    /// Enables to know if the piece has been placed properly
    /// </summary>
    /// <returns></returns>
    public bool getIsReady()
    {
        return _isReady;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == PlaceTag)
        {
            // Place at the collider's position and rotation if it has the corresponding tag
            this.place(other.transform);
            _isReady = true;
        }
    }
}