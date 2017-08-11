using UnityEngine;

/// <summary>
/// Contain iformation about the magnets
/// </summary>
public class MagnetPlace : MonoBehaviour {

    [Tooltip("To know if the previous magnet has been removed")]
    public bool OldMagnetRemoved = false;

    [Tooltip("To know if the new magnet has been placed")]
    public bool NewMagnetPlaced = false;

    /// <summary>
    /// Set OldMagnetRemoved to true
    /// </summary>
    public void removeOldMagnet()
    {
        OldMagnetRemoved = true;
    }
}
