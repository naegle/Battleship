PIXI.utils.sayHello();

var isPlayerTurn;

// create a 10x10 grid when the page loads
// creates a hover effect that changes the color of a square to black when the mouse passes over it, leaving a (pixel) trail through the grid
// allows the click of a button to prompt the user to create a new grid
$(document).ready(function () {
    createGrid(10);
    isPlayerTurn = true;
});

// function that builds a grid in the "container"
function createGrid(x) {
    for (var rows = 0; rows < x; rows++) {
        for (var columns = 0; columns < x; columns++) {
            $("#playerGrid").append("<div class='Player_Cell' " + "id=" + columns + "_" + rows + " x=" + columns + " y=" + rows + ">" + "<canvas id=canvasPlayerCell_" + columns + "_" + rows + " width=50 height=50 ></canvas>" + "</div>");
            $("#AIGrid").append("<div class='AI_Cell' " + "id=" + "AICell_" + columns + "_" + rows + " x=" + columns + " y=" + rows + " onclick=\"ShootCellAIGrid(this.id)\"" + ">" + "<canvas id=canvasAICell_" + columns + "_" + rows + " width=50 height=50 ></canvas>" + "</div>");
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
    if (isPlayerTurn) {

        isPlayerTurn = false;

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

                            if (response.resultText.includes("WIN")) {
                                //ExplosionAtAILocation("canvasAICell", response.col, response.row);
                                //$("#" + elementID + ".AI_Cell").css("background-color", "red");

                                updateHighScore(response.score);

                                Swal.fire({
                                    title: 'You Won, Do you want to play again?',
                                    text: "Here is your score " + response.score + "%" + "\n" + "You have earned " + response.credit + " credits.",
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
                                ExplosionAtAILocation("canvasAICell", response.col, response.row);
                                $("#" + elementID + ".AI_Cell").css("background-color", "red");
                                setTimeout(ShootCellPlayerGrid, 1500);
                                
                                // if it's a WINN after the hit
                            }
                            else if (response.resultText == "MISS") {
                                splashAtAILocation("canvasAICell", response.col, response.row);
                                $("#" + elementID + ".AI_Cell").css("background-color", "grey");
                                setTimeout(ShootCellPlayerGrid, 1500);
                                
                            }
                            // name the ship is down
                            else if (!response.resultText.includes("WIN")) {
                                ExplosionAtAILocation("canvasAICell", response.col, response.row);
                                $("#" + elementID + ".AI_Cell").css("background-color", "red");
                                alert("AI ship: " + response.resultText + " is sunked.");
                                setTimeout(ShootCellPlayerGrid, 1500);
                                
                            }
                        }

                    }

                }
            }
        });



    }
}

function ShootCellPlayerGrid() {
    $.ajax({
        method: "POST",
        url: "/GamePlay/FireAtPlayerGrid",
        success: function (response) {
            if (response.success) {

                if (response.resultText.toUpperCase().includes("LOSE")) {
                    ExplosionAtAILocation("canvasPlayerCell", response.col, response.row);
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
                            CreateGameService();
                        }
                    });
                }
                // if hit
                else if (response.resultText == "HIT") {
                    ExplosionAtAILocation("canvasPlayerCell", response.col, response.row);
                    $("#" + response.col + "_" + response.row + ".Player_Cell").css("background-color", "red");
                    isPlayerTurn = true;

                }
                else if (response.resultText == "MISS") {
                    splashAtAILocation("canvasPlayerCell", response.col, response.row);
                    $("#" + response.col + "_" + response.row + ".Player_Cell").css("background-color", "grey");
                    isPlayerTurn = true;
                }
                else {
                    ExplosionAtAILocation("canvasPlayerCell", response.col, response.row);
                    $("#" + response.col + "_" + response.row + ".Player_Cell").css("background-color", "red");
                    alert("Player ship:" + response.resultText + " is sunked");
                    isPlayerTurn = true;
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
                updateFullGrid(response);
                Swal.fire("The game is ready");
            }
        }
    });
}


function rocketBarrage() {

    $.ajax({
        method: "POST",
        url: "/GamePlay/RocketBarrage",
        data: {},
        success: function (response) {
            if (response.success) {
                updateCellAfterShot(response.resultText1, response.col1, response.row1);
                updateCellAfterShot(response.resultText2, response.col2, response.row2);
                updateCellAfterShot(response.resultText3, response.col3, response.row3);
                updateCellAfterShot(response.resultText4, response.col4, response.row4);
                updateCellAfterShot(response.resultText5, response.col5, response.row5);
                updateCellAfterShot(response.resultText6, response.col6, response.row6);
                updateCellAfterShot(response.resultText7, response.col7, response.row7);
                updateCellAfterShot(response.resultText8, response.col8, response.row8);
                updateCellAfterShot(response.resultText9, response.col9, response.row9);
                updateCellAfterShot(response.resultText10, response.col10, response.row10);

                document.getElementById("cannonBarrageCount").innerHTML = response.power1Count;

                ShootCellPlayerGrid();
            }
            else {
                Swal.fire({
                    icon: 'error',
                    title: 'Oh no',
                    text: "It's seem you don't have enough quantity to use this."
                })
            }
        }
    });
}

function updateCellAfterShot(result, column, row) {
    if (result != "NOT YOUR TURN") {

        // ignore duplicate shots
        if (result != "DUPLICATE") {

            if (result == "WIN") {
                //$("#" + elementID + ".AI_Cell").css("background-color", "red");
                updateHighScore(column);

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
                        CreateGameService();
                    }
                });
            }
            // if hit
            else if (result == "HIT") {

                $("#AICell_" + column + "_" + row + ".AI_Cell").css("background-color", "red");

                // if it's a WINN after the hit
            }
            else if (result == "MISS") {
                $("#AICell_" + column + "_" + row + ".AI_Cell").css("background-color", "grey");
            }
            // name the ship is down
            else {
                $("#AICell_" + column + "_" + row + ".AI_Cell").css("background-color", "red");
                alert("AI ship: " + result + " is sunked.");
            }
        }

    }
}

function updateHighScore(score) {
    $.ajax({
        method: "POST",
        url: "/HighScore/AddHighScore",
        data: { score: score },
        success: function (response) {
        }
    });
}

// This wipes both grids and shows the players' ships.
function updateFullGrid(response) {
    if (response.gridStatus.includes("WIN")) {
        //$("#" + elementID + ".AI_Cell").css("background-color", "red");
        updateHighScore(10);

        Swal.fire({
            title: 'You Won, Do you want to play again?',
            text: "Here is your score 1%",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, rematch!'
        }).then((result) => {
            if (result.value) {
                Swal.fire("You game is reset");
                location.reload();
                CreateGameService();
            }
        });
    }

    responseArray = response.gridStatus.split(" ");

    for (i = 0; i < 300; i = i + 3) {
        cellStatus = responseArray[i];
        column = responseArray[i + 1];
        row = responseArray[i + 2];

        //Put player cell to blank or ship
        if (cellStatus == "NONE") {
            $("#" + column + "_" + row + ".Player_Cell").css("background-color", "aqua");
        }
        else {
            $("#" + column + "_" + row + ".Player_Cell").css("background-color", "black");
        }

        $("#AICell_" + column + "_" + row + ".AI_Cell").css("background-color", "aqua");
    }
}


var canvasWidth = 50;
var canvasHeight = 50;

var spriteWidth;
var spriteHeight;

var rows;
var cols;

var trackRight = 0;

//  Which row
var currentTrack;

var width;
var height;

var curFrame = 0;
var frameCount = 4;

var x;
var y;

var srcX;
var srcY;
var canvas;
var ctx;

var character = new Image();

var stopInterval;

function ExplosionAtAILocation(user, col, row) {
    spriteWidth = 192;
    spriteHeight = 192;
    rows = 4;
    cols = 4;
    width = spriteWidth / cols;
    height = spriteHeight / rows;
    frameCount = 4;
    currentTrack = 0;
    x = 0;
    y = 0;

    canvas = document.getElementById(user + "_" + col + "_" + row);
    canvas.width = canvasWidth;
    canvas.height = canvasHeight;
    ctx = canvas.getContext("2d");
    character.src = "/images/explosion.png";
    stopInterval = setInterval(drawExplosion, 85);
}

function splashAtAILocation(user, col, row) {
    spriteWidth = 128;
    spriteHeight = 32;
    rows = 1;
    cols = 4;
    width = spriteWidth / cols;
    height = spriteHeight / rows;
    frameCount = 4;
    currentTrack = 0;
    x = 13;
    y = 13;


    canvas = document.getElementById(user + "_" + col + "_" + row);
    canvas.width = canvasWidth;
    canvas.height = canvasHeight;
    ctx = canvas.getContext("2d");

    character.src = "/images/Splash.png";

    stopInterval = setInterval(drawSplash, 100);
}
//  Below code is the splash sprite animation
function updateSplashFrame() {

    if (curFrame == frameCount - 1) {
        ctx.clearRect(x, y, width, height);
        clearInterval(stopInterval);
        character.src = null;
    }

    curFrame = ++curFrame % frameCount;
    srcX = curFrame * width;
    ctx.clearRect(x, y, width, height);
    srcY = trackRight * height;
}

function drawSplash() {
    updateSplashFrame();
    ctx.drawImage(character, srcX, srcY, width, height, x, y, width, height);
}

//  Below code is the animation for explosion sprite
function updateExplosionFrame() {

    if (currentTrack == 3 && curFrame == 3) {
        clearInterval(stopInterval);
        character.src = null;
    }


    curFrame = ++curFrame % frameCount;
    if (curFrame == 3) {
        srcY = ++currentTrack * height;
        srcX = 0;
    } else {
        srcX = curFrame * width;
        ctx.clearRect(x, y, width, height);
    }



}

function drawExplosion() {
    updateExplosionFrame();
    ctx.drawImage(character, srcX, srcY, width, height, x, y, width, height);
}