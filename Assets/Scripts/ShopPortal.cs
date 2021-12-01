using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider))]
public class ShopPortal : MonoBehaviour
{
    private BoxCollider _boxCollider;
    public BoxCollider BoxCollider => _boxCollider ??= GetComponent<BoxCollider>();
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.TryGetComponent(out PlayerController player))
        {
            return;
        }
        
        FindObjectOfType<LevelController>().ChangeScene("Lvl");
    }
}
