$(document).ready(function () {
    Pagination_EmployeeList();
    $("#btnShowModal").click(function () {
        ResetForm();
        $("#btnEditPost").hide();
        $("#btnSave").show();
        $("#EmployeeModal").modal("show");
        LoadCountry();
    })

    LoadCountry();

    $("#CountryId").mouseup(function () {
        var ConId = $("#CountryId").val();

        $.ajax({
            url: '/CountryDistrictThana/LoadDistrictByConId/' + ConId,
            type: 'GET',
            processType: false,
            contentType: 'application/json',
            success: function (data) {
                console.log(data);

                var optionOfDistrict = '<option value="">Select District</option>';
                

                $.each(data, function (index, item) {
                    optionOfDistrict += '<option value="' + item.districtId + '">' + item.districtName+'</option>';
                });
                $("#DistrictId").empty();
                $("#DistrictId").append(optionOfDistrict);

            },
            error: function (GetError) {
                alert(GetError.responseText);
            }


        });
    });



    $("#DistrictId").mouseup(function () {
        var DistrictId = $("#DistrictId").val();

        $.ajax({
            url: '/CountryDistrictThana/LoadThanaByDistId/' + DistrictId,
            type: 'GET',
            processType: false,
            contentType: 'application/json',
            success: function (data) {
                console.log(data);

                var optionOfThana = '<option value="">Select thana </option>';

                $.each(data, function (index, item) {
                    optionOfThana += '<option value="' + item.thanaId + '">' + item.thanaName + '</option>';
                });
                $("#ThanaId").empty();
                $("#ThanaId").append(optionOfThana);

            },
            error: function (GetError) {
                alert(GetError.responseText);
            }


        });
    });






});




function LoadCountry() {
    $.ajax({
        url: '/CountryDistrictThana/LoadCountry',
        type: 'GET',
        processType: false,
        contentType: 'application/json',
        success: function (data) {
            console.log(data);

            var optionOfCountry = '<option value="">Select Country</option>';

            $.each(data, function (index, item) {
                optionOfCountry += '<option value="' + item.countryId + '">' + item.countryName + '</option>';
            });
            $("#CountryId").empty();
            $("#CountryId").append(optionOfCountry);

        },
        error: function (GetError) {
            alert(GetError.responseText);
        }


    });
}



function LoadCountry_MouseOver() {
    
    $("#CountryId").mouseover(function () {
        $.ajax({
            url: '/CountryDistrictThana/LoadCountry',
            type: 'GET',
            processType: false,
            contentType: 'application/json',
            success: function (data) {
                console.log(data);

                var optionOfCountry = '<option value="">Select Country</option>';

                $.each(data, function (index, item) {
                    optionOfCountry += '<option value="' + item.countryId + '">' + item.countryName + '</option>';
                });
                $("#CountryId").empty();
                $("#CountryId").append(optionOfCountry);

            },
            error: function (GetError) {
                alert(GetError.responseText);
            }


        });
    })
    
}






function SearchEmployeeInfo() {
    var SearchValue = $("#SearchText").val();
    var SearchPerams = {
         EmployeeName:SearchValue,
      EmployeeAddress:SearchValue,
     EmployeeMobileNo:SearchValue,
                Email:SearchValue,
           GenderName:SearchValue,
          CountryName:SearchValue,
         DistrictName:SearchValue,
            ThanaName:SearchValue
    }

    console.log(SearchPerams);

    $.ajax({
        type: 'POST',
        url: "/EmployeeCtrl/SearchEmployee",
        dataType: 'JSON',
        data: JSON.stringify(SearchPerams),
        contentType: "application/json",
        success: function (data) {

            console.log(data);
            var EmployeeList = '';
            $.each(data, function (index, item) {
                EmployeeList += ' <tr><td>' + item.employeeName + '</td><td>' + item.employeeAddress + '</td><td>' + item.genderName + '</td><td>' + item.employeeMobileNo + '</td><td>' + item.countryName + '</td><td>' + item.districtName + '</td><td>' + item.thanaName + '</td><td><button type="button" onclick="GetEditEmployee(' + item.employeeId + ')" class="btn btn-success">Edit</button><button type="button" onclick="DeleteEmployeeRecord(' + item.employeeId + ')" class="btn btn-warning">Delete</button></td></tr>';
            })
            $("#tblSearchEmployee").show();
            $("#tblEmployeeList").hide();
            $("#tblSearchEmpDisplay").append(EmployeeList);
            console.log(EmployeeList);
        },
        error: function (GetError) {

            alert(GetError.responseText);

        }
    });


}

