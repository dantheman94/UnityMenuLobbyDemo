using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

//******************************
//
//  Created by: Daniel Marton
//
//  Last edited by: Daniel Marton
//  Last edited on: 12/1/2019
//
//******************************

public class UI_MenuButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {

    //**************************************************************************************************************
    //                                                                                                             *
    //      INSPECTOR                                                                                              *
    //                                                                                                             *
    //**************************************************************************************************************

    [Space]
    [Header("----------------------------------------------------------------------------------------------------")]
    [Header("*** MENU BUTTON HOVER *** ")]
    [Header(" Description ")]
    [Space]
    public Text DescriptorText;
    [TextArea]
    public string DescriptionString;
    [Space]
    [Header("-----------------------------------")]
    [Header(" Button Text ")]
    [Space]
    public Color DefaultTextColour;
    [Space]
    public bool ChangeColourOnHover = true;
    public Color HoveredTextColour;
    [Space]
    public bool ChangeColourOnDisabled = true;
    public Color DisabledTextColour = Color.grey;
    public bool ShowImageOnDisabled = false;
    public Image DisabledImage;

    //**************************************************************************************************************
    //                                                                                                             *
    //      VARIABLES                                                                                              *
    //                                                                                                             *
    //**************************************************************************************************************
    
    private Button _Button;
    private Text _ButtonText;

    //**************************************************************************************************************
    //                                                                                                             * 
    //      FUNCTIONS                                                                                              * 
    //                                                                                                             * 
    //**************************************************************************************************************

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /// <summary>
    //  Called just before Update() for the first time.
    /// </summary>
    private void Start() {

        // Get component references
        _Button = GetComponent<Button>();
        _ButtonText = GetComponentInChildren<Text>();
        if (_ButtonText == null) {

            // Log warning to console
            Debug.LogWarning("WARNING: Could not find a Text component in the children of the button '" +
                             gameObject.name + "'.");
        }
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /// <summary>
    //  Called once every frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void Update() {
        
        if (_Button) {

            // Update the attached button's child text component's colour if the button is disabled
            if (ChangeColourOnDisabled && !_Button.IsInteractable())
                _ButtonText.color = DisabledTextColour;

            // Show the image referenced if the attached button is disabled
            if (DisabledImage)
                DisabledImage.gameObject.SetActive(ShowImageOnDisabled && !_Button.IsInteractable());
        }
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    //**************************************************************************************************************
    //                                                                                                             * 
    //      EVENTS                                                                                                 * 
    //                                                                                                             * 
    //**************************************************************************************************************

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /// <summary>
    //  On button hover
    /// </summary>
    //  <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData) {

        // Change button text colour on hover?
        if (ChangeColourOnHover && _ButtonText)
            _ButtonText.color = HoveredTextColour;

        // Update description text?
        if (DescriptorText)
            DescriptorText.text = DescriptionString;
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /// <summary>
    //  On button unhover
    /// </summary>
    //  <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData) {

        // Return attached button child text component's colour back to the default colour
        if (_ButtonText)
            _ButtonText.color = DefaultTextColour;
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /// <summary>
    //  On button click
    /// </summary>
    // <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData) {

        // Unselect the button from the Unity EventSystem (removes the hover colouring effect)
        EventSystem.current.SetSelectedGameObject(null);

        // Return attached button child text component's colour back to the default colour
        if (_ButtonText)
            _ButtonText.color = DefaultTextColour;
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    //**************************************************************************************************************
    //                                                                                                             * 
    //      GETTERS & SETTERS                                                                                      * 
    //                                                                                                             * 
    //**************************************************************************************************************

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

}
