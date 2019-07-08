using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectBox : MonoBehaviour {

    [SerializeField]
    private RectTransform selectRectImg;

    Vector3 StartPos;
    Vector3 EndPos;

	// Use this for initialization
	void Start () {
        selectRectImg.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D rayHit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity);

            if(rayHit.collider != null)
            {
                StartPos = rayHit.point;
                StartPos.z = 0;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            selectRectImg.gameObject.SetActive(false);
        }

        if (Input.GetMouseButton(0))
        {
            if (!selectRectImg.gameObject.activeInHierarchy)
            {
                selectRectImg.gameObject.SetActive(true);
            }

            EndPos = Input.mousePosition;

            Vector3 rectStart = Camera.main.WorldToScreenPoint(StartPos);

            rectStart.z = 0;

            Vector3 centre = (rectStart + EndPos) / 2f;

            float sizeX = Mathf.Abs(rectStart.x - EndPos.x);
            float sizeY = Mathf.Abs(rectStart.y - EndPos.y);

            selectRectImg.sizeDelta = new Vector2(sizeX, sizeY);
            selectRectImg.position = centre;
        }
	}
}
