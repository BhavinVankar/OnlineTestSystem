
$(document).ready(function () {
    LoadAssessmentData(1);
});

//------------------Assessment Listing--------------------------
function LoadAssessmentData() {
    ShowLoader();
    $.ajax({
        type: 'get',
        url: '/Assessment/Read_Assessments',
        data: null,
        success: function (data) {
            HideLoader();
            $('#AssessmentPartialData').html(data);
            $('#datatable').dataTable();
        },
        error: function (error) {
            ErrorRedirection(error);
        },
    })
};
//------------------Add Assessment--------------------------
function btnAddAssessmentClick() {
    window.location.href = '/Assessment/AddAssessment';
}
function EditAssessment(id) {
    window.location.href = '/Assessment/EditAssessment?id='+id;
}
//------------------View Assessment--------------------------
function ViewAssessment(id) {
    $.ajax({
        type: 'get',
        url: '/Assessment/ViewAssessment',
        data: { id: id },
        success: function (data) {
            $('#modelViewAssessmentBody').html(data);
        },
        error: function (error) {
            ErrorRedirection(error);
        },

    });
}//------------------Delete Assessment--------------------------
function deleteAssessment(id) {
    swal({
        title: "Delete Assessment",
        text: "Are you sure to deleted this Assessment?",
        buttons: true,
        dangerMode: true,
    })
        .then((willDelete) => {
            if (willDelete) {
                $.ajax({
                    type: 'get',
                    url: '/Assessment/DeleteAssessment',
                    data: { id: id },
                    success: function (data) {
                        LoadAssessmentData();
                        toastr.success("Assessment Deleted successfully!");

                    },
                    error: function (error) {
                        ErrorRedirection(error);
                    }
                })
            }
        });
};
