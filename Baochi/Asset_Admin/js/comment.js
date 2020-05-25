function SearchComments(search) {
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
            url: "/Admin/Comment/Searching",
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
                            if (item.binhluans.length > 0) {
                                item.binhluans.forEach(function (cmt) {
                                    myvar = myvar + '<tr id="comment-' + cmt.id + '">' +
                                        '                                                <td class="text-center">' + cmt.id + '</td>' +
                                        '                                                <td style="max-width: 300px; overflow: hidden; text-overflow: ellipsis; white-space: nowrap; vertical-align: middle;">' + item.tieude + '</td>' +
                                        '                                                <td>' + cmt.tennguoidang + '</td>' +
                                        '                                                <td>' + cmt.noidung + '</td>' +
                                        '                                                <td class="text-center text-danger" onclick="Delete(' + cmt.id + ')" style="cursor: pointer;"><i class="far fa-trash-alt"></i></td>' +
                                        '                                            </tr>';
                                });
                            }
                        });
                        $("tbody").append(myvar);
                    }
                }
            }
        });
    }
}


function Delete(commentId) {
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

                formData.append('commentId', commentId);
                $.ajax({
                    type: "POST",
                    url: "/Admin/Comment/Deleting",
                    data: formData,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    processData: false,
                    contentType: false,
                    success: function (response) {
                        if (response.status) {

                            $("#comment-" + commentId).remove();

                            swal("Poof! Your comment has been deleted!", {
                                icon: "success",
                            });
                        }
                    }
                });
            } else {
                swal("Your comment is safe!");
            }
        });
}