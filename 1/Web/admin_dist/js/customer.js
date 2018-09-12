var ref;
ref = $("#myTable").DataTable({ "scrollX": true });
$(document).ready(function () {
    loaddatabyHung();

});
// load data by Hung
function loaddatabyHung() {
    ref.clear().draw();
    var html = '';
    $.ajax({
        url: "/Base/GetAllProduct",
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            $.each(result, function (key, item) {
                html = '<a href="#" onclick="showcontent(' + item.CategoryID + ');"> Chi tiết </a> | <a href="#" onclick="return getbyID(' + item.CategoryID + ');"> Chỉnh sửa</a> | <a href="#" onclick="Delete(' + item.CategoryID + ');">Xóa</a>';
                ref.row.add([

                    item.Id,
                    item.Name,
                    item.MadeFrom,
                    item.CategoryId,
                    item.Dimenson,
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
// Show content popup hiển thị chi tiết nội dung category đó
function showcontent(id) {
    $.ajax({
        url: "/Base/GetProductById/" + id,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.Remark) {
                $('#modalshowcontent_body').html(result.Remark);
            }
            else {
                $('#modalshowcontent_body').html('Không có mô tả');
            }
            $('#modalshowcontent').modal('show');
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
// function lấy dữ liệu theo category ID để chỉnh sửa
function getbyID(ID) {
    $('#CategoryID').css('border-color', 'lightgrey');
    $('#Level').css('border-color', 'lightgrey');
    $('#Level').attr('disabled', 'disabled');
    $('#CategoryName').css('border-color', 'lightgrey');
    $('#ParentCategoryId').css('border-color', 'lightgrey');
    $('#ParentCategoryId').attr('disabled', 'disabled');
    $('#Description').css('border-color', 'lightgrey');
    $('#Remarks').css('border-color', 'lightgrey');
    $('#btnCreatenewlevel').attr('disabled', 'disabled');

    $.ajax({
        url: "/Home/GetByID/" + ID,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            $('#CategoryID').val(result.CategoryID);

            $('#Level').find('option').remove().end();
            $('#Level').append('<option value="' + result.Level + '">' + result.Level + '</option>');
            $('#CategoryName').val(result.CategoryName);
            $('#ParentCategoryId').find('option').remove().end();
            $('#ParentCategoryId').append('<option value="' + result.ParentCategoryID + '">' + result.ParentCategoryID + '</option>');
            $('#Description').val(result.Description);
            CKEDITOR.instances['Remarks'].setData(result.Remarks);

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
    if ($('#Level option:selected').val().trim() === "") {
        $('#Level').css('border-color', 'red');
        isvalidate = false;
    }
    else {
        $('#Level').css('border-color', 'lightgrey');
    }

    if ($('#CategoryName').val().trim() === "") {
        $('#CategoryName').css('border-color', 'red');
        isvalidate = false;
    }
    else {
        $('#CategoryName').css('border-color', 'lightgrey');
    }

    if ($('#ParentCategoryId option:selected').val().trim() === "") {
        $('#ParentCategoryId').css('border-color', 'red');
        isvalidate = false;
    }
    else {
        $('#ParentCategoryId').css('border-color', 'lightgrey');
    }

    if ($('#Description').val().trim() === "") {
        $('#Description').css('border-color', 'red');
        isvalidate = false;
    }
    else {
        $('#Description').css('border-color', 'lightgrey');
    }
    return isvalidate;
}

//function load level cho dropdown level
function loaddropdownlevel() {
    var html = '<option value="0"> All Level </option>';
    var html1 = '<option disabled selected value> -- select an option -- </option>';
    $.ajax({
        url: "/Home/GetListLevel",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            $.each(result, function (index, value) {

                html += '<option value="' + value + '">' + value + '</option>';
                html1 += '<option value="' + value + '">' + value + '</option>';
            });
            $("#levelfilter").html(html); // load tại filter lọc
            $("#Level").html(html1); //load cho trong modal
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
// Script cho phan Add: them moi Content
function Add() {
    //alert(CKEDITOR.instances['Remarks'].getData());
    var date = new Date();
    var res = validate();
    if (res == false) {
        return false;
    }
    var category =
    {
        CategoryID: $('#CategoryID').val(),
        Level: $('#Level').val(),
        CategoryName: $('#CategoryName').val(),
        ParentCategoryID: $('#ParentCategoryId').val(),
        EditDate: date.toLocaleDateString(),
        Editor: 'thuan',
        Description: $('#Description').val(),
        Remarks: CKEDITOR.instances['Remarks'].getData()
    };
    //alert(JSON.stringify(category));
    $.ajax({
        url: "/Home/Add",
        data: JSON.stringify(category),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        datatype: "json",
        success: function (result) {
            bootbox.alert('Add new category success!');
            //loaddatabyHung();
            loaddatabylevel($('#Level option:selected').val());
            loaddropdownlevel();
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

    var date = new Date();
    var res = validate();
    if (res == false) {
        return false;
    }
    var category =
    {
        CategoryID: $('#CategoryID').val(),
        Level: $('#Level').val(),
        CategoryName: $('#CategoryName').val(),
        ParentCategoryID: $('#ParentCategoryId').val(),
        EditDate: date.toLocaleDateString(),
        Editor: 'thuan',
        Description: $('#Description').val(),
        Remarks: CKEDITOR.instances['Remarks'].getData()
    };
    $.ajax({
        url: "/Home/Update",
        data: JSON.stringify(category),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        datatype: "json",
        success: function (result) {
            //loaddatabyHung();
            var c = $('#Level option:selected').val();
            loaddatabylevel(c);
            loaddropdownlevel();
            $('#myModal').modal('hide');

            bootbox.alert('Update success !');
        },
        error: function (errormessage) {
            bootbox.alert(errormessage.responseText);
        }
    });

}

// function delete ID
function Delete(ID) {
    bootbox.confirm({
        title: "Delete Category?",
        message: "Do you want to delete this category?",
        buttons: {
            cancel: {
                label: '<i class="glyphicon glyphicon-remove"></i> Cancel'
            },
            confirm: {
                label: '<i class="glyphicon glyphicon-ok"></i> Confirm'
            }
        },
        callback: function (a) {
            if (a) {
                $.ajax({
                    url: "/Home/Delete/" + ID,
                    type: "POST",
                    contentType: "application/json;charset=UTF-8",
                    dataType: "json",
                    success: function (result) {
                        if (result == -1) {
                            bootbox.alert("You cannot this category. You must delete all child of it");

                        }
                        else {
                            //loaddatabyHung();   
                            var b = $('#levelfilter option:selected').val();
                            // load lại tại đúng level đó
                            loaddatabylevel(b);
                            // load lại level ở trang
                            loaddropdownlevel();
                            bootbox.alert("Delete Successful!");
                        }
                        //loaddropdownparentcatgory();
                    },
                    error: function (errormessage) {
                        alert(errormessage.responseText);
                    }
                });
            }
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


