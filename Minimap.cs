//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class Minimap : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private RectTransform minimapRect;
    [SerializeField] private Transform[] minimapIconsToChange;
    private Camera minimapCam;
    [SerializeField] private float scaleChange;
    [SerializeField] private float scaleChangeReverse;
    [SerializeField] private GameObject panel;
    private Vector3 scale;
    static private short minimapClickToogle;

    private void Start()
    {
        minimapCam = gameObject.GetComponent<Camera>();
        minimapClickToogle = 0;
    }
    private void Update()
    {
        //press M to open/close map
        if (Input.GetKeyDown(KeyCode.M))
        {
            minimapOnclick();
        }
    }
    private void LateUpdate()
    {
        Vector3 newPosition = player.position;
        newPosition.y = transform.position.y;
        transform.position = newPosition;

        //to also make your minimap rotate with the player
        //transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f);
        if(minimapClickToogle == 1)
        {
            if (Input.GetMouseButtonDown(0)) { minimapOnclick(); }
        }
    }

    public void minimapOnclick()
    {
        if (minimapClickToogle == 0)
        {
            minimapRect.localScale = new Vector3(2.5f, 2.5f, 2.5f);
            minimapRect.anchorMin = new Vector2(0.5f, 0.5f);
            minimapRect.anchorMax = new Vector2(0.5f, 0.5f);
            minimapRect.pivot = new Vector2(0.5f, 0.5f);
            minimapCam.orthographicSize = 360f;
            minimapClickToogle = 1;
            panel.SetActive(true);
            foreach (Transform trans in minimapIconsToChange)
            {
                scale = trans.localScale;
                scale *= scaleChange;
                trans.localScale = scale;
            }
        }
        else
        {
            minimapRect.localScale = new Vector3(1, 1, 1);
            minimapRect.anchorMin = new Vector2(1f, 1f);
            minimapRect.anchorMax = new Vector2(1f, 1f);
            minimapRect.pivot = new Vector2(1f, 1f);
            minimapCam.orthographicSize = 50f;
            minimapClickToogle = 0;
            panel.SetActive(false);
            foreach (Transform trans in minimapIconsToChange)
            {
                scale = trans.localScale;
                scale *= scaleChangeReverse;
                trans.localScale = scale;
            }
        }
    }
}
