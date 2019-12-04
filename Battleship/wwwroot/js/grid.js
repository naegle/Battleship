// function that builds a grid in the "container"
function createGrid(x) {
    for (var rows = 1; rows < x + 1; rows++) {
        for (var columns = 1; columns < x + 1; columns++) {
            $("#playerGrid").append("<div class='Player_Cell' " + "id=" + rows + "_" + columns + " x=" + rows + " y=" + columns + "></div>");
            $("#AIGrid").append("<div class='AI_Cell' " + "id=" + rows + "_" + columns + " x=" + rows + " y=" + columns + "></div>");
        };
    };
    $(".Player_Cell").width(500 / x);
    $(".Player_Cell").height(500 / x);
    $(".Player_Cell").droppable({
        snap: true,
        snapTolerance: 50,
        accept: ".battleshipImage",
        classes: {
            "ui-droppable-active": "ui-state-active",
            "ui-droppable-hover": "ui-state-hover"
        },
        drop: function (event, ui) {
            var id = '#' + ui.draggable.attr("id");
            //Sets title
            $(id).prop('title', '1');
        }
    });
    $(".AI_Cell").width(500 / x);
    $(".AI_Cell").height(500 / x);
    $(".AI_Cell").mouseover = function () { mouseHoverAI() };
};

// function that clears the grid
function clearGrid() {
    $(".grid").remove();
};

// function that prompts the user to select the number of boxes in a new grid
// the function then also creates that new grid
function refreshGrid() {
    var z = prompt("How many boxes per side?");
    clearGrid();
    createGrid(z);
};

// create a 16x16 grid when the page loads
// creates a hover effect that changes the color of a square to black when the mouse passes over it, leaving a (pixel) trail through the grid
// allows the click of a button to prompt the user to create a new grid
$(document).ready(function () {

    createGrid(10);
    //$(".grid").mouseover(function () {
    //    $(this).css("background-color", "#808080");
    //});

});

