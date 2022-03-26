using UnityEngine;

public class KillZone : MonoBehaviour
{
    // Update is called once per frame
    private void OnTriggerStay2D(Collider2D others)
    {
        if (others.CompareTag("Player")) others.gameObject.SendMessage("OnDamage", 5);
    }
}