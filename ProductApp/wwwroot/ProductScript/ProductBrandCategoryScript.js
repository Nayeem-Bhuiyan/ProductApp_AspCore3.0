$(document).ready(function () {

    $("#btnShowModal").click(function () {
        $("#btnSave").show();
        $("#btnEditPost").hide();
        $("#ProductBrandModal").modal('show');
    });


});


function DeleteProductRecord(ProductId) {
    var ans = confirm("Are you sure you want to delete this!!");
    if (ans) {
        $.post('/ProductCtrl/Delete/' + ProductId, {}, function(data) {
            alert(data);
        })
    }
}



function AddProductBrandCategory() {
    
    if (window.FormData !== undefined) {
      
        var frmData = new FormData($('#frmProductBrandCategory')[0]);
        //var file = $("#frmProductBrandCategory").find('input:file[name="ProductImage"]')[0];
        var UploadedFile = $("#ProductImage")[0].files[0];

        frmData.append('ProductImage', UploadedFile);
        frmData.append('BrandName', $("#BrandName").val());
        frmData.append('ProductCategoryName', $("#ProductCategoryName").val());
        frmData.append('ProductName', $("#ProductName").val());



        console.log(frmData);

        $.ajax({
            type: "POST",
            url: "/ProductCtrl/CreateProduct",
            data: frmData,
            processData: false,
            contentType: false,
            cache: false,
            success: function (data) {
                window.location.href = "/ProductCtrl/IndexDisplay";
                ResetForm();
                $("#ProductBrandModal").modal("hide");
                alert(data);
                
            },
            error: function (getError) {
                alert(getError.responseText);
                ResetForm();
                $("#ProductBrandModal").modal("hide");
            }
            });





         
    };
    
    
};


function LoadBrandList() {
    $.ajax({
        type: "GET",
        url: "/ProductCtrl/GetEditProductBrandCategory/" + ProductId,
        data: frmData,
        processData: false,
        contentType: false,
        cache: false,
        success: function (data) {

            var option = "";

            $.each(data, function (index,item) {
                option += '<option value="0">' + item.brandId+'</option>';
            });
        },
        error: function (getError) {
            alert(getError.responseText);
        }
    });
}

//function GetEditData(ProductId) {

    

//    $.ajax({
//        type: "GET",
//        url: "/ProductCtrl/GetEditProductBrandCategory/" + ProductId,
//        data: frmData,
//        processData: false,
//        contentType: false,
//        cache: false,
//        success: function (data) {



//            $("#EditProductBrandModal").modal("show");
//        },
//        error: function (getError) {
//            alert(getError.responseText);
//        }
//    });


//}



function EditPost_ProductBrandCategory() {
    if (window.FormData !== undefined) {

        var frmData = new FormData($('#frmProductBrandCategory')[0]);
        //var file = $("#frmProductBrandCategory").find('input:file[name="ProductImage"]')[0];
        var UploadedFile = $("#ProductPicturePath")[0].files[0];

        frmData.append('ProductImage', UploadedFile);
        frmData.append('BrandName', $("#BrandName").val());
        frmData.append('ProductCategoryName', $("#ProductCategoryName").val());
        frmData.append('ProductName', $("#ProductName").val());

        console.log(frmData);

        $.ajax({
            type: "POST",
            url: "/ProductCtrl/CreateProduct",
            data: frmData,
            processData: false,
            contentType: false,
            cache: false,
            success: function (data) {
                alert(data);
                ResetForm();
                $("#ProductBrandModal").modal("hide");
                window.location.replace("/ProductCtrl/IndexDisplay")
            },
            error: function (getError) {
                alert(getError.responseText);
            }
        });






    };

}


function ResetForm() {
        $("#BrandName").val(''),
        $("#ProductCategoryName").val(''),
        $("#ProductName").val(''),
        $("#ProductPicturePath").val('')
}




