var ref;
ref = $("#myTable").DataTable({ "scrollX": true });
$(document).ready(function () {
    loaddata();
});
// load data by Hùng
function loaddata() {
    ref.clear().draw();
    var html = '';
    $.ajax({
        url: "/Base/GetAllCategory",
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            $.each(result, function (key, item) {
                html = '<a href="#" onclick="return getbyID(' + item.CategoryID + ');"> Edit</a> | <a href="#" onclick="Delete(' + item.CategoryID + ');">Delete</a>';
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