
$(document).ready(function () {
    LoadAssessmentHistoryData();
});

//------------------Assessment History Listing--------------------------
function LoadAssessmentHistoryData() {
    ShowLoader();
    $.ajax({
        type: 'get',
        url: '/Assessment/Read_AssessmentHistory',
        data: null,
        success: function (data) {
            HideLoader();
            $('#AssessmentHistoryPartialData').html(data);
            $('#datatable').dataTable();
        },
        error: function (error) {
            ErrorRedirection(error);
        },
    })
};