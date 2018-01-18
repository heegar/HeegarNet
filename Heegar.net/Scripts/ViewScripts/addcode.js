$('#btnTicketFilter').click(function () {
    $.ajax({
        url: '/Home/FilterCode',
        contentType: 'application/html; charset=utf-8',
        type: 'GET',
        dataType: 'html'

    })
    .success(function (result) {
        $('#divTicketList').html(result);
    })
    .error(function (xhr, status) {
        alert(status);
    })
    // Return false will prevent the button default action
    // from overriding the submit request.  this is required
    // on FireFox and IE, but not Chrome.
    return false;
});

$('#ddlCodeTypes').on('change', (function () {
    $("#disablingDiv").css({ height: $(document).height() });
    $("#disablingDiv").css({ display: "block" });
    $('#ddlEmployees').attr('disabled', true);
    $('#ddlEmployees').css("background-color", "#ccc");
    $.ajax({
        url: '/Home/FilterCode',
        contentType: 'application/html; charset=utf-8',
        data: { codeTypeID: this.value },
        type: 'GET',
        dataType: 'html'

    })
    .success(function (result) {
        $("#disablingDiv").css({ display: "none" });
        $('#ddlCodeTypes').attr('disabled', false);
        $('#ddlCodeTypes').css("background-color", "");ww
        $('#divTicketList').html(result);
    })
    .error(function (xhr, status) {
        alert(status);
    })
    // Return false will prevent the button default action
    // from overriding the submit request.  this is required
    // on FireFox and IE, but not Chrome.
    return false;
}));

function ToggleTicketDetails(detailsId, divSender) {
    //alert(divSender);
    var element = $('#' + detailsId);
    //alert(element);
    if (element.hasClass("TicketDetailsOff")) { // Show the details and change button to collapse
        element.removeClass("TicketDetailsOff").addClass("TicketDetailsOn");
        $(divSender).removeClass("DivAsExpand").addClass("DivAsCollapse");
    }
    else { // Remove details and change button to expand
        element.removeClass("TicketDetailsOn").addClass("TicketDetailsOff");
        $(divSender).removeClass("DivAsCollapse").addClass("DivAsExpand");

    }
}