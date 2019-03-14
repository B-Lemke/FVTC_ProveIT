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

    return validPass;
}

function passMatch(){
    var newPass = $('#passwordInput').val();
    var confirmPass = $('#confirmPasswordInput').val();

    //Clear the matching password field
    $("#confirmPassMatchMsg").empty();

    if (newPass === confirmPass) {
        var confirmPassErr = document.createElement("p");
        confirmPassErr.innerHTML = "Passwords must match";
        $("#confirmPassMatchMsg").append(pswdErrorElement);
        return true;
    } else {
        return false;
    }
}

function addPasswordError(errorMsg) {
    validPass = false;

    var pswdErrorElement = document.createElement("p");
    pswdErrorElement.innerHTML = errorMsg;
    $("#displayPasswordMsg").append(pswdErrorElement);

}
