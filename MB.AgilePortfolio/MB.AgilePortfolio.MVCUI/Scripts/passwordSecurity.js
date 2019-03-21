var validPass;

function checkPassword() {
    validPass = true;


    //clear out errors
    $('#displayPasswordMsg').empty();


    var newPassword = $('#passwordInput').val();
    var minNumberofChars = 6;
    var maxNumberofChars = 16;

    var regularExpressionNum = /.*[0-9].*/;
    var regularExpressionChar = /.*[!@#$%^&*].*/;
    var regularExpressionCap = /.*[A-Z].*/;
    var regularExpressionLow = /.*[a-z].*/;

    if (newPassword.length < minNumberofChars || newPassword.length > maxNumberofChars) {
        addPasswordError("Password must be between 6 and 16 characters.");
    }
    if (!regularExpressionChar.test(newPassword)) {
        addPasswordError("Password must contain a special character !@#$%^&*.");
    }
    if (!regularExpressionNum.test(newPassword)) {
        addPasswordError("Password must contain a number.");
    }
    if (!regularExpressionCap.test(newPassword)) {
        addPasswordError("Password must contain an uppercase letter.");
    }
    if (!regularExpressionLow.test(newPassword)) {
        addPasswordError("Password must contain a lowercase letter.");
    }


    if (!validPass) {
        $('#displayPasswordMsg').removeClass("d-none");
    } else {
        $('#displayPasswordMsg').addClass("d-none");
        clearPassMatch();
    }
    return validPass;
    
}



function addPasswordError(errorMsg) {
    validPass = false;

    //Only add the error messages on the page if there is a value entered, but throw the errors still even if empty.
    if ($('#passwordInput').val() !== "") {
        var pswdErrorElement = document.createElement("p");
        pswdErrorElement.innerHTML = errorMsg;
        $("#displayPasswordMsg").append(pswdErrorElement);
    }
}


function passMatch() {

    var newPass = $('#passwordInput').val();
    var confirmPass = $('#confirmPasswordInput').val();

    clearPassMatch();


    if (newPass !== confirmPass) {
        //Don't put in the error message if the confirm password is cleared out, but still throw the error
        if (confirmPass !== "") {
            var confirmPassErr = document.createElement("p");
            confirmPassErr.innerHTML = "Passwords must match";
            $("#confirmPassMatchMsg").append(confirmPassErr);
            $('#confirmPassMatchMsg').removeClass("d-none");
        } else {
            //pass doesn't match, but it's empty
        }

        return false;
    } else {
        return true;
    }
}

function clearPassMatch() {
    //Clear the matching password field
    $('#confirmPassMatchMsg').addClass("d-none");
    $("#confirmPassMatchMsg").empty();
}