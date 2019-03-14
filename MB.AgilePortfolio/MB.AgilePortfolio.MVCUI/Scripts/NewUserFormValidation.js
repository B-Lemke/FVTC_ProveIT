var formInvalid;

function validateRegistration() {
    console.log("Working!");
    //form is currently valid
    formInvalid = false;

    //Clear out any errors from JS or the server
    $("#errorMsgs").empty();
    $('.validation-summary-errors').empty();


    //Check that email isn't empty and less than 50 characters and regEx for email format against practical RFC 2822
    var emailVal = $("#email").val()
    if (emailVal == "") {
        addError("Email is required.");
    } else if (emailVal.length > 50) {
        addError("Email must be 50 characters or less.");
    } else if (!validateEmail(emailVal)) {
        addError("This is not a valid email. Please try another.");
    }


    //Check the password
    var passwordValid = checkPassword();
    if (!passwordValid) {
        addError("Password is invalid");
        //Possibly highlight the password requirement box?
    }

    //Check that first name isn't empty and less than 50 characters
    var firstNameVal = $("#firstName").val()
    if (firstNameVal == "") {
        addError("First name is required.");
    } else if (firstNameVal.length > 50) {

        addError("First name must be 50 characters or less.");
    }


    //Check that last name isn't empty and less than 50 characters
    var lastNameVal = $("#lastName").val()
    if (lastNameVal == "") {
        addError("Last name is required.");
    } else if (lastNameVal.length > 50) {
        addError("Last name must be 50 characters or less.");
    }

    


    if (formInvalid) {
        $("#jsErrorBox").removeClass("d-none");
        $("#jsErrorBox").show(1000);
        return false;
    }

}

function addError(errorMsg) {
    formInvalid = true;

    var errorElement = document.createElement("li");
    errorElement.innerHTML = errorMsg;
    $("#errorMsgs").append(errorElement);
}




function validateEmail(email) {
    var re = /[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?/;
    console.log(re.test(String(email).toLowerCase()));
    return re.test(String(email).toLowerCase());
}