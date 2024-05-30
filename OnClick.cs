//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class OnClick : MonoBehaviour
{
    [SerializeField] private Player player;
    private void Start()
    {
        if(player == null)
        {
            player = FindObjectOfType<Player>();
        }
    }
    /*public void OnMouseDown()
    {
        Debug.Log("was clicked");
        player.FoundSomething(name);
    }*/
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.name == gameObject.name)
                {
                    Logger.Log("was clicked");
                    player.FoundSomething(hit.collider.name);
                }
            }
        }
    }
}
