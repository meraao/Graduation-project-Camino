using UnityEngine;

public class MiniMap : MonoBehaviour
{
    public GameObject player;
    public void LateUpdate()
    {
        transform.position= new Vector3(player.transform.position.x, 5, player.transform.position.z);

    }
}
