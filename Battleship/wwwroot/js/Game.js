/**
    Authors: Eric Naegle, Chris Bordoy, and Tom Nguyen
    Partners: Eric Naegle, Chris Bordoy, and Tom Nguyen
    Date: 11/25/2019
    Course: CS-4540, University of Utah, School of Computing
    Copyright: CS 4540 and Eric Naegle, Chris Bordoy, and Tom Nguyen - This work may not be copied for use in Academic Coursework.

    We, Eric Naegle, Chris Bordoy, and Tom Nguyen, certify that we wrote this code from scratch and did not copy it in part or whole from another source.
    Any references used in the completion of the assignment are cited.
    
    This file tried to rotate the ships but since they were attached to drag and drop functionality,
    ran into a huge problem displaying the correct rotated ships after being dropped on the grid.
*/
var horizontalShipsList = [
    "cruiserImage",
    "raiderImage",
    "destroyerImage",
    "battleshipImage",
    "aircraftCarrierImage"
];
var verticalShipsList = [
    "cruiserImage90",
    "raiderImage90",
    "destroyerImage90",
    "battleshipImage90",
    "aircraftCarrierImage90",
];
var isHorizontal;
var shipsPlayList;
var firstVertDisplay;

//Rotates ships on button click
function rotateShips() {
    //vert list
    if (isHorizontal === true) {
        isHorizontal = false;
    } else {
        isHorizontal = true;
    }
    if (checkForDrop() === false) {
        if (firstVertDisplay === false) {
            firstVertDisplay = true;
        } else {
            firstVertDisplay = false;
        }
        displayInitialOrientation();
    }
    else {
        updateShipLists();
    }
}

//Should display full vertical/horizontal lists of ships
function displayInitialOrientation() {
    if (firstVertDisplay === false) {
        for (var i = 0; i < horizontalShipsList.length; i++) {
            id = '#' + horizontalShipsList[i];
            $(id).css('display', 'block');
        }
        for (var i = 0; i < verticalShipsList.length; i++) {
            id = '#' + verticalShipsList[i];
            $(id).css('display', 'none');
        }
    }
    else {
        for (var i = 0; i < horizontalShipsList.length; i++) {
            id = '#' + horizontalShipsList[i];
            $(id).css('display', 'none');
        }
        for (var i = 0; i < verticalShipsList.length; i++) {
            id = '#' + verticalShipsList[i];
            $(id).css('display', 'block');
        }
    }
}

//Checks to see if an image has been dragged and dropped.
function checkForDrop() {
    var id;
    var title;

    for (var i = 0; i < horizontalShipsList.length; i++) {
        //Gets title
        id = '#' + horizontalShipsList[i];
        title = $(id).prop('title');
        if (title === '1') {
            return true;
        }
    }
    for (var i = 0; i < verticalShipsList.length; i++) {
        id = '#' + verticalShipsList[i];
        title = $(id).prop('title');
        if (title === '1') {
            return true;
        }
    }
    return false;
}

//Should update each ship list in terms of visual display
function updateShipLists() {
    var id;
    var title;
    if (isHorizontal === true) {
        for (var i = 0; i < horizontalShipsList.length; i++) {
            //Gets title
            id = '#' + horizontalShipsList[i];
            title = $(id).prop('title');
            if (title === '0') {
                $(id).css('display', 'block');
            }
            if (title === '1') {
                $(id).css('display', 'block');
                $(id + "90").css('display', 'none');
                horizontalShipsList.remove(horizontalShipsList[i]);
                verticalShipsList.remove(horizontalShipsList[i] + "90");
            }
        }

        for (var i = 0; i < verticalShipsList.length; i++) {
            id = '#' + verticalShipsList[i];
            title = $(id).prop('title');
            if (title === '0') {
                $(id).css('display', 'none');
            }
            if (title === '1') {
                $(id).css('display', 'block');
                $(id.substring(0, verticalShipsList[i].length - 2)).css('display', 'none');
                verticalShipsList.remove(id.substring(1, id.length));
                horizontalShipsList.remove(verticalShipsList[i].substring(0, verticalShipsList[i].length - 2));
            }
        }
    } else {
        for (var i = 0; i < horizontalShipsList.length; i++) {
            id = '#' + horizontalShipsList[i];
            title = $(id).prop('title');
            if (title === '0') {
                $(id).css('display', 'none');
            }
            if (title === '1') {
                $(id).css('display', 'block');
                $(id + "90").css('display', 'none');
                horizontalShipsList.remove(horizontalShipsList[i]);
                verticalShipsList.remove(horizontalShipsList[i] + "90");
            }
        }

        for (var i = 0; i < verticalShipsList.length; i++) {
            id = '#' + verticalShipsList[i];
            title = $(id).prop('title');
            if (title === '0') {
                $(id).css('display', 'block');
            }
            if (title === '1') {
                $(id).css('display', 'block');
                $(id.substring(0, verticalShipsList[i].length - 2)).css('display', 'none');
                verticalShipsList.remove(id.substring(1, id.length));
                horizontalShipsList.remove(verticalShipsList[i].substring(0, verticalShipsList[i].length - 2));
            }
        }
    }
}

//document ready function. Runs this after DOM has loaded
$(function () {
    isHorizontal = true;
    firstVertDisplay = true;
    displayInitialOrientation();
});