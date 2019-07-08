using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTSSelectScript : MonoBehaviour {

    [SerializeField]
    private LayerMask clickableLayer;

    [HideInInspector]
    private List<Selectable> selectedUnits;

    [HideInInspector]
    public List<Selectable> selectableUnits;

    Vector3 MousePos1;
    Vector3 MousePos2;

    private void Awake()
    {
        selectedUnits = new List<Selectable>();
        selectableUnits = new List<Selectable>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            MousePos1 = Camera.main.ScreenToViewportPoint(Input.mousePosition);

            RaycastHit2D rayHit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, clickableLayer);

            if (rayHit.collider != null)
            {
                SelectableUnit unit = rayHit.collider.GetComponent<SelectableUnit>();
                if(Input.GetKey("left ctrl"))
                {
                    if(unit.isSelected == false)
                    {
                        selectedUnits.Add(unit);
                        unit.isSelected = true;
                        unit.UpdateSelect();
                    }
                    else
                    {
                        selectedUnits.Remove(unit);
                        unit.isSelected = false;
                        unit.UpdateSelect();
                    }
                }
                else
                {
                    if(selectedUnits.Count > 0)
                    {
                        ClearSelection();
                    }
                    selectedUnits.Add(unit);
                    unit.isSelected = true;
                    unit.UpdateSelect();
                }
            }
            else
            {
                ClearSelection();
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            MousePos2 = Camera.main.ScreenToViewportPoint(Input.mousePosition);

            if (MousePos1 != MousePos2)
            {
                SelectObjects();
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            foreach(Selectable pawn in selectedUnits)
            {

                SelectableUnit unit = pawn.GetComponent<SelectableUnit>();
                if(unit != null)
                {
                    unit.FindPath(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                }
            }
        }
	}

    private void SelectObjects()
    {
        List<Selectable> remSelectables = new List<Selectable>();

        if(Input.GetKey("left ctrl") == false)
        {
            ClearSelection();
        }

        Rect selectRect = new Rect(MousePos1.x, MousePos1.y, MousePos2.x - MousePos1.x, MousePos2.y - MousePos1.y);

        foreach(Selectable selectable in selectableUnits)
        {
            if(selectable != null)
            {
                if(selectRect.Contains(Camera.main.WorldToViewportPoint(selectable.transform.position), true))
                {
                    selectedUnits.Add(selectable);
                    selectable.isSelected = true;
                    selectable.UpdateSelect();
                }
            }
            else
            {
                remSelectables.Add(selectable);
            }
        }

        if(remSelectables.Count > 0)
        {
            foreach(Selectable remSelectable in remSelectables)
            {
                selectableUnits.Remove(remSelectable);
            }
            remSelectables.Clear();
        }
    }

    private void ClearSelection()
    {
        foreach (Selectable unit in selectedUnits)
        {
            if(unit != null)
            {
                unit.isSelected = false;
                unit.UpdateSelect();
            }
        }
        selectedUnits.Clear();
    }
}
