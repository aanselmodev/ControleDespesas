$(document).ready(function () {
    $(".btn-danger").click(function (e) {
        var result = confirm("Tem certeza que deseja realizar essa operação?");

        if (result == false) {
            e.preventDefault();
        }
    })
})
