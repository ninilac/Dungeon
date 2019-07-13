using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsContainer : MonoBehaviour, IObserver
{
    #region Observer pattern
    public void Notify()
    {
        //update the UI
    }
    #endregion

    [SerializeField]
    private RTSControlScript ControlScript;

    [SerializeField]
    private List<GameObject> Buttons;

    [SerializeField]
    private List<Sprite> AbilityIcons;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
