$(document).ready(function () {

    Pagination_ProductCostList();
    LoadEmployeeDropdown();
    LoadProduct();
    TotalCostCount();
    //CalculateGrandTotal();
    $("#btnShowModal").click(function () {

        $("#btnSave").show();
        $("#btnEditPost").hide();
        ResetForm();
        $("#ModalHeadLine").empty();
        $("#ModalHeadLine").html("Add Product Cost");
        LoadEmployeeDropdown();
        LoadProduct();

        var d = new Date();
        var todayDate = d.getFullYear() + "/" + (d.getMonth() + 1) + "/" + d.getDate();

        $("#DateOfOrder").val(todayDate)
        $("#ProductCostModal").modal('show');
        
    });





    $("#Quantity").keyup(function () {
        var quantity = $("#Quantity").val();
        var unitprice = $("#UnitPrice").val();
        var discount = $("#Discount").val();
        var transportCost = $("#TransportCost").val()
        var subTotal = parseFloat(quantity * unitprice + transportCost) - parseFloat(discount);
        $("#Subtotal").val(subTotal);
        $("#GrandTotal").val(subTotal);
    })

    $("#Discount").keyup(function () {
        var quantity = $("#Quantity").val();
        var unitprice = $("#UnitPrice").val();
        var discount = $("#Discount").val();
        var transportCost = $("#TransportCost").val()
        var subTotal = parseFloat(parseFloat(quantity * unitprice) + parseFloat(transportCost)) - parseFloat(discount);
        $("#Subtotal").val(subTotal);
        $("#GrandTotal").val(subTotal);
    })


    $("#CurrentPayment").keyup(function () {
        var cPayment=parseFloat($("#CurrentPayment").val());
        var GTotalPayment = parseFloat($("#GrandTotal").val());
        var dAmount = parseFloat(GTotalPayment - cPayment)
        $("#DueAmount").val(dAmount);
        
    });


    $("#ProductId").mouseup(function () {
        LoadBrand();
    });


    $("#BrandId").mouseup(function () {
        ProductCategory();
    });



});





function TotalCostCount() {
    
    var GrandTotal =0;
    for (var i = 0; i <= $("#tbl_ProductCost tbody tr").length; i++) {
   
        //console.log($("#tbl_ProductCost tbody tr").eq(i).find("td:eq(10)").html());
        //var subtotal = parseFloat($("#tbl_ProductCost tbody tr").eq(i).find("td:eq(9)").html());
        //console.log(subtotal);
        //GrandTotal += parseInt(subtotal);
        //$("#grandTotal").text(GrandTotal);

        console.log($("#tbl_ProductCost tbody tr").length);

    }
  

}





function LoadProduct() {
    $.ajax({
        url: '/Home/LoadProduct',
        type: 'GET',
        processType: false,
        contentType: 'application/json',
        success: function (data) {
            console.log(data);

            var optionOfProduct = '<option value="">Select product</option>';

            $.each(data, function (index, pd) {
                optionOfProduct += '<option value="' + pd.productId + '">' + pd.productName + '</option>';
            });

            $("#ProductId").empty();
            $("#ProductId").append(optionOfProduct);

        },
        error: function (GetError) {
            alert(GetError.responseText);
        }


    });
}

function LoadProductDropdown() {
    $("#ProductId").mouseover(function () {
        $.ajax({
            url: '/Home/LoadProduct',
            type: 'GET',
            processType: false,
            contentType: 'application/json',
            success: function (data) {
                console.log(data);

                var optionOfProduct = '<option value="">Select product</option>';

                $.each(data, function (index, pd) {
                    optionOfProduct += '<option value="' + pd.productId + '">' + pd.productName + '</option>';
                });

                $("#ProductId").empty();
                $("#ProductId").append(optionOfProduct);

            },
            error: function (GetError) {
                alert(GetError.responseText);
            }


        });
    });
};



