function displaycomment(data) {
    $(".lastcomments").empty();
    $.each(data, function (i, item) {
        if (item && item.Comments && item.Comments.length > 0) {
            $.each(item.Comments, function (j, commentDetail) {
                var formatDate = fomateDate(commentDetail.Date);
                var username = commentDetail.UserName;
                var rows = "<li class = 'list-group-item'><p><b class='yorumbaslik'>"
                    + username + "</b></p> \ <p>"
                    + item.Comment + "</p> \ <span class='float-right'><i class=''>"
                    + formatDate + "</i></span></li>";
                $('.lastcomments').append(rows);
            });          
        } else {
            console.warn('No comment details for item:', item);
        }
        
    });
}
function commentcontent() {
    $.ajax
        ({
            type: 'GET',
            url: 'Partial/DetailComments',
            dataType: 'json',
            data: {},
            success: function (data) {
                displaycomment(data);
            },
            error: function (xhr, status, error) {
                console.error('AJAX hatası:', status, error);
            }
        });
}
function postComment(data) {
    $.ajax({
        url: 'Comment/CreateComment',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(data),
        dataType: 'json',
        success: function (data) {
        },
        error: function (xhr, status, error) {
            console.error('AJAX hatası:', status, error);
        }
    });
}
function fomateDate(dateString) {
    // Verilen stringi sayısal kısmı al
    var unixTime = parseInt(dateString.match(/\d+/)[0]);
    // Unix zamanını Date objesine çevir
    var date = new Date(unixTime);
    // Tarih ve saat bilgisini istediğiniz formata dönüştür
    var formattedDate = ("0" + date.getDate()).slice(-2) + "/" + ("0" + (date.getMonth() + 1)).slice(-2) + "/" + date.getFullYear();
    var formattedTime = ("0" + date.getHours()).slice(-2) + ":" + ("0" + date.getMinutes()).slice(-2);
    var formattedDateTime = formattedDate + " " + formattedTime;
    return formattedDateTime;
}
$(function () {
/*    articleComment();*/
    commentcontent();
});