function Pagination_EmployeeList(currentPage, pageSize) {

    var currentPage = currentPage || 1;   // default currentPage is 1
    var pageSize = pageSize || 5;     //default pageSize is 10
    $.get("/EmployeeCtrl/Index_Employee",

        { currentPage: currentPage, pageSize: pageSize }, // pass the parameter to server
        function (data) {
            var pagination = "";   // the string to represent the pagination
            if (currentPage != 1) { // if is the first page , not show the previous link

                pagination += '<li class="page-item "><a class="page-link" href="' + (parseInt(currentPage) - 1) + '" tabindex = "-1"  > Previous</a ></li >'
            }
            for (var i = 1; i <= data.totalPage; i++) {
                // splice pagination data of page 1,2,3,4,5...
                pagination += ' <li class="page-item  ' + (i == currentPage ? 'active' : '') + '"><a class="page-link " href="' + (i == currentPage ? "javascript:;" : i) + '">' + i + '</a></li>';

            }

            if (currentPage != data.totalPage) {  // if is the last page, not show the next link
                console.log(currentPage + 1);
                pagination += ' <li class="page-item"><a class="page-link" href = "' + (parseInt(currentPage) + 1) + '" > Next</a ></li >'
            }
            $(".pagination").html(pagination);  // fill the pagination 
            var tableHtml ='';       // define the string to represent data of customers

            for (var i = 0; i < data.data.length; i++) { //loop through the customer data and splice every customer
                tableHtml+= ' <tr><td>' + data.data[i].employeeName + '</td><td>' + data.data[i].employeeAddress + '</td><td>' + data.data[i].genderName + '</td><td>' + data.data[i].employeeMobileNo + '</td><td>' + data.data[i].countryName + '</td><td>' + data.data[i].districtName + '</td><td>' + data.data[i].thanaName + '</td><td><button type="button" onclick="GetEditEmployee(' + data.data[i].employeeId + ')" class="btn btn-success">Edit</button><button type="button" onclick="DeleteEmployeeRecord(' + data.data[i].employeeId + ')" class="btn btn-warning">Delete</button></td></tr>';
            }


            //$.each(data.data, function (key, item) {
            //    tableHtml += ' <tr><td>' + item.employeeName + '</td><td>' + item.employeeAddress + '</td><td>' + item.genderName + '</td><td>' + item.employeeMobileNo + '</td><td>' + item.countryName + '</td><td>' + item.districtName + '</td><td>' + item.thanaName + '</td><td><button type="button" onclick="GetEditEmployee(' + item.employeeId + ')" class="btn btn-success">Edit</button><button type="button" onclick="DeleteEmployeeRecord(' + item.employeeId + ')" class="btn btn-warning">Delete</button></td></tr>';

            //});

            $("#DisplayEmpList").empty();
            $("#DisplayEmpList").append(tableHtml);  // fill the customer data into tbody

            $(".pagination li").not(".active").find("a").click(function () {
                // bind click event to every a link except the current link
                var currentPage = $(this).attr("href"); // get the page stored in the a link's href attribute
                var pageSize = 5;     // default pageSize
                Pagination_EmployeeList(currentPage, pageSize);    // recall the function to send an ajax request
                return false;   // return false to prevent refresh the page
            })
        }
        ,
        "json"
    )

}

$(function () {
    // call the defined function
    Pagination_EmployeeList();
})