function ProductCategory() {
    var brandId = $("#BrandId").val();

    $.ajax({
        url: '/Home/LoadProductCategory/' + brandId,
        type: 'GET',
        processType: false,
        contentType: 'application/json',
        success: function (data) {
            console.log(data);

            var optionOfProductCategory = '<option value="">Select product Category </option>';

            $.each(data, function (index, pc) {
                optionOfProductCategory += '<option value="' + pc.productCategoryId + '">' + pc.productCategoryName + '</option>';
            });
            $("#ProductCategoryId").empty();
            $("#ProductCategoryId").append(optionOfProductCategory);

        },
        error: function (GetError) {
            alert(GetError.responseText);
        }


    });
}


function LoadBrand() {
    var productId = $("#ProductId").val();

    $.ajax({
        url: '/Home/LoadBrand/' + productId,
        type: 'GET',
        processType: false,
        contentType: 'application/json',
        success: function (data) {
            console.log(data);

            var optionOfBrand = '<option value="">Select Brand </option>';

            $.each(data, function (index, bd) {
                optionOfBrand += '<option value="' + bd.brandId + '">' + bd.brandName + '</option>';
            });
            $("#BrandId").empty();
            $("#BrandId").append(optionOfBrand);

        },
        error: function (GetError) {
            alert(GetError.responseText);
        }


    });
}





function LoadEmployeeDropdown() {

    $.ajax({
        url: '/Home/LoadEmployee',
        type: 'GET',
        processType: false,
        contentType: 'application/json',
        success: function (data) {
            console.log(data);

            var optionOfEmployee = '<option value="">Select Employee </option>';

            $.each(data, function (index, emp) {
                optionOfEmployee += '<option value="' + emp.employeeId + '">' + emp.employeeName + '</option>';
            });

            $("#EmployeeId").empty();
            $("#EmployeeId").append(optionOfEmployee);

        },
        error: function (GetError) {
            alert(GetError.responseText);
        }


    });
}












// define a function to send jquery ajax request
function Pagination_ProductCostList(currentPage, pageSize) {

    var currentPage = currentPage || 1;   // default currentPage is 1
    var pageSize = pageSize || 3;     //default pageSize is 10
    $.get("/ProductCostCtrl/Index_ProductCostList",

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
            var tableHtml = "";       // define the string to represent data of customers

            //for (var i = 0; i < data.data.length; i++) { //loop through the customer data and splice every customer
            //    tableHtml += "<tr><td>" + data.data[i].CustomerID + "</td><td>" + data.data[i].CompanyName + "</td><td>" + data.data[i].ContactName + "</td><td>" + data.data[i].ContactTitle + "</td><td>" + data.data[i].Address + "</td></tr>"
            //}

            console.log(data.data);
            $.each(data.data, function (key, item) {

                tableHtml += '<tr><td>' + item.productName + '</td><td>' + item.brandName + '</td><td>' + item.productCategoryName + '</td><td>' + item.productSize + '</td><td>' + item.unitPrice + '</td> ' +
                    '<td>' + item.quantity + '</td><td>' + item.transportCost + '</td><td>' + item.discount + '</td><td>' + item.dateOfOrder+'</td><td>' + item.subtotal + '</td> ' +
                    '<td>' + item.grandTotal + '</td><td>' + item.currentPayment + '</td><td>' + item.dueAmount + '</td><td>' + item.employeeName + '</td> ' +
                    '<td><button type="button" class="btn btn-warning" onclick="GetEditData(' + item.productCostInfoId + ')">Edit</button>| ' +
                    '<button type="button" class="btn btn-danger" onclick="DeleteConfirm(' + item.productCostInfoId + ')">Delete</button></td></tr> ';

            });


            $("tbody").html(tableHtml);  // fill the customer data into tbody

            $(".pagination li").not(".active").find("a").click(function () {
                // bind click event to every a link except the current link
                var currentPage = $(this).attr("href"); // get the page stored in the a link's href attribute
                var pageSize = 3;     // default pageSize
                Pagination_ProductCostList(currentPage, pageSize);    // recall the function to send an ajax request
                return false;   // return false to prevent refresh the page
            })
        }
        ,
        "json"
    )

}

