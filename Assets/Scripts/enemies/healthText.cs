using UnityEngine;
using TMPro;

public class healthText : MonoBehaviour
{
    public TextMeshPro messageText;
    

    void Update()
    {
        // limit rot to y axis
        Transform _player = levelController.instance.thePlayer.transform;
        Vector3 _theTarget = new Vector3(_player.position.x, transform.position.y, _player.position.z);
        //turn the enemy body
        transform.LookAt(_theTarget);
    }
   
}
