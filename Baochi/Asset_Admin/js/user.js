function SearchUsers(search) {
    if (search == "") {
        $(".pagination-div").attr('style', 'display: flex !important');

    }
    else {
        $(".pagination-div").attr('style', 'display: none !important');
        var formData = new FormData();
        formData.append('__RequestVerificationToken', $('input[name="__RequestVerificationToken"]').val());

        formData.append('search', search);

        $.ajax({
            type: "POST",
            url: "/Admin/User/Searching",
            data: formData,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            processData: false,
            contentType: false,
            success: function (response) {
                if (response.status) {
                    var data = response.data;
                    $("tbody").empty();
                    if (data.length > 0) {
                        myvar = '';
                        data.forEach(function (item) {
                            myvar = myvar + '<tr id="user-' + item.id + '">' +
                                '                                                <td class="text-center">' + item.id + '</td>' +
                                '                                                <td>' + item.ten + '</td>' +
                                '                                                <td>' + item.email + '</td>' +
                                '                                                <td>' + item.sdt + '</td>' +
                                '                                                <td class="text-center">' + item.ngaysinh + '</td>' +
                                '                                                <td class="text-center">' + item.gioitinh + '</td>' +
                                '                                                <td class="text-center">' + item.vaitro + '</td>' +
                                '                                                <td class="text-center text-primary"><a href="/Admin/User/Editing/' + item.id + '"><i class="far fa-edit"></i></a></td>' +
                                '                                                <td class="text-center text-danger" onclick="Delete(' + item.id + ')" style="cursor: pointer;"><i class="far fa-trash-alt"></i></td>' +
                                '                                            </tr>';
                        });
                        $("tbody").append(myvar);
                    }
                }
            }
        });
    }
}


function Delete(cateId) {
    swal({
        title: "Are you sure?",
        text: "Once deleted, you will not be able to recover this imaginary file!",
        icon: "warning",
        buttons: true,
        dangerMode: true,
    })
        .then((willDelete) => {
            if (willDelete) {
                var formData = new FormData();
                formData.append('__RequestVerificationToken', $('input[name="__RequestVerificationToken"]').val());

                formData.append('cateId', cateId);
                $.ajax({
                    type: "POST",
                    url: "/Admin/Category/Deleting",
                    data: formData,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    processData: false,
                    contentType: false,
                    success: function (response) {
                        if (response.status) {

                            $("#cate-" + cateId).remove();

                            swal("Poof! Your category has been deleted!", {
                                icon: "success",
                            });
                        }
                    }
                });
            } else {
                swal("Your category is safe!");
            }
        });
}