using UnityEngine;

/// <summary>
/// Contain information about the locker an welding module
/// </summary>
public class Locker : MonoBehaviour {

    [Tooltip("To know if the welding part is unlocked")]
    public bool Unlocked = false;

    [Tooltip("To know if the drawer place is free for another welding part")]
    public bool Free = false;

    [Tooltip("To know if the welding part has been changed")]
    public bool Changed = false;

    /// <summary>
    /// Set Unlocked to true
    /// </summary>
    public void unlockDrawer()
    {
        Unlocked = true;
    }

    /// <summary>
    /// Set Unlocked to false
    /// </summary>
    public void lockDrawer()
    {
        Unlocked = false;
    }
}