function PostNewEmployee() {

    var frmObj = {
        EmployeeName:$("#EmployeeName").val(),
        EmployeeAddress:$("#EmployeeAddress").val(),
        EmployeeMobileNo:$("#EmployeeMobileNo").val(),
        Email:$("#Email").val(),
        GenderId:$("#GenderId").val(),
        CountryId:$("#CountryId").val(),
        DistrictId:$("#DistrictId").val(),
        ThanaId:$("#ThanaId").val()
    }




    $.ajax({
        type: 'POST',
        url: "/EmployeeCtrl/Create",
        dataType: 'JSON',
        data: JSON.stringify(frmObj),
        contentType: "application/json",
        success: function (data) {

            alert(data);
            window.location.replace("/EmployeeCtrl/Index");

        },
        error: function (GetError) {

            alert(GetError.responseText);

        }
    });

}

var EmpId = '';

function GetEditEmployee(employeeId) {



    $.ajax({
        type: 'GET',
        url: "/EmployeeCtrl/Details/" + employeeId,
        dataType: 'JSON',
        contentType: "application/json",
        success: function (data) {

            console.log(data);
            ResetForm();
            EmpId = data.employeeId;
            $("#EmployeeName").val(data.employeeName);
            $("#EmployeeAddress").val(data.employeeAddress);
            $("#EmployeeMobileNo").val(data.employeeMobileNo);
            $("#Email").val(data.email);
            $("#GenderId").val(data.genderId);
            //$("#CountryId").val(data.countryId);
            $("#CountryId").empty();
            $("#CountryId").html(' <option value="' + data.countryId + '" selected>' + data.countryName + '</option>');
            $("#DistrictId").empty();
            $("#DistrictId").html(' <option value="' + data.districtId + '" selected>' + data.districtName + '</option>');
            $("#ThanaId").empty();
            $("#ThanaId").html(' <option value="' + data.thanaId + '" selected>' + data.thanaName+'</option>');
            $("#ModalHeadLine").empty();
            $("#ModalHeadLine").html("Edit Employee");
            $("#btnEditPost").show();
            $("#btnSave").hide();
            $("#EmployeeModal").modal("show");
            LoadCountry_MouseOver();
            console.log(EmpId);

        },
        error: function (GetError) {

            alert(GetError.responseText);

        }
    });

}

function UpdatePost_Employee() {


    var frmData = {
        EmployeeId :EmpId,
        EmployeeName:$("#EmployeeName").val(),
        EmployeeAddress:$("#EmployeeAddress").val(),
        EmployeeMobileNo:$("#EmployeeMobileNo").val(),
        Email:$("#Email").val(),
        GenderId:$("#GenderId").val(),
        CountryId:$("#CountryId").val(),
        DistrictId:$("#DistrictId").val(),
        ThanaId:$("#ThanaId").val()
    }

    $.ajax({
        type: 'POST',
        url: "/EmployeeCtrl/Edit",
        dataType: 'JSON',
        data: JSON.stringify(frmData),
        contentType: "application/json",
        success: function (data) {

            alert(data);
            window.location.replace("/EmployeeCtrl/Index");

        },
        error: function (GetError) {

            alert(GetError.responseText);

        }
    });

}


function DeleteEmployeeRecord(employeeId) {
    var ans = confirm("Are you sure you want to Delete this record!!");

    if (ans) {
        $.post('/EmployeeCtrl/Delete/' + employeeId, {}, function(data) {
            alert(data);
            window.location.replace("/EmployeeCtrl/Index");
        })
    }
}




function ResetForm() {
    $("#EmployeeName").val('');
    $("#EmployeeAddress").val('');
    $("#EmployeeMobileNo").val('');
    $("#Email").val('');
    $("#GenderId").val('');
    $("#CountryId").val('');
    $("#DistrictId").val('');
    $("#ThanaId").val('');
}


function RefreshEmployeeIndex() {
    $("#tblSearchEmployee").hide();
    $("#tblEmployeeList").show();
    window.location.replace("/EmployeeCtrl/Index");
}