$(function () {
    // call the defined function
    Pagination_ProductCostList();
})


function PostNewProductCost() {

    var frmData = {
        ProductId: $("#ProductId").val(),
        BrandId: $("#BrandId").val(),
        ProductCategoryId: $("#ProductCategoryId").val(),
        ProductSize: $("#ProductSize").val(),
        UnitPrice: $("#UnitPrice").val(),
        Quantity: $("#Quantity").val(),
        TransportCost: $("#TransportCost").val(),
        Discount: $("#Discount").val(),
        DateOfOrder: $("#DateOfOrder").val(),
        Subtotal: $("#Subtotal").val(),
        GrandTotal: $("#GrandTotal").val(),
        CurrentPayment: $("#CurrentPayment").val(),
        DueAmount: $("#DueAmount").val(),
        EmployeeId: $("#EmployeeId").val()
    }


    console.log(frmData);

    $.ajax({
        url: '/ProductCostCtrl/Create',
        type: 'POST',
        data: JSON.stringify(frmData),
        dataType: 'JSON',
        processType: false,
        contentType: 'application/json',
        success: function (data) {
            console.log(data);
            alert(data);
            ResetForm();
            $("#ProductCostModal").modal('hide');
            window.location.replace('/ProductCostCtrl/Index');
            getIndex_ProductCostList();
        },
        error: function (GetError) {
            alert(GetError.responseText);
        }


    })
}




var EditPost_productCostInfoId = '';

function GetEditData(ProductCostInfoId) {
    $.get('/ProductCostCtrl/Details/' + ProductCostInfoId, {}, function (data) {
        console.log(data);
            ResetForm();

        $("#ProductId").html('<option value="' + data.productId + '" selected>' + data.productName + '</option>');
        $("#BrandId").html('<option value="' + data.brandId + '" selected>' + data.brandName + '</option>');
        $("#ProductCategoryId").html('<option value="' + data.productCategoryId + '" selected>' + data.productCategoryName + '</option>');
        $("#ProductSize").val(data.productSize);
        $("#UnitPrice").val(data.unitPrice);
        $("#Quantity").val(data.quantity);
        $("#TransportCost").val(data.transportCost);
        $("#Discount").val(data.discount);
        $("#DateOfOrder").val(data.dateOfOrder);
        $("#Subtotal").val(data.subtotal);
        $("#GrandTotal").val(data.grandTotal);
        $("#CurrentPayment").val(data.currentPayment);
        $("#DueAmount").val(data.dueAmount);
        $("#EmployeeId").val(data.employeeId);
        EditPost_productCostInfoId = data.productCostInfoId;
           
        $("#ModalHeadLine").empty();
        $("#ModalHeadLine").html("Edit Product Cost");
        $("#btnSave").hide();
        $("#btnEditPost").show();
        $("#ProductCostModal").modal('show');
        LoadProductDropdown();
    })
}






function PostUpdateProductCostInfo() {

    var frmEditData = {
        ProductCostInfoId: EditPost_productCostInfoId,
        ProductId: $("#ProductId").val(),
        BrandId: $("#BrandId").val(),
        ProductCategoryId: $("#ProductCategoryId").val(),
        ProductSize: $("#ProductSize").val(),
        UnitPrice: $("#UnitPrice").val(),
        Quantity: $("#Quantity").val(),
        TransportCost: $("#TransportCost").val(),
        Discount: $("#Discount").val(),
        DateOfOrder: $("#DateOfOrder").val(),
        Subtotal: $("#Subtotal").val(),
        GrandTotal: $("#GrandTotal").val(),
        CurrentPayment: $("#CurrentPayment").val(),
        DueAmount: $("#DueAmount").val(),
        EmployeeId: $("#EmployeeId").val()

    }



    $.ajax({
        url: '/ProductCostCtrl/Edit',
        type: 'POST',
        data: JSON.stringify(frmEditData),
        dataType: 'JSON',
        processType: false,
        contentType: 'application/json; charset=UTF-8',
        success: function (data) {
            console.log(data);
            alert(data);
            ResetForm();
            $("#ProductCostModal").modal('hide');
            window.location.replace('/ProductCostCtrl/Index');
            getIndex_ProductCostList();
        },
        error: function (GetError) {
            alert(GetError.responseText);
        }


    })

}



function SearchProductCostInfo() {


    var Vmojb = {
        EmployeeMobileNo: $("#SearchText").val(),
    }

    console.log(Vmojb);

    $.ajax({
        url: '/SearchProductCostCtrl/Search_VmCostProductInfo',
        type: 'POST',
        data: JSON.stringify(Vmojb),
        dataType: 'JSON',
        processType: false,
        contentType: 'application/json; charset=UTF-8',
        success: function (data) {

            console.log(data);
            

            var SearchHtml = '';
            $.each(data, function (key, item) {
                SearchHtml += '<tr><td>' + item.productName + '</td><td>' + item.brandName + '</td><td>' + item.productCategoryName + '</td><td>' + item.productSize + '</td><td>' + item.unitPrice + '</td> ' +
                    '<td>' + item.quantity + '</td><td>' + item.transportCost + '</td><td>' + item.discount + '</td><td>' + item.dateOfOrder + '</td><td>' + item.subtotal + '</td> ' +
                    '<td>' + item.grandTotal + '</td><td>' + item.currentPayment + '</td><td>' + item.dueAmount + '</td><td>' + item.employeeName + '</td> <td>' + item.employeeAddress + '</td><td>' + item.employeeMobileNo + '</td>' +
                    '<td><button type="button" class="btn btn-warning" onclick="GetEditData(' + item.productCostInfoId + ')">Edit</button>| ' +
                    '<button type="button" class="btn btn-danger" onclick="DeleteConfirm(' + item.productCostInfoId + ')">Delete</button></td></tr> ';

            });

            if (data.length<=0) {
                alert("sorry no data has found!!");
                $("#Search_tblCostDetail").hide();
            } else {
                $("#div_productCostDetails_Display").hide();
                $("#DisplaySearchResult").find("tr").each(function () {
                    $(this).remove();
                });

                $("#DisplaySearchResult").find("thead").addClass("thead-light");
                $("#DisplaySearchResult").html(SearchHtml);
                $("#div_Search").show();
            }


           


        },
        error: function (GetError) {
            alert(GetError.responseText);
        }


    })

}





function DeleteConfirm(productCostInfoId) {
    var ans = confirm('are you sure you want to delete this record???');
    if (ans) {
        $.post('/ProductCostCtrl/Delete/' + productCostInfoId, {}, function (data) {
            alert(data);
            window.location.replace('/ProductCostCtrl/Index');
        })
    } else {
        window.location.replace('/ProductCostCtrl/Index');
    }


}




function RefreshProductCostDetails() {
    window.location.replace('/ProductCostCtrl/Index');
    TotalCostCount();
}







function ResetForm() {
    $("#ProductId").val('');
    $("#BrandId").val('');
    $("#ProductCategoryId").val('');
    $("#ProductSize").val('');
    $("#UnitPrice").val('');
    $("#Quantity").val('');
    $("#TransportCost").val('');
    $("#Discount").val('');
    $("#DateOfOrder").val('');
    $("#Subtotal").val('');
    $("#GrandTotal").val('');
    $("#CurrentPayment").val('');
    $("#DueAmount").val('');
    $("#EmployeeId").val('');
}




//function CalculateGrandTotal() {
//    var grd = 0;
//    for (var i = 1; i <= $("#tbl_ProductCost tr").length; i++) {
//        console.log(i);
//    }
//}