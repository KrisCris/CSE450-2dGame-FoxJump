using UnityEngine;

public class KillZone : MonoBehaviour
{
    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D others)
    {
        if (others.CompareTag("Player")) others.gameObject.SendMessage("OnDamage", 10000000);
    }
}