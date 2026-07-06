
$(document).ready(function () {
    Sys.WebForms.PageRequestManager.getInstance().add_initializeRequest(OnBeginRequest);
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endRequest);
});

function showBlock() {
    //$.ajax({ url: 'wait.php', cache: false });

    var myModal = new bootstrap.Modal(document.getElementById('domMessage'), {}); myModal.show();
}

function OnBeginRequest(sender, args) {
    $.blockUI({
        message: $('#domMessage')
    });
    //showBlock();
}
function endRequest(sender, args) {
    $.unblockUI();
}


