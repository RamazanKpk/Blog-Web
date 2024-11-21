$(document).ready(function () {

    function postArticleData() {
        var postData = {
            Article = article,
        };
        $.ajax({
            url: 'Article/PostArticle',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(postData),
            dataType: 'json',
            success: function (data) {

            },
            error: function (xhr, status, error) {
                console.error('AJAX hatası:', status, error);
            }
        });

    }
});