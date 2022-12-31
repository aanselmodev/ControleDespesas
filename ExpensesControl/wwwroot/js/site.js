$(document).ready(function () {
    $(".btn-danger").click(function (e) {
        var result = confirm("Tem certeza que deseja realizar essa operação?");

        if (result == false) {
            e.preventDefault();
        }
    })

    $(".btn-show-password").click(function () {
        const passwordField = $(".password-field");
        const btnShowPassword = $(".btn-show-password");

        if (passwordField.attr("type") === "password") {
            passwordField.attr("type", "text");
            btnShowPassword.removeClass("bi-eye").addClass("bi-eye-slash");
        }
        else {
            passwordField.attr("type", "password");
            btnShowPassword.removeClass("bi-eye-slash").addClass("bi-eye");
        }
    })

    $(".btn-show-confirm-password").click(function () {
        const confirmPasswordField = $(".confirm-password-field");
        const btnShowConfirmPassword = $(".btn-show-confirm-password");

        if (confirmPasswordField.attr("type") === "password") {
            confirmPasswordField.attr("type", "text");
            btnShowConfirmPassword.removeClass("bi-eye").addClass("bi-eye-slash");
        }
        else {
            confirmPasswordField.attr("type", "password");
            btnShowConfirmPassword.removeClass("bi-eye-slash").addClass("bi-eye");
        }
    })
})
       
