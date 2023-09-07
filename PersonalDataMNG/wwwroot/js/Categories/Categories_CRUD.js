
var Details = function (id) {
    var url = "/Categories/Details?id=" + id;
    $('#titleBigModal').html("Categories Details");
    loadBigModal(url);
};


var AddEdit = function (id) {
    var url = "/Categories/AddEdit?id=" + id;
    if (id > 0) {
        $('#titleBigModal').html("Edit Categories");
    }
    else {
        $('#titleBigModal').html("Add Categories");
    }
    loadBigModal(url);
};

var Save = function () {
    var _Name = $('#Name').val();
    console.log(_Name);

    if (_Name == '' || _Name == null) {
        Swal.fire({
            title: 'Name is Required.',
            icon: "error",
            onAfterClose: () => {
                setTimeout(function () {
                    $('#Name').focus();
                }, 500);
            }
        });
    }

    var _frmCategories = $("#frmCategories").serialize();
    $.ajax({
        type: "POST",
        url: "/Categories/AddEdit",
        data: _frmCategories,
        success: function (result) {
            if (result.IsSuccess) {
                Swal.fire({
                    title: result.AlertMessage,
                    icon: "success"
                }).then(function () {
                    document.getElementById("btnClose").click();
                    $('#tblCategories').DataTable().ajax.reload();
                });
            }
            else {
                Swal.fire({
                    title: result.AlertMessage,
                    icon: "error"
                });
                $('#Name').focus();
            }
        },
        error: function (errormessage) {
            SwalSimpleAlert(errormessage.responseText, "warning");
        }
    });
}

var Delete = function (id) {
    Swal.fire({
        title: 'Do you want to delete this item?',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes'
    }).then((result) => {
        if (result.value) {
            $.ajax({
                type: "DELETE",
                url: "/Categories/Delete?id=" + id,
                success: function (result) {
                    var message = "Categories has been deleted successfully. Categories ID: " + result.Id;
                    Swal.fire({
                        title: message,
                        icon: 'info',
                        onAfterClose: () => {
                            $('#tblCategories').DataTable().ajax.reload();
                        }
                    });
                }
            });
        }
    });
};