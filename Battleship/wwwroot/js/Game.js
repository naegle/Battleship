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

function checkDisplayValue() {
    shipsList.forEach(function (item) {
        if (document.getElementById(item).getAttribute("myValue") === "0") {
            if (str.charAt(item.length) === "0") {
                document.getElementById(item + "90").style.display = "block";
                document.getElementById(item).style.display = "none";
            } else {
                document.getElementById(item + "90").style.display = "none";
                document.getElementById(item).style.display = "block";
            }
        }
    });
}

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
        //$('.horizontalList').css('display', 'block');
        //$('.vertList').css('display', 'block');
        updateShipLists();
        //alert("IM HITTING THIS");
        //if (isHorizontal === true) {
        //    alert(isHorizontal);
        //    isHorizontal = false;
        //    for (var i = 0; i < shipsList.length / 2; i++) {
        //        //Gets title
        //        var id = '#' + shipsList[i];
        //        var title = $(id).prop('title');

        //        if (title === '1') {
        //            alert("HOWDY");
        //            $(id).css("display", "block")
        //            alert($(id).css("display"));
        //            $(id + "90").css("display", "none");
        //            alert($(id + "90").css("display"));
        //            shipsList.remove(id);
        //            shipsList.remove(shipsList[i].substring(0, id.shipsList[i].length - 2));
        //        }
        //    }

        //} else {//horizontal list
        //    alert(isHorizontal);
        //    isHorizontal = true;
        //    for (var i = shipsList.length / 2; i < shipsList.length; i++) {
        //        //Gets title
        //        var id = '#' + shipsList[i];
        //        var title = $(id).prop('title');

        //        if (title === '1') {
        //            alert("HOWDY");
        //            $(id + "90").css("display", "block")
        //            alert($(id + "90").css("display"));
        //            $(id).css("display", "none");
        //            alert($(id).css("display"));
        //            //shipsList.remove(shipsList[i].substring(0, id.shipsList[i].length - 2));
        //        }
        //    }
        //}
    }

}

//Should display full vertical/horizontal lists of ships
function displayInitialOrientation() {
    if (firstVertDisplay === false) {
        for (var i = 0; i < horizontalShipsList.length; i++) {
            //Gets title
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
            //Gets title
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
            //Gets title
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