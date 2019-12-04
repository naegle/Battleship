// function that builds a grid in the "container"
function createGrid(x) {
    for (var rows = 0; rows < x; rows++) {
        for (var columns = 0; columns < x; columns++) {
            $("#playerGrid").append("<div class='Player_Cell' " + "id=" + columns + "_" + rows + " x=" + columns + " y=" + rows + "></div>");
            $("#AIGrid").append("<div class='AI_Cell' " + "id=" + columns + "_" + rows + " x=" + columns + " y=" + rows + " onclick=\"ShootCellAIGrid(this.id)\"" + "></div>");
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

function ShootCellAIGrid(elementID) {

    $.ajax({
        method: "POST",
        url: "/GamePlay/FireAIGrid",
        data: {
            coords: elementID
        },
        success: function (response) {
            if (response.success) {

                //  must be player turn

                if (response.resultText != "NOT YOUR TURN") {

                    // ignore duplicate shots
                    if (response.resultText != "DUPLICATE") {

                        if (response.resultText == "WIN") {
                            $("#" + elementID + ".AI_Cell").css("background-color", "red");

                            Swal.fire({
                                title: 'You Won, Do you want to play again?',
                                text: "Here is your score " + response.score + "%",
                                icon: 'warning',
                                showCancelButton: true,
                                confirmButtonColor: '#3085d6',
                                cancelButtonColor: '#d33',
                                confirmButtonText: 'Yes, rematch!'
                            }).then((result) => {
                                if (result.value) {
                                    Swal.fire("You game is reseted");
                                    location.reload();
                                }
                            });
                        }
                        // if hit
                        else if (response.resultText == "HIT") {
                            $("#" + elementID + ".AI_Cell").css("background-color", "red");
                            ShootCellPlayerGrid();

                            // if it's a WINN after the hit
                        }
                        // if hit
                        else if (response.resultText == "MISS") {
                            $("#" + elementID + ".AI_Cell").css("background-color", "grey");
                            ShootCellPlayerGrid();
                        }
                        // name the ship is down
                        else {
                            $("#" + elementID + ".AI_Cell").css("background-color", "red");
                            alert("AI ship: " + response.resultText + " is sunked.");
                            ShootCellPlayerGrid();
                        }
                    }

                }

            }
        }
    });



}

function ShootCellPlayerGrid() {
    $.ajax({
        method: "POST",
        url: "/GamePlay/FireAtPlayerGrid",
        success: function (response) {
            if (response.success) {

                if (response.resultText == "LOSE") {
                    $("#" + response.col + "_" + response.row + ".Player_Cell").css("background-color", "red");

                    Swal.fire({
                        title: 'You Lose, Do you want to play again?',
                        text: "You won't be able to revert this!",
                        icon: 'warning',
                        showCancelButton: true,
                        confirmButtonColor: '#3085d6',
                        cancelButtonColor: '#d33',
                        confirmButtonText: 'Yes, rematch!'
                    }).then((result) => {
                        if (result.value) {
                            Swal.fire("You game is reseted");
                            location.reload();
                        }
                    });
                }
                // if hit
                else if (response.resultText == "HIT") {
                    $("#" + response.col + "_" + response.row + ".Player_Cell").css("background-color", "red");

                }
                else if (response.resultText == "MISS") {
                    $("#" + response.col + "_" + response.row + ".Player_Cell").css("background-color", "grey");
                }
                else {
                    $("#" + response.col + "_" + response.row + ".Player_Cell").css("background-color", "red");
                    alert("Player ship:" + response.resultText + " is sunked");
                }

            }

        }
    });
}

function CreateGameService() {
    $.ajax({
        method: "POST",
        url: "/GamePlay/CreateGameService",
        success: function (response) {
            if (response.success) {
                Swal.fire("The game is ready");
            }
        }
    });
}
// create a 16x16 grid when the page loads
// creates a hover effect that changes the color of a square to black when the mouse passes over it, leaving a (pixel) trail through the grid
// allows the click of a button to prompt the user to create a new grid
$(document).ready(function () {
    createGrid(10);
});

