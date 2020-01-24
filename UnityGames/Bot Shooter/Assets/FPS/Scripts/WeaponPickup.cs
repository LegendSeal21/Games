using UnityEngine;

[RequireComponent(typeof(Pickup))]
public class WeaponPickup : MonoBehaviour
{
    [Tooltip("The prefab for the weapon that will be added to the player on pickup")]
    public WeaponController weaponPrefab;

    Pickup m_Pickup;

    void Start()
    {
        m_Pickup = GetComponent<Pickup>();
        DebugUtility.HandleErrorIfNullGetComponent<Pickup, WeaponPickup>(m_Pickup, this, gameObject);

        // Subscribe to pickup action
        m_Pickup.onPick += OnPicked;

        // Set all children layers to default (to prefent seeing weapons through meshes)
        foreach(Transform t in GetComponentsInChildren<Transform>())
        {
            if (t != transform)
                t.gameObject.layer = 0;
        }
    }

    void OnPicked(PlayerCharacterController byPlayer)
    {
        Debug.Log("Wassup, piccking sumpin");

        var playerWeaponsManagers = byPlayer.GetComponents<PlayerWeaponsManager>();
        foreach (var mgr in playerWeaponsManagers)
        {
            if (mgr.AddWeapon(weaponPrefab))
            {
                // Handle auto-switching to weapon if no weapons currently
                if (mgr.GetActiveWeapon() == null)
                {
                    mgr.SwitchWeapon(true);
                }

                m_Pickup.PlayPickupFeedback();

                Destroy(gameObject);
            }
        }
    }
}
