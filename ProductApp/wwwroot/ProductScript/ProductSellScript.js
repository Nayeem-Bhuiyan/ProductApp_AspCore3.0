$(document).ready(function () {

    Pagination_ProductSellList();
    LoadEmployeeDropdown();
    LoadCustomerDropdown();
    ResetForm();
    LoadProduct();
    $("#btnShowModal").click(function () {
        ResetForm();
        $("#btnSave").show();
        $("#btnEditPost").hide();
        $("#ModalHeadLine").empty();
        $("#ModalHeadLine").html("Add Product Sell");
        LoadEmployeeDropdown();
        LoadCustomerDropdown();
        LoadProduct();
        var d = new Date();
        var todayDate = d.getFullYear() + "/" + (d.getMonth() + 1) + "/" + d.getDate();
        $("#DateOfSell").val(todayDate);
        $("#ProductSellModal").modal('show');
    });





    $("#Quantity").keyup(function () {
        var quantity = $("#Quantity").val();
        var unitprice = $("#UnitPrice").val();
        var discount = $("#Discount").val();
        var subTotal = parseFloat(quantity * unitprice) - parseFloat(discount);
        $("#Subtotal").val(subTotal);
        $("#GrandTotal").val(subTotal);
    })

    $("#Discount").keyup(function () {
        var quantity = $("#Quantity").val();
        var unitprice = $("#UnitPrice").val();
        var discount = $("#Discount").val();
        var subTotal = parseFloat(quantity * unitprice) - parseFloat(discount);
        $("#Subtotal").val(subTotal);
        $("#GrandTotal").val(subTotal);
    })


    $("#CurrentPayment").keyup(function () {
        var cPayment = parseFloat($("#CurrentPayment").val());
        var GTotalPayment = parseFloat($("#GrandTotal").val());
        var dAmount = parseFloat(GTotalPayment - cPayment)
        $("#DueAmount").val(dAmount);

    });

    $("#ProductId").mouseup(function () {
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
    });


    $("#BrandId").mouseup(function () {
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
    });


    $("#SearchCustomer").keyup(function () {
        FindCustomerNameByMobile();
    });

    $("#CustomerId").mouseup(function () {
        LoadCustomerDropdown();
    });

});




function FindCustomerNameByMobile() {
 

    var searchParams = {
        
        CustomerMobile: $("#SearchCustomer").val(),
       
    }
    console.log(searchParams)


    $.ajax({
        url: '/Customers/FindCustomer',
        type: 'POST',
        data: JSON.stringify(searchParams),
        dataType: 'JSON',
        contentType: 'application/json',
        processData: false,
        success: function (data) {
            
            var text ='';
          
            console.log(data);
            if (data.customerId>0) {
                text = '<option value="' + data.customerId + '" selected>' + data.customerName + '</option>';

            }
            else {
                text = '<option value="0" selected>No customer has found</option>';
            }

            $("#CustomerId").empty();
            $("#CustomerId").html(text);
        },
        error: function (getError) {
            alert(getError.responseText);
        }
    })

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


function LoadCustomerDropdown() {

    $.ajax({
        url: '/Home/LoadCustomer',
        type: 'GET',
        processType: false,
        contentType: 'application/json',
        success: function (data) {
            console.log(data);

            var optionOfCustomer = '<option value="">Select Customer </option>';

            $.each(data, function (index, cust) {
                optionOfCustomer += '<option value="' + cust.customerId + '">' + cust.customerName + '</option>';
            });

            $("#CustomerId").empty();
            $("#CustomerId").append(optionOfCustomer);

        },
        error: function (GetError) {
            alert(GetError.responseText);
        }


    });
}


// define a function to send jquery ajax request
function Pagination_ProductSellList(currentPage, pageSize) {

    var currentPage = currentPage || 1;   // default currentPage is 1
    var pageSize = pageSize || 3;     //default pageSize is 10
    $.get("/ProductSellCtrl/ProductSell_Index",

        { currentPage: currentPage, pageSize: pageSize }, // pass the parameter to server
        function (data) {
            console.log(data);
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

            $.each(data.data, function (key, item) {

                tableHtml += '<tr><td>' + item.productName + '</td><td>' + item.brandName + '</td><td>' + item.productCategoryName + '</td><td>' + item.productSize + '</td><td>' + item.unitPrice + '</td> ' +
                    '<td>' + item.quantity + '</td><td>' + item.discount + '</td><td>' + item.dateOfSell+'</td><td>' + item.subtotal + '</td> ' +
                    '<td>' + item.grandTotal + '</td><td>' + item.currentPayment + '</td><td>' + item.dueAmount + '</td><td>' + item.customerName + '</td><td>' + item.customerMobile + '</td><td>' + item.customerAddress + '</td><td>' + item.employeeName + '</td> <td>' + item.employeeMobileNo + '</td><td>' + item.employeeAddress + '</td>' +
                    '<td><button type="button" class="btn btn-warning" onclick="GetEditData(' + item.productSellInfoId + ')">Edit</button>| ' +
                    '<button type="button" class="btn btn-danger" onclick="DeleteConfirm(' + item.productSellInfoId + ')">Delete</button></td></tr> ';

            });

            $("#tblProductSellData").empty();
            $("#tblProductSellData").html(tableHtml);  // fill the customer data into tbody

            $(".pagination li").not(".active").find("a").click(function () {
                // bind click event to every a link except the current link
                var currentPage = $(this).attr("href"); // get the page stored in the a link's href attribute
                var pageSize = 3;     // default pageSize
                Pagination_ProductSellList(currentPage, pageSize);    // recall the function to send an ajax request
                return false;   // return false to prevent refresh the page
            })
        }
        ,
        "json"
    )

}



$(function () {
    // call the defined function
    Pagination_ProductSellList();
})

function RefreshProductSellDetails() {
    window.location.replace('/ProductSellCtrl/Index');
}




function DeleteConfirm(productSellInfoId) {
    var ans = confirm('are you sure you want to delete this record???');
    if (ans) {
        $.post('/ProductSellCtrl/Delete/' + productSellInfoId, {}, function (data) {
            alert(data);
            window.location.replace('/ProductSellCtrl/Index');
        })
    } else {
        window.location.replace('/ProductSellCtrl/Index');
    }


}


function PostNewProductSell() {
       
    var frmData = {
                            ProductId: $("#ProductId").val(),
                              BrandId: $("#BrandId").val(),
                    ProductCategoryId: $("#ProductCategoryId").val(),
                          ProductSize: $("#ProductSize").val(),
                            UnitPrice: $("#UnitPrice").val(),
                             Quantity: $("#Quantity").val(),
                             Discount: $("#Discount").val(),
                             Subtotal: $("#Subtotal").val(),
                           DateOfSell: $("#DateOfSell").val(),
                           GrandTotal: $("#GrandTotal").val(),
                       CurrentPayment: $("#CurrentPayment").val(),
                            DueAmount: $("#DueAmount").val(),
                           CustomerId: $("#CustomerId").val(),
                           EmployeeId: $("#EmployeeId").val(),
                  }

    console.log(frmData);

    $.ajax({
        url:'/ProductSellCtrl/Create',
        type:'POST',
        data:JSON.stringify(frmData),
        dataType:'JSON',
        contentType:'application/json',
        processData: false,
        success: function (data) {
            ResetForm();
            alert(data);
            $("#ProductSellModal").modal('hide');
            window.location.replace('/ProductSellCtrl/Index');
        },
        error: function (getError) {
            ResetForm();
            alert(getError.responseText);
            $("#ProductSellModal").modal('hide');
            window.location.replace('/ProductSellCtrl/Index');
        }
    })

}




var GetEditProductSellInfo_Id = 0;
function GetEditData(productSellInfoId) {
    $.get('/ProductSellCtrl/Details/' + productSellInfoId, {}, function (data) {
        console.log(data);
        ResetForm();

        $("#ProductId").html('<option value="' + data.productId + '" selected>' + data.productName + '</option>');

        $("#BrandId").html('<option value="' + data.brandId + '" selected>' + data.brandName + '</option>');

        $("#ProductCategoryId").html('<option value="' + data.productCategoryId + '" selected>' + data.productCategoryName + '</option>');


        //$("#ProductId").val(data.productId);
        //$("#BrandId").val(data.brandId);
        //$("#ProductCategoryId").val(data.productCategoryId);


        $("#ProductSize").val(data.productSize);
        $("#UnitPrice").val(data.unitPrice);
        $("#Quantity").val(data.quantity);
        $("#Discount").val(data.discount);
        $("#Subtotal").val(data.subtotal);
        $("#DateOfSell").val(data.dateOfSell);
        $("#GrandTotal").val(data.grandTotal);
        $("#CurrentPayment").val(data.currentPayment);
        $("#DueAmount").val(data.dueAmount);
        $("#CustomerId").html('<option value="' + data.customerId + '" selected>' + data.customerName + '</option>');
        $("#EmployeeId").val(data.employeeId);

        GetEditProductSellInfo_Id = data.productSellInfoId;

        $("#ModalHeadLine").empty();
        $("#ModalHeadLine").html("Edit Sell Info");
        $("#btnSave").hide();
        $("#btnEditPost").show();
        $("#ProductSellModal").modal('show');
        LoadProductDropdown();

    })
}
function PostUpdateProductSellInfo() {
    var frmData = {
        ProductSellInfoId: GetEditProductSellInfo_Id,
        ProductId: $("#ProductId").val(),
        BrandId: $("#BrandId").val(),
        ProductCategoryId: $("#ProductCategoryId").val(),
        ProductSize: $("#ProductSize").val(),
        UnitPrice: $("#UnitPrice").val(),
        Quantity: $("#Quantity").val(),
        Discount: $("#Discount").val(),
        Subtotal: $("#Subtotal").val(),
        DateOfSell: $("#DateOfSell").val(),
        GrandTotal: $("#GrandTotal").val(),
        CurrentPayment: $("#CurrentPayment").val(),
        DueAmount: $("#DueAmount").val(),
        CustomerId: $("#CustomerId").val(),
        EmployeeId: $("#EmployeeId").val(),
    }

    console.log(frmData);

    $.ajax({
        url: '/ProductSellCtrl/Edit_ProductSellInfo',
        type: 'POST',
        data: JSON.stringify(frmData),
        dataType: 'JSON',
        contentType: 'application/json',
        processData:false,
        success: function (data) {
            alert(data);
            ResetForm();
            $("#ProductSellModal").modal('hide');
            window.location.replace("/ProductSellCtrl/Index");
        },
        error: function (getError) {
            alert("Error has found please try again later!!");
            ResetForm();
            $("#ProductSellModal").modal('hide');
        }
    })

}

function SearchProductSellInfo() {
    var SearchParameter = {
        EmployeeMobileNo: $("#SearchText").val(),
        CustomerMobile: $("#SearchText").val()
    };
    $.ajax({
        url: '/ProductSellCtrl/SearchProductSellDetails',
        type: 'POST',
        data: JSON.stringify(SearchParameter),
        dataType: 'JSON',
        contentType: 'application/json',
        processData: false,
        success: function (data) {
            console.log(data);
           
            var SearchHtml = "";     

            $.each(data, function (key, item) {
                SearchHtml += '<tr><td>' + item.productName + '</td><td>' + item.brandName + '</td><td>' + item.productCategoryName + '</td><td>' + item.productSize + '</td><td>' + item.unitPrice + '</td> ' +
                    '<td>' + item.quantity + '</td><td>' + item.discount + '</td><td>' + item.dateOfSell + '</td><td>' + item.subtotal + '</td> ' +
                    '<td>' + item.grandTotal + '</td><td>' + item.currentPayment + '</td><td>' + item.dueAmount + '</td><td>' + item.customerName + '</td><td>' + item.customerMobile + '</td><td>' + item.customerAddress + '</td><td>' + item.employeeName + '</td> <td>' + item.employeeMobileNo + '</td><td>' + item.employeeAddress + '</td>' +
                    '<td><button type="button" class="btn btn-warning" onclick="GetEditData(' + item.productSellInfoId + ')">Edit</button>| ' +
                    '<button type="button" class="btn btn-danger" onclick="DeleteConfirm(' + item.productSellInfoId + ')">Delete</button></td></tr> ';

            });
            if (data.length <= 0) {
                alert("sorry no data has found!!");
                $("#div_productSellDetails_Display").hide();
                $("#div_Search").hide();
            } else {
                $("#tblProductSellData").empty();
                $("#div_productSellDetails_Display").hide();
                $("#Search_tblSellDetail").find("thead").addClass("thead-light");
                $("#div_Search").show();
                $("#DisplaySearchResult").html(SearchHtml);
            }
            
        },
        error: function (getError) {
            alert(getError.responseText);
            window.location.replace('/ProductSellCtrl/Index');
        }
    })
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