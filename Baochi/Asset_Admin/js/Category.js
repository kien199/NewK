function SearchCates(search) {
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
            url: "/Admin/Category/Searching",
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
                            myvar = myvar + '<tr>' +
                                '                                                <td class="text-center">' + item.id + '</td>' +
                                '                                                <td>' + item.ten + '</td>' +
                                '                                                <td>' + item.slug + '</td>' +
                                '                                                <td class="text-center text-primary"><a href="/Admin/Category/Editing/' + item.id + '"><i class="far fa-edit"></i></a></td>' +
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

function GenSlug(e) {
    //Lấy text từ thẻ input title 
    var title = e.value;

    //Đổi chữ hoa thành chữ thường
    var slug = title.toLowerCase();

    //Đổi ký tự có dấu thành không dấu
    slug = slug.replace(/á|à|ả|ạ|ã|ă|ắ|ằ|ẳ|ẵ|ặ|â|ấ|ầ|ẩ|ẫ|ậ/gi, 'a');
    slug = slug.replace(/é|è|ẻ|ẽ|ẹ|ê|ế|ề|ể|ễ|ệ/gi, 'e');
    slug = slug.replace(/i|í|ì|ỉ|ĩ|ị/gi, 'i');
    slug = slug.replace(/ó|ò|ỏ|õ|ọ|ô|ố|ồ|ổ|ỗ|ộ|ơ|ớ|ờ|ở|ỡ|ợ/gi, 'o');
    slug = slug.replace(/ú|ù|ủ|ũ|ụ|ư|ứ|ừ|ử|ữ|ự/gi, 'u');
    slug = slug.replace(/ý|ỳ|ỷ|ỹ|ỵ/gi, 'y');
    slug = slug.replace(/đ/gi, 'd');
    //Xóa các ký tự đặt biệt
    slug = slug.replace(/\`|\~|\!|\@|\#|\||\$|\%|\^|\&|\*|\color{#fff}{|}∣|\+|\=|\,|\.|\/|\?|\>|\<|\'|\"|\:|\;|_/gi, '');
    //Đổi khoảng trắng thành ký tự gạch ngang
    slug = slug.replace(/ /gi, "-");
    //Đổi nhiều ký tự gạch ngang liên tiếp thành 1 ký tự gạch ngang
    //Phòng trường hợp người nhập vào quá nhiều ký tự trắng
    slug = slug.replace(/\-\-\-\-\-/gi, '-');
    slug = slug.replace(/\-\-\-\-/gi, '-');
    slug = slug.replace(/\-\-\-/gi, '-');
    slug = slug.replace(/\-\-/gi, '-');
    //Xóa các ký tự gạch ngang ở đầu và cuối
    slug = '@' + slug + '@';
    slug = slug.replace(/\@\-|\-\@|\@/gi, '');
    //In slug ra textbox có id “slug”
    $("#slug").val(slug);
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