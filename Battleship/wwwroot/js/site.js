/**
    Authors: Eric Naegle, Chris Bordoy, and Tom Nguyen
    Partners: Eric Naegle, Chris Bordoy, and Tom Nguyen
    Date: 11/25/2019
    Course: CS-4540, University of Utah, School of Computing
    Copyright: CS 4540 and Eric Naegle, Chris Bordoy, and Tom Nguyen - This work may not be copied for use in Academic Coursework.

    We, Eric Naegle, Chris Bordoy, and Tom Nguyen, certify that we wrote this code from scratch and did not copy it in part or whole from another source.
    Any references used in the completion of the assignment are cited.

    The stylesheet for the audio buttons and their functionality.
*/
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

