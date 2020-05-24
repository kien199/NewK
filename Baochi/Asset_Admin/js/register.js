$('form').submit(function () {
    if ($("#name").val() == "") {
        $("#name").css("border", "1px solid red");
    }
    else {
        $("#name").css("border", "1px solid #ced4da");
    }

    if ($("#email").val() == "") {
        $("#email").css("border", "1px solid red");
    }
    else {
        $("#email").css("border", "1px solid #ced4da");
    }

    if ($("#password").val() == "") {
        $("#password").css("border", "1px solid red");
    }
    else {
        $("#password").css("border", "1px solid #ced4da");
    }

    if ($("#retype_password").val() != $("#password").val() || $("#retype_password").val() == "") {
        $("#retype_password").css("border", "1px solid red");
    }
    else {
        $("#retype_password").css("border", "1px solid #ced4da");
    }
    if ($("#name").val() != "" && $("#email").val() != "" && $("#password").val() != "" && $("#retype_password").val() != "" && $("#retype_password").val() == $("#password").val()) {
        $("form").submit();
    }
    else {
        return false;
    }
});