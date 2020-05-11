function AddComment(postId) {
    if ($("#comment-username").val() == "") {
        alert("Bạn chưa nhập tên !");
    }
    else if ($("#comment-content").val() == "") {
        alert("Nội dung phải từ 1-2000 ký tự")
    }
    else {
        var formData = new FormData();
        formData.append('__RequestVerificationToken', $('input[name="__RequestVerificationToken"]').val());

        formData.append('username', $("#comment-username").val());
        formData.append('content', $("#comment-content").val());
        formData.append('postId', postId);

        $.ajax({
            type: "POST",
            url: "/Comment/AddComment",
            data: formData,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            processData: false,
            contentType: false,
            success: function (response) {
                if (response.status) {
                    var html = '<div class="post-comment-item">' +
                        '                                            <div class="user-ava">' +
                        '                                                <img src="/Asset/Images/default-avatar.png" alt="Alternate Text" />' +
                        '                                            </div>' +
                        '                                            <div class="post-comment-item-detail">' +
                        '                                                <p class="user-name">' +
                        '                                                    ' + response.data.tennguoidang + '' +
                        '                                                </p>' +
                        '                                                <p class="post-comment-item-content">' +
                        '                                                    ' + response.data.noidung + '' +
                        '                                                </p>' +
                        '                                            </div>' +
                        '                                        </div>';
                    $(".list-post-comment").append(html);
                    $("#comment-username").val("");
                    $("#comment-content").val("");
                    $(".list-post-comment > h3").remove();
                }
            }
        });
    }
}