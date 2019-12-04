// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
document.getElementById("audioControl").volume = .3;
var toggleAudioMute = document.getElementById("audioControl");

function toggleMute() {
    if (toggleAudioMute.paused) {
        toggleAudioMute.play();
        $('#audioButton').css('background-image', 'url(../images/volume2.png)');
    }
    else {
        toggleAudioMute.pause();
        $('#audioButton').css('background-image', 'url(../images/volumeMute2.png)');
    }
}

