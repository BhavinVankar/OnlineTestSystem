
$(document).ready(function () {
    LoadUserData(1);
});

//------------------User Listing--------------------------
function LoadUserData() {
    ShowLoader();
    $.ajax({
        type: 'get',
        url: '/User/Read_Users',
        data: null,
        success: function (data) {
            HideLoader();
            $('#UserPartialData').html(data);
            $('#datatable').dataTable({
                "order": [[0, "desc"]] 
            });
        },
        error: function (error) {
            ErrorRedirection(error);
        },
    })
};

//------------------Add User--------------------------
function btnAddUserClick() {
    $.ajax({
        type: 'get',
        url: '/User/AddUser',
        data: null,
        success: function (data) {
            $("#modelAddUserBody").html(data);
        },
        error: function (error) {
            ErrorRedirection(error);
        },
    });
}
function AddUserData() {
    $.ajax({
        type: 'post',
        url: '/User/AddUser',
        data: $('#AddUserForm').serialize(),
        success: function (data) {
            LoadUserData(1);

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

//------------------View User--------------------------
function ViewUser(id) {
    $.ajax({
        type: 'get',
        url: '/User/ViewUser',
        data: { id: id },
        success: function (data) {
            $('#modelViewUserBody').html(data);
        },
        error: function (error) {
            ErrorRedirection(error);
        },

    });
}

//------------------Edit User--------------------------
function EditUser(id) {

    $.ajax({
        type: 'get',
        url: '/User/EditUser',
        data: { id: id },
        success: function (data) {
            $('#modelEditUserBody').html(data);
        },
        error: function (error) {
            ErrorRedirection(error);
        },

    });
};

function UpdateUserData() {
    $.ajax({
        type: 'post',
        url: '/User/EditUser',
        data: $('#EditUserForm').serialize(),
        success: function (data) {
            LoadUserData(1);
            if (data == true) {
                $('#editclose').click();
                toastr.success("User Updated successfully!");

            }
            else {
                $("#modelEditUserBody").html(data);
            }
        },
        error: function (error) {
            ErrorRedirection(error);
        },
    });
};

//------------------Delete User--------------------------
function deleteUser(id) {
    swal({
        title: "Delete User",
        text: "Are you sure to deleted this User?",
        buttons: true,
        dangerMode: true,
    })
        .then((willDelete) => {
            if (willDelete) {
                $.ajax({
                    type: 'get',
                    url: '/User/DeleteUser',
                    data: { id: id },
                    success: function (data) {
                        LoadUserData();
                        toastr.success("User Deleted successfully!");

                    },
                    error: function (error) {
                        ErrorRedirection(error);
                    }
                })
            }
        });
};

//------------------Block/Unblock User--------------------------
function blockUser(id) {
    swal({
        title: "Block User",
        text: "Are you sure to Block this User?",
        buttons: true,
        dangerMode: true,
    })
        .then((willDelete) => {
            if (willDelete) {
                $.ajax({
                    type: 'get',
                    url: '/User/BlockUser',
                    data: { id: id },
                    success: function (data) {
                        LoadUserData(pageNumber);
                        toastr.success("User Blocked successfully!");
                    },
                    error: function (error) {
                        ErrorRedirection(error);
                    }
                })
            }
        });
};
function unblockUser(id) {
    swal({
        title: "UnBlock User",
        text: "Are you sure to UnBlock this User.",
        buttons: true,
        dangerMode: true,
    })
        .then((willDelete) => {
            if (willDelete) {
                $.ajax({
                    type: 'get',
                    url: '/User/UnblockUser',
                    data: { id: id},
                    success: function (data) {
                        LoadUserData();
                        toastr.success("User Unblocked successfully!");
                    },
                    error: function (error) {
                        ErrorRedirection(error);
                    }
                })
            }
        });
};
