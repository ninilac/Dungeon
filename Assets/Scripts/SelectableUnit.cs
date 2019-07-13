using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class SelectableUnit : Selectable {

    [SerializeField]
    private GameObject SelectArrow;

    [HideInInspector]
    private GameObject SelectArrowObject;

    [SerializeField]
    Seeker seeker;

    [SerializeField]
    private float speed;

    [HideInInspector]
    public Vector2 targetPos;

    [SerializeField]
    Rigidbody2D rb;

    [SerializeField]
    public float nextWaypointDistance;

    Path CurrentPath;
    int CurrentWaypoint = 0;

    private bool isMoving;
    
	// Use this for initialization
	void Start () {
        targetPos = transform.position;
        Vector2 pos = new Vector2(transform.position.x, transform.position.y+2);
        SelectArrowObject = Instantiate(SelectArrow, pos, Quaternion.identity);
        SelectArrowObject.transform.SetParent(transform, true);

        Camera.main.gameObject.GetComponent<RTSControlScript>().selectableUnits.Add(this);

        rb.velocity = Vector2.zero;

        UpdateSelect();
    }
	
	// Update is called once per frame
	void Update () {
        Vector2 pos2D = transform.position;
        /*if(Vector2.Distance(pos2D, targetPos) > speed*Time.deltaTime)
        {
            isMoving = true;
            Vector2 direction = new Vector2(targetPos.x - transform.position.x, targetPos.y - transform.position.y);
            direction = direction.normalized;
            rb.velocity = direction*speed;
            //transform.position = Vector2.MoveTowards(transform.position, targetPos, 0.05f);
        }
        else if (isMoving)
        {
            Vector2 direction = new Vector2(targetPos.x - transform.position.x, targetPos.y - transform.position.y);
            direction = direction.normalized;
            rb.velocity = direction * Vector2.Distance(pos2D, targetPos);
        }*/
        if(CurrentPath != null)
        {
            if(CurrentWaypoint >= CurrentPath.vectorPath.Count)
            {
                rb.velocity = Vector2.zero;
                isMoving = false;
            }
            else
            {
                isMoving = true;
                Vector2 direction = ((Vector2)CurrentPath.vectorPath[CurrentWaypoint] - rb.position).normalized;
                Vector2 movement = direction * speed * Time.deltaTime;

                rb.velocity = movement;

                float distance = Vector2.Distance(rb.position, CurrentPath.vectorPath[CurrentWaypoint]);
                Debug.Log($"{rb.position}, {CurrentPath.vectorPath[CurrentWaypoint]}, {distance}");

                if (distance < nextWaypointDistance)
                {
                    CurrentWaypoint++;
                }
            }
        }
	}

    public override void UpdateSelect()
    {
        if (isSelected)
        {
            SelectArrowObject.SetActive(true);
        }
        else
        {
            SelectArrowObject.SetActive(false);
        }
    }

    public void FindPath(Vector2 target)
    {
        targetPos = target;
        seeker.StartPath(rb.position, target, OnPathComplete);
    }

    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            CurrentPath = p;
            CurrentWaypoint = 0;
        }
    }
}
