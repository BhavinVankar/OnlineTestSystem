function ajaxGet(url, successCallback, errorCallback) {
    $.ajax({
        url: url,
        type: 'Get',
        contentType: 'application/json; charset=UTF-8',
        dataType: 'json',
        success: successCallback,
        error: errorCallback
    })
}
function ajaxGetHtml(url, successCallback, errorCallback) {
    $.ajax({
        url: url,
        type: 'Get',
        contentType: 'application/json',
        success: successCallback,
        error: errorCallback
    })
}
function ajaxPost(url, data, successCallback, errorCallback) {
    $.ajax({
        url: url,
        type: 'POST',
        async: false,
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        dataType: 'json',
        data: data,
        success: successCallback,
        error: errorCallback
    })
}

function ajaxPostHtml(url, data, successCallback, errorCallback) {
    $.ajax({
        url: url,
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(data),
        success: successCallback,
        error: errorCallback
    })
}
function ajaxPostHtmlContent(url, data, successCallback, errorCallback) {
    $.ajax({
        url: url,
        type: 'POST',
        async: false,
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        dataType: 'text',
        data: data,
        success: successCallback,
        error: errorCallback
    })
}
function ajaxPostJson(url, data, successCallback, errorCallback) {
    $.ajax({
        url: url,
        type: 'POST',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        data: data,
        success: successCallback,
        error: errorCallback
    })
}
$(document).ready(function () {
    $('#datatable').dataTable();
});
function ErrorRedirection(error) {
    HideLoader();
    if (error.status == 400) {
        toastr.error(" Bad Request!", 'Error');

    }
    else if (error.status == 401) {
        toastr.error("Unauthorized request.!", 'Error');

    } else if (error.status == 403) {
        toastr.error("Internal Error!", 'Error');

    } else if (error.status == 404) {
        toastr.error("Not Found!", 'Error');
    } else if (error.status == 405) {
        toastr.error("Method Not Allowed!", 'Error');

    } else if (error.status == 500) {
        toastr.error("Internal Server Error.", 'Error');
    }
    else {
        window.location.href = '/Account/Signin?returnUrl=' + encodeURIComponent(window.location.href);
    }
}
function ShowLoader() {
    $(".loader").css('display', 'block');

}
function HideLoader() {
    $(".loader").css('display', 'none');

}