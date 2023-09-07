$(document).ready(function () {
    LoadIndex();
});


var LoadIndex = function () {
    $('#tblCategories').DataTable({
        serverSide: true,
        processing: true,
        order: [[0, 'desc']],

        ajax: {
            url: '/Categories/Data',
            type: "POST"
        },
        rowId: 'Id',
        columns: [
            {
                data: "Id", "name": "Id", render: function (data, type, row) {
                    return "<a href='#' class='fa fa-eye' onclick=Details('" + row.Id + "');> " + row.Id + "</a>";
                }
            },
            { data: "Name", title: "Name" },
            { data: "Description", title: "Description" },
            {
                data: null, render: function (data, type, row) {
                    return "<a href='#' class='btn btn-info btn-xs' onclick=AddEdit('" + row.Id + "');>Edit</a>";
                }
            },
            {
                data: null, render: function (data, type, row) {
                    return "<a href='#' class='btn btn-danger btn-xs' onclick=Delete('" + row.Id + "'); >Delete</a>";
                }
            }
        ],
        'columnDefs': [{
            'targets': [3],
            'orderable': false,
        }],
    });
}