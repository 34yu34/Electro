using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class BossPortal : MonoBehaviour
{
    private BoxCollider _boxCollider;
    public BoxCollider BoxCollider => _boxCollider ??= GetComponent<BoxCollider>();

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.TryGetComponent(out PlayerController _))
        {
            return;
        }

        FindObjectOfType<LevelController>().ChangeScene("Lvl");
    }
}
