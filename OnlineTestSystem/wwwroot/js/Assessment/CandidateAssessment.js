
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
function btnSubmitAssessmentData() {
   var formData= $('#assessmentFormId').serialize();
    $.ajax({
        type: 'post',
        url: '/Assessment/SubmitAssessment',
        data: formData,
        success: function (data) {
            LoadAssessmentData();
            if (data == true) {
                toastr.success("Assessment Completed successfully!");
                redirectToPage();
            }
            else {
                $("#modelAddUserBody").html(data);
            }
        },
        error: function (error) {
            ErrorRedirection(error);
        },
    });
}; function redirectToPage() {
    window.location.href = "/Assessment/PendingAssessmentList";
}
