
$(document).ready(function () {
    LoadAssessmentMappingData();
});

//------------------Assessment Listing--------------------------
function LoadAssessmentMappingData() {
    ShowLoader();
    $.ajax({
        type: 'get',
        url: '/AssessmentMapping/Read_AssessmentsMapping',
        data: null,
        success: function (data) {
            HideLoader();
            $('#AssessmentMappingPartialData').html(data);
            $('#datatable').dataTable();
        },
        error: function (error) {
            ErrorRedirection(error);
        },
    })
};

//------------------Add Assessment Mapping--------------------------
function btnAddAssessmentMappingClick() {
    $.ajax({
        type: 'get',
        url: '/AssessmentMapping/AddAssessmentMapping',
        data: null,
        success: function (data) {
            $("#modelAddAssessmentMappingBody").html(data);
        },
        error: function (error) {
            ErrorRedirection(error);
        },
    });
}

function AddAssessmentMappingData() {
    $.ajax({
        type: 'post',
        url: '/AssessmentMapping/AddAssessmentMapping',
        data: $('#AddAssessmentMappingForm').serialize(),
        success: function (data) {
            LoadAssessmentMappingData();
            if (data == true) {
                $('#addclose').click();
                toastr.success("User added successfully!");
            }
            else {
                $("#modelAddUserBody").html(data);
            }
        },
        error: function (error) {
            ErrorRedirection(error);
        },
    });
};
//------------------View Assessment Mapping--------------------------
function ViewAssessmentMapping(id) {
    $.ajax({
        type: 'get',
        url: '/AssessmentMapping/ViewAssessmentMapping',
        data: { id: id },
        success: function (data) {
            $('#modelViewAssessmentMappingBody').html(data);
        },
        error: function (error) {
            ErrorRedirection(error);
        },

    });
}
