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
    $('#Id').css('border-color', 'lightgrey');
    $('#Name').css('border-color', 'lightgrey');    
    $('#MadeFrom').css('border-color', 'lightgrey');
    $('#CategoryId').css('border-color', 'lightgrey');    
    $('#Dimenson').css('border-color', 'lightgrey');
    $('#Image').css('border-color', 'lightgrey');
    $('#Remark').css('border-color', 'lightgrey');
    //$('#btnCreatenewlevel').attr('disabled', 'disabled');

    $.ajax({
        url: "/Base/GetProductById/" + ID,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            $('#Id').val(result.Id);
            $('#Name').val(result.Name);
            $('#MadeFrom').val(result.MadeFrom);
            $('#Dimenson').val(result.Dimenson);
            $('#Image').val(result.Image);
            CKEDITOR.instances['Remark'].setData(result.Remark);

            $('#myModalLabel').html('<span class="glyphicon glyphicon-envelope"></span> Chỉnh sửa sản phẩm');
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
    if ($('#Id').val().trim() === "") {
        $('#Id').css('border-color', 'red');
        isvalidate = false;
    }
    else {
        $('#Id').css('border-color', 'lightgrey');
    }

    if ($('#Name').val().trim() === "") {
        $('#Name').css('border-color', 'red');
        isvalidate = false;
    }
    else {
        $('#Name').css('border-color', 'lightgrey');
    }
    

    if ($('#MadeFrom').val().trim() === "") {
        $('#MadeFrom').css('border-color', 'red');
        isvalidate = false;
    }
    else {
        $('#MadeFrom').css('border-color', 'lightgrey');
    }

    if ($('#Dimeson').val().trim() === "") {
        $('#Dimeson').css('border-color', 'red');
        isvalidate = false;
    }
    else {
        $('#Dimeson').css('border-color', 'lightgrey');
    }

    if ($('#Image').val().trim() === "") {
        $('#Image').css('border-color', 'red');
        isvalidate = false;
    }
    else {
        $('#Image').css('border-color', 'lightgrey');
    }

    return isvalidate;
}


// Script cho phan Add: them moi Content
function Add() {
    //alert(CKEDITOR.instances['Remarks'].getData());  
    var res = validate();
    if (res == false) {
        return false;
    }
    var product =
    {
        Id: $('#Id').val(),
        Level: $('#Name').val(),
        MadeFrom: $('#MadeFrom').val(),
        CategoryId: $('#CategoryId').val(),       
        Dimenson: $('#Dimenson').val(),
        Image: $('#Image').val(),
        Remark: CKEDITOR.instances['Remark'].getData()
    };
    //alert(JSON.stringify(category));
    $.ajax({
        url: "/Base/InsertProduct",
        data: JSON.stringify(product),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        datatype: "json",
        success: function (result) {
            bootbox.alert('Thêm mới thành công!');
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
    var product =
    {
        Id: $('#Id').val(),
        Level: $('#Name').val(),
        MadeFrom: $('#MadeFrom').val(),
        CategoryId: $('#CategoryId').val(),
        Dimenson: $('#Dimenson').val(),
        Image: $('#Image').val(),
        Remark: CKEDITOR.instances['Remark'].getData()
    };
    //alert(JSON.stringify(category));
    $.ajax({
        url: "/Base/UpdateProduct",
        data: JSON.stringify(product),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        datatype: "json",
        success: function (result) {
            bootbox.alert('Update thành công!');
            loaddatabyHung();            
            $('#myModal').modal('hide');
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });

}
// function clear text box trong modal
function clearTextBox() {
    $('#Id').val("");
    $('#Name').val("");
    $('#MadeFrom').val("");
    $('#CategoryId').val("");
    $('#Dimenson').val("");
    $('#Image').val("");    
    //$('#Remarks').val("");
    CKEDITOR.instances['Remark'].setData("")

    $('#Id').removeAttr('disabled');
    $('#Name').removeAttr('disabled');
    $('#MadeFrom').removeAttr('disabled');
    $('#CategoryId').removeAttr('disabled');
    $('#Dimenson').removeAttr('disabled');
    $('#Image').removeAttr('disabled');
    $('#Remark').removeAttr('disabled');
    $('#btnCreatenewlevel').removeAttr('disabled');

    $('#btnUpdate').hide();
    $('#btnAdd').show();
    $('#Name').css('border-color', 'lightgrey');
    $('#CategoryId').css('border-color', 'lightgrey');
    $('#Dimenson').css('border-color', 'lightgrey');
    $('#Remark').css('border-color', 'lightgrey');

}
// mở popup khi bấm nút add category
function addpopup() {
    $('#myModalLabel').html('<h4><span class="glyphicon glyphicon-envelope"></span> Thêm mới sản phẩm</h4>');
    $('#myModal').modal('show');
    clearTextBox();    
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


// function delete ID
function Delete(ID) {
    bootbox.confirm({
        title: "Xóa?",
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
