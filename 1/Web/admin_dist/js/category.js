﻿var ref;
ref = $("#myTable").DataTable({ "scrollX": true });
$(document).ready(function () {
    loaddatabyHung();
});
// load data by Hung
function loaddatabyHung() {
    ref.clear().draw();
    var html = '';
    $.ajax({
        url: "/Base/GetAllCategory",
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            $.each(result, function (key, item) {
                html = '<a href="#" onclick="return getbyID(' + item.CategoryID + ');"> Chỉnh sửa</a> | <a href="#" onclick="Delete(' + item.CategoryID + ');">Xóa</a>';
                ref.row.add([

                    item.CategoryId,
                    item.CategoryName,                    
                    html
                ]);
            });
            ref.draw(false);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });

}
// function lấy dữ liệu theo category ID để chỉnh sửa
function getbyID(ID) {
    $('#CategoryId').css('border-color', 'lightgrey');   
    $('#CategoryName').css('border-color', 'lightgrey');    
    $.ajax({
        url: "/Base/GetCategoryById/" + ID,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            $('#CategoryID').val(result.CategoryId);           
            $('#CategoryName').val(result.CategoryName);           

            $('#myModalLabel').html('<span class="glyphicon glyphicon-envelope"></span> Edit Category');
            $('#myModal').modal('show');
            $('#btnUpdate').show();
            $('#btnAdd').hide();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

// function validate khi thêm mới hoặc chỉnh sửa
function validate() {

    var isvalidate = true;    
    if ($('#CategoryId option:selected').val().trim() === "") {
        $('#CategoryId').css('border-color', 'red');
        isvalidate = false;
    }
    else {
        $('#CategoryId').css('border-color', 'lightgrey');
    }
    if ($('#CategoryName').val().trim() === "") {
        $('#CategoryName').css('border-color', 'red');
        isvalidate = false;
    }
    else {
        $('#CategoryName').css('border-color', 'lightgrey');
    }    
    return isvalidate;
}
// Script cho phan Add: them moi Content
function Add() {    
    var res = validate();
    if (res == false) {
        return false;
    }
    var category =
    {
        CategoryId: $('#CategoryId').val(),        
        CategoryName: $('#CategoryName').val()       
    };
    //alert(JSON.stringify(category));
    $.ajax({
        url: "/Base/InsertCategory",
        data: JSON.stringify(category),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        datatype: "json",
        success: function (result) {
            bootbox.alert('Thêm mới chủng loại sản phẩm thành công!');
            loaddatabyHung();           
            //loaddropdownparentcatgory();
            $('#myModal').modal('hide');
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });

}

// fucntion Update dữ liệu
function Update() {  
    var res = validate();
    if (res == false) {
        return false;
    }
    var category =
    {
        CategoryId: $('#CategoryId').val(),
        CategoryName: $('#CategoryName').val()        
    };
    $.ajax({
        url: "/Base/UpdateCategory",
        data: JSON.stringify(category),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        datatype: "json",
        success: function (result) {
            //loaddatabyHung();            
            $('#myModal').modal('hide');
            bootbox.alert('Cập nhật thành công !');
        },
        error: function (errormessage) {
            bootbox.alert(errormessage.responseText);
        }
    });
}
// function clear text box trong modal
function clearTextBox() {
    $('#CategoryID').val("");
    $('#CategoryName').val("");
    $('#Level').val("");
    //$('#ParentCategoryId').val("");
    $('#ParentCategoryId').find('option').remove().end();
    $('#Description').val("");
    //$('#Remarks').val("");
    CKEDITOR.instances['Remarks'].setData("")

    $('#CategoryName').removeAttr('disabled');
    $('#Level').removeAttr('disabled');
    $('#ParentCategoryId').removeAttr('disabled');
    $('#Description').removeAttr('disabled');
    $('#Remarks').removeAttr('disabled');
    $('#btnCreatenewlevel').removeAttr('disabled');

    $('#btnUpdate').hide();
    $('#btnAdd').show();
    $('#CategoryName').css('border-color', 'lightgrey');
    $('#ParentCategoryID').css('border-color', 'lightgrey');
    $('#Description').css('border-color', 'lightgrey');
    $('#Remarks').css('border-color', 'lightgrey');

}
// mở popup khi bấm nút add category
function addpopup() {
    $('#myModalLabel').html('<h4><span class="glyphicon glyphicon-envelope"></span> Add New Category</h4>');
    $('#myModal').modal('show');
    clearTextBox();
    loaddropdownlevel();
    $('#btnUpdate').hide();
    $('#btnAdd').show();
    $.ajax({
        url: "/Home/GetNextCategoryID",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            $('#CategoryID').val(result);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}




