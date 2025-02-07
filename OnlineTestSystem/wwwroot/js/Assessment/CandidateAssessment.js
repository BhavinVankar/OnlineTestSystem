
$(document).ready(function () {
    LoadAssessmentData();
});

//------------------Assessment Listing--------------------------
function LoadAssessmentData() {
    ShowLoader();
    $.ajax({
        type: 'get',
        url: '/Assessment/Read_PendingAssessments',
        data: null,
        success: function (data) {
            HideLoader();
            $('#PendingAssessmentPartialData').html(data);
            $('#datatable').dataTable();
        },
        error: function (error) {
            ErrorRedirection(error);
        },
    })
};
//------------------Terms and Condition Assessment--------------------------
function TermsAndConditionAssessment(id) {
    $.ajax({
        type: 'get',
        url: '/Assessment/TermsAnsConditionAssessment',
        data: { id: id },
        success: function (data) {
            $('#modelTermsAndConditionAssessmentBody').html(data);
        },
        error: function (error) {
            ErrorRedirection(error);
        },

    });
}
