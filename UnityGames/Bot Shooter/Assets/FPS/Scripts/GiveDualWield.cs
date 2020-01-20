using UnityEngine;

public class GiveDualWield : MonoBehaviour
{
    void OnDestroy()
    {
        GivePlayerDualWield();
    }

    public void GivePlayerDualWield()
    {
        foreach (var wpnMgr in GameObject.FindObjectsOfType<PlayerWeaponsManager>())
        {
            if (wpnMgr != null && !wpnMgr.isActiveAndEnabled)
            {
                wpnMgr.enabled = true;
                Debug.Log("GIVING DUAL WIELD 2 PLAYA!");
            }
        }
    }
}
