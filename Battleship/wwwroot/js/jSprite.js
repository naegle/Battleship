/**
    Authors: Eric Naegle, Chris Bordoy, and Tom Nguyen
    Partners: Eric Naegle, Chris Bordoy, and Tom Nguyen
    Date: 11/25/2019
    Course: CS-4540, University of Utah, School of Computing
    Copyright: CS 4540 and Eric Naegle, Chris Bordoy, and Tom Nguyen - This work may not be copied for use in Academic Coursework.

    We, Eric Naegle, Chris Bordoy, and Tom Nguyen, certify that we wrote this code from scratch and did not copy it in part or whole from another source.
    Any references used in the completion of the assignment are cited.

    Sprite management/actions file.
*/
let jSprite = function () {
        let args = {
        debug: false,
        spriteSheet: false,
        container: false,
        columns: false,
        rows: false,
        autoStart: false,
        repeat: true,
        startFrame: false,
        length: false,
        timing: false,
        timings: false,
        widthOffset: 0,
        onStart: false,
        onStop: false,
        onProgress: false,
        onComplete: false,
        onRepeat: false,
    };

    if (arguments[0] && typeof (arguments[0]) == "object") {
        for (var key in arguments[0]) {
            if (args.hasOwnProperty(key)) {
                let v = arguments[0][key];
                if (v == undefined){
                    args[key] = false;
                } else {
                    args[key] = arguments[0][key];
                }                
            }
        }
    }

    let vars = {
        dom: {
            container: false
        },
        img: false,
        spriteSheetW: 0,
        spriteSheetH: 0,
        frameW: 0,
        frameH: 0,

        startFrame: 0,
        endFrame: 0,
        length: 0,
        maxFrames: 0,
        noOfFramesToPlay: 0,

        frame: 1,
        frames: [],
        stopRequested: false,
        bg: false,
        dispose: false,
        status: "stopped",
    };



    function constructor() {
        // validate args
        if (args.startFrame < 1){
            args.startFrame = 1;
        }

        if (args.spriteSheet === false) {
            console.error("jsprite.js: usage error: you must specify a sprite sheet to use!");
            return;
        }

        if (args.container === false) {
            console.error("jsprite.js: usage error: you must specify a container (must be an element id");
            return;
        }

        if (args.columns === false) {
            console.error("jsprite.js: usage error: you must specify the no' of frames (columns) on the x axis!");
            return;
        }

        if (args.rows === false) {
            console.error("jsprite.js: usage error: you must specify the no' of frames (rows) onthe y axis!");
            return;
        }

        if (args.timing === false && args.timings === false) {
            console.error("jsprite.js: usage error: you must specify either timing or timings for each frame!");
            return;
        }

        // Load sprite sheet image
        vars.img = new Image();
        vars.img.onload = imageLoaded;
        vars.img.src = args.spriteSheet;
    }

    function imageLoaded(e) {
        // We can now get the width and height of the sprite sheet calculate vars
        vars.spriteSheetW = e.target.width;
        vars.spriteSheetH = e.target.height;

        vars.frameW = Math.ceil(e.target.width / args.columns);
        vars.frameH = Math.ceil(e.target.height / args.rows);

        // setup css background on container from img src
        let bg = "url(" + e.target.src + ")";

        vars.dom.container = document.getElementById(args.container);
        vars.dom.container.style.backgroundImage = bg;
        vars.dom.container.style.backgroundRepeat = "no-repeat";
        vars.dom.container.style.backgroundPosition = "0px 0px";
        vars.dom.container.style.width = vars.frameW + "px";
        vars.dom.container.style.height = vars.frameH + "px";

        vars.xLim = vars.spriteSheetW / vars.frameW;
        vars.yLim = vars.spriteSheetH / vars.frameH;

        // Handle width offset
        vars.frameW += args.widthOffset;

        vars.dom.container.style.width = vars.frameW + "px";
        vars.dom.container.style.height = vars.frameH + "px";

        // Limits
        vars.maxFrames = args.columns * args.rows;
        calcVars();

        // Calculate all frames and background positions
        // NOTE: This was done in timing loop but to make start frame and end frame playback easier
        // the control to get specific frame from vars.frames by index (index = frame no you want)
        // is easier

        let frame = 0;
        for (let row=0; row < args.rows; row++){
            for (let col=0; col < args.columns; col++){
                frame++;
                let x = 0 - (col * vars.frameW);
                let y = 0 - (row * vars.frameH);
                vars.frames.push( [x,y] );
            }
        }
        vars.frame = vars.startFrame;

        // Position sprite at first frame
        let startPos = vars.frames[(vars.startFrame-1)];
        let x = startPos[0];
        let y = startPos[1];
        vars.dom.container.style.backgroundPosition = x + "px " + y + "px";

        if (args.debug){
            log("frameW:" + vars.frameW + "   frameH:" + vars.frameH);
            log("cols:" + args.columns + "   rows:" + args.rows);
            log("startPos: " + startPos);
        }

        if (args.autoStart){
            if (args.onStart !== false){
                args.onStart();
            }
            animate();
        }
    }

    function calcVars(){
        // Handle args start frame
        if (args.startFrame === false){
            vars.startFrame = 1;
        } else {
            vars.startFrame = parseInt(args.startFrame);
            if (vars.startFrame < 0){
                vars.startFrame = 1;
            }
        }

        // Handle length
        if (args.length !== false && typeof(args.length) == "number"){
            vars.length = args.length;

            // Check no of frames from startFrame can handle the length
            let possibleEndFrame = vars.startFrame + vars.length-1;
            if (possibleEndFrame > vars.maxFrames){
                // User is trying to play past the number of frames available!
                let msg = "jSprite: You have set a playback length of [" + args.length + "] from a ";
                msg += "starting frame of [" + vars.startFrame + "].";
                msg += " No enough frames to play! RESETTING playback to first and last frames on sprite src[";
                msg += args.spriteSheet + "]";
                console.error(msg);
                vars.startFrame = 1;
                vars.length = vars.maxFrames;
            }
        } else {
            vars.length = vars.maxFrames;
        }

        // Work out number of frames to play
        vars.noOfFramesToPlay = vars.startFrame + (vars.length-1)

        // Work out end frame
        vars.endFrame = vars.startFrame + vars.length - 1;
        if (vars.endFrame > vars.maxFrames){
            vars.endFrame = vars.maxFrames;
        }

        if (args.debug){
            log("jSprite: calc");
            log("maxFrames = " + vars.maxFrames);
            log("startFrame = " + vars.startFrame + "   vars.endFrame = " + vars.endFrame);
            log("vars.length = " + vars.length + "    vars.noOfFramesToPlay = " + vars.noOfFramesToPlay);
        }
    }

    function animate() {
        // Stop for dispose
        if (this.dispose === true){
            return;
        }

        // Handle stop request
        if (vars.stopRequested === true){
            vars.stopRequested = false;
            vars.status = "stopped";

            if (args.onStop !== false){
                args.onStop();
            }
            return;
        }

        if (args.repeat === true){
            if (vars.frame > vars.endFrame){
                vars.frame = vars.startFrame;
            }
        }

        // Playback!
        vars.status = "playing";

        // log(vars.frame);
        let fTime = getFrameTime();
        let framePos = vars.frames[vars.frame-1];

        let msg = "";
        msg += vars.frame + "/" + vars.maxFrames + "   ";
        msg += framePos + "   ";
        msg += "frames.len = " + vars.frames.length + "   ";

        let x = framePos[0];
        let y = framePos[1];
        vars.dom.container.style.backgroundPosition = x + "px " + y + "px";

        if (args.onProgress !== false){
            let o = {
                frame: vars.frame,
                totalFrames: vars.maxFrames
            }
            args.onProgress(o);
        }

        // Move on
        vars.frame++;
        if (vars.frame <= vars.endFrame) {
            setTimeout(animate, fTime);
        } else {
            vars.frame = vars.startFrame;

            if (args.repeat) {
                if (args.onRepeat !== false){
                    args.onRepeat();
                }
                setTimeout(animate, fTime);
            } else {
                if (args.onComplete !== false){
                    args.onComplete();
                }
                vars.status = "stopped";
            }
        }
    }

    //Grabs the frame time
    function getFrameTime() {
        // timing vars
        if (args.timing !== false) {
            // timing is constant
            return args.timing

        } else {
            // timing is array based must be same size as frames
            let t = args.timings[(vars.frame-1)];
            // log(vars.frame + " : " + t);
            if (!t){
                t = 1000;
            }
            return t;
        }
    }

    //Restarts the frame
    function restart(){
        vars.frame = vars.startFrame;
        if (vars.status == "stopped"){
            animate();
        }
    }

    //Starts the frame
    function start() {
        if (vars.status != "playing"){

            if (args.onStart !== false){
                args.onStart();
            }
            animate();
        }
    }

    //Stops the frame
    function stop() {
        vars.stopRequested = true;
    }

    //Logs the frame and outputs it to console.
    function log(arg) {
        console.log(arg);
    }

    //Converts undefined to false
    function convertUndefinedToFalse(v) {
        if (v == undefined){
            v = false;
        };
    }

    //Grabs the inner html of the dom element.
    function html(id,value){
        let element = document.getElementById(id);
        if (element){
            element.innerHTML = value;
        }
    }

    this.start = function () {
        start();
    }

    this.stop = function () {
        stop();
    }

    this.restart = function () {
        restart();
    }

    this.setStartFrame = function(v){
        if (args.debug){
            console.clear();
            log("jSprite.setStartFrame(v:"+v+")");
        }


        args.startFrame = v;
        args.length = false;
        vars.length = false;
        vars.frame = args.startFrame;
        calcVars();
    }

    this.setLength = function(v){

        if (args.debug){
            console.clear();
            log("jSprite.setLength(v:"+v+")");
        }

        args.length = v;
        vars.length = v;
        vars.frame = vars.startFrame;
        calcVars();
    }

    this.dispose = function(){
        vars.dispose = true;
        delete vars;
        delete args;
        delete setLength;
        delete setStartFrame;
        delete start;
        delete stop;
        delete restart;
    }

    constructor();
}