function AddFeature() {
    debugger;
    var detail = JSON.stringify({
        FeatureID: $("#FeatureId").val(),

        FeatureTitle: $("#FeatureId :selected").text(),


        FeatureValue: $("#txtValue").val()
    });
    var fd = new FormData();
    fd.append("FeatureDetails", detail);
    $.ajax({
        url: "/admin/Products/AddFeatures",
        type: "Post",
        contentType: false,
        processData: false,
        data: fd,

    }).done(function (res) {
        $("#AddFeature").html(res);
        $("#txtValue").val("");
    });
}
function RemoveFeature(id, val) {
    debugger;
    var detail = JSON.stringify({
        FeatureID:id,
        FeatureValue:val
    });
    var fd = new FormData();
    fd.append("FeatureDetails", detail);
    $.ajax({
        url: "/admin/Products/RemoveFeatures",
        type: "Post",
        contentType: false,
        processData: false,
        data: fd,

    }).done(function (res) {
        $("#AddFeature").html(res);
       
    });
}