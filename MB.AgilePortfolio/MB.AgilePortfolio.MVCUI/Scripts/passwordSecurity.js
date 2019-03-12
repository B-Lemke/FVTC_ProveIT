

function checkPassword() {
    var passwordMessageBox = document.getElementById('displayPasswordMsg');
    //Clear out the box
    passwordMessageBox.innerHTML = '';


    var newPassword = document.getElementById('passwordInput').value;
    var minNumberofChars = 6;
    var maxNumberofChars = 16;

    var regularExpressionNum = /.*[0-9].*/;
    var regularExpressionChar = /.*[!@#$%^&*].*/;
    var regularExpressionCap = /.*[A-Z].*/;
    var regularExpressionLow = /.*[a-z].*/;

    if (newPassword.length < minNumberofChars || newPassword.length > maxNumberofChars) {
        var lengthP = document.createElement("p");
        lengthP.innerHTML = "Password must be between 6 and 16 characters.";
        passwordMessageBox.appendChild(lengthP);
    }
    if (!regularExpressionChar.test(newPassword)) {
        var specialCharactersP = document.createElement("p");
        specialCharactersP.innerHTML = "Password must contain a special character !@#$%^&*.";
        passwordMessageBox.appendChild(specialCharactersP);
    }
    if (!regularExpressionNum.test(newPassword)) {
        var numP = document.createElement("p");
        numP.innerHTML = "Password must contain a number.";
        passwordMessageBox.appendChild(numP);
    }
    if (!regularExpressionCap.test(newPassword)) {
        var capP = document.createElement("p");
        capP.innerHTML = "Password must contain an uppercase letter.";
        passwordMessageBox.appendChild(capP);
    }
    if (!regularExpressionLow.test(newPassword)) {
        var lowP = document.createElement("p");
        lowP.innerHTML = "Password must contain a lowercase letter.";
        passwordMessageBox.appendChild(lowP);
    }
}


