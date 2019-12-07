/**
    Authors: Eric Naegle, Chris Bordoy, and Tom Nguyen
    Partners: Eric Naegle, Chris Bordoy, and Tom Nguyen
    Date: 11/25/2019
    Course: CS-4540, University of Utah, School of Computing
    Copyright: CS 4540 and Eric Naegle, Chris Bordoy, and Tom Nguyen - This work may not be copied for use in Academic Coursework.

    We, Eric Naegle, Chris Bordoy, and Tom Nguyen, certify that we wrote this code from scratch and did not copy it in part or whole from another source.
    Any references used in the completion of the assignment are cited.

    This is the game store state machine.
*/
function purchasePowerUps(powerupType) {

    //do a swal fire to update the use credit
    // and quanity count
    // if user try to buy the power but don't have enough credit 
    // a second swal fire telling the user you don't have enough credit
    Swal.fire({
        title: 'Are you sure you want to make this purchase?',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, give me that power up!'
    }).then((result) => {
        if (result.value) {

            //need to check if user have enough credit
            $.ajax({
                method: "POST",
                url: "/GameStore/PurchasePowerUps",
                data: {
                    powerupID: powerupType
                },
                success: function (response) {

                    if (response.resultText == "NOT ENOUGH") {

                        Swal.fire({
                            icon: 'error',
                            title: "You don't have enough credit to make this purchase.",

                        });

                    }
                    else if (response.resultText == "PURCHASED_POWER1") {
                        document.getElementById("power1Count").innerHTML = "Quantity: " + response.powerupCount;
                        document.getElementById("playerIncome").innerHTML = "CREDIT: " + response.userCredit;

                        Swal.fire('You have purchased this power up!');

                    }
                    else if (response.resultText == "PURCHASED_POWER2") {
                        document.getElementById("power2Count").innerHTML = "Quantity: " + response.powerupCount;
                        document.getElementById("playerIncome").innerHTML = "CREDIT: " + response.userCredit;

                        Swal.fire('You have purchased this power up!');
                    }
                    else {
                        document.getElementById("power3Count").innerHTML = "Quantity: " + response.powerupCount;
                        document.getElementById("playerIncome").innerHTML = "CREDIT: " + response.userCredit;

                        Swal.fire('You have purchased this power up!');
                    }


                }
            });

        }
    });
}




