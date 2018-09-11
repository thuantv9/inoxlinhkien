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
        url: "/Base/GetAllProduct",
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            $.each(result, function (key, item) {
                html = '<a href="#" onclick="showcontent(' + item.CategoryID + ');"> Details </a> | <a href="#" onclick="return getbyID(' + item.CategoryID + ');"> Edit</a> | <a href="#" onclick="Delete(' + item.CategoryID + ');">Delete</a>';
                ref.row.add([
                    item.Id,
                    item.Name,
                    item.MadeFrom,
                    item.CategoryId,
                    item.Dimenson,
                    item.Image,                   
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