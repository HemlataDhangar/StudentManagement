
//Identifiers
var tempId = 0;
$(document).ready(function () {

    //Student delete confirmation modal pop-up
    $("#ConfDelS").click(function () {
        $.ajax({
            url: "/Student/DeleteStudent",
            method: 'POST',
            data: { studentid: tempId},
            success: function () {
                $('#removeUser').modal('hide');
                $("#StudentTable").DataTable().ajax.reload(null, false);
            },
            error: function () {
                Alert("Something went wrng");
            }
        });
    });

    //Student datatable
    $('#StudentTable').DataTable({
        "searching": true,
        "ordering": true,
        "pageLength": 7,
        "ajax": {
            "url": "/Student/GetStudents",
            "dataSrc": "",
            "method": 'GET'
        },
        "columns": [
            {

                "sWidth": "5%",
                "bSortable": true,
                "sClass": "TextCenter ID",
                "visible": false,
                "data": "StudentId",
                "title": "Id"
            },
            {

                "sWidth": "20%",
                "bSortable": true,
                "sClass": "TextCenter ID",
                "visible": true,
                "data": "Name",
                "title": "Student Name"
            },
            {

                "sWidth": "20%",
                "bSortable": true,
                "sClass": "TextCenter ID",
                "visible": true,
                "data": "DateOfBirth",
                "title": "Date of Birth",

                "render": function (data) {
                    return moment(data).format('MM/DD/YYYY');  
                    return moment(data).format('MM/DD/YYYY');  
                }
            },
            {

                "sWidth": "10%",
                "bSortable": true,
                "sClass": "TextCenter ID",
                "visible": true,
                "data": "Age",
                "title": "Age-Years",

                "render": function (data) {

                    if (data <= 10) {
                        return '<span class="highlight">' + data + '</span>';
                    } else {
                        return '<span >' + data + '</span>';
                    }
                }
            },
            {

                "sWidth": "20%",
                "bSortable": true,
                "sClass": "TextCenter ID",
                "visible": true,
                "data": "GenderType",
                "title": "Gender"
            },
            {
                "sWidth": "20%",
                "bSortable": false,
                "sClass": "TextCenter ID",
                "visible": true,
                "data": "StudentId",
                "title": "Action",
                "render": function (data) {
                    return '<a class="btn btn-success" href="/Student/ManageStudent/?studentid=' + data + '">Edit</a>    <a class="btn btn-danger" onclick="return deleteStudent(' + data + ');"  href="#">Delete</a>';
                }
            }
        ],
        "success": function (data) {
            $('#StudentTable tbody').empty();
            $('#StudentTable tbody').append(data);
        }
    });
});


//Delete student
function deleteStudent(param) { 
    tempId = param;  
    $('#removeUser').modal('show');
}

