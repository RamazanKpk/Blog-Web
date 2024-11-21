$(document).ready(function () {
    fetchArticleData();

    function fetchArticleData() {
        $.ajax({
            url: 'Article/GetArticle',
            type: 'GET',
            data: {},
            dataType: 'json',
            success: function (data) {

            },
            error: function (xhr, status, error) {
                console.error('AJAX hatasý:', status, error);
            }
        });
    }
    function fetchCategoryData() {
        $.ajax({
            url: 'Partial/GetCategory',
            type: 'GET',
            data: {},
            dataType: 'json',
            success: function (data) {

            },
            error: function (xhr, status, error) {
                console.error('AJAX hatasý:', status, error);
            }
        });
    }

    function fetchCategoryData() {
        $.ajax({
            url: 'Article/GetCategory',
            type: 'GET',
            data: {},
            dataType: 'json',
            success: function (data) {

            },
            error: function (xhr, status, error) {
                console.error('AJAX hatasý:', status, error);
            }
        });
    }
    function postArticleData(data) {
        $.ajax({
            url: 'Article/CreateArticle',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(data),
            dataType: 'json',
            success: function (data) {
            },
            error: function (xhr, status, error) {
                console.error('AJAX hatasý:', status, error);
            }
        });
    }
    function postLoginUserInfo(data) {
        $.ajax({
            url: 'Home/SendUser',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(data),
            dataType: 'json',
            success: function (data) {
            },
            error: function (xhr, status, error) {
                console.error('AJAX hatasý:', status, error);
            }
        });
    }

});
