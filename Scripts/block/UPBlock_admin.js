
//$(document).ready(function () {
Sys.WebForms.PageRequestManager.getInstance().add_initializeRequest(OnBeginRequest);
Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endRequest);

//return false;

//});

function showLoader() {
    const loader = document.getElementById('domMessage');
    loader.style.display = 'block';
}

function hideLoader() {
    const loader = document.getElementById('domMessage');
    loader.style.display = 'none';
}

function OnBeginRequest(sender, args) {
    //$.blockUI({ message: $('#domMessage') });
    showLoader();
}
function endRequest(sender, args) {
    // $.unblockUI();
    hideLoader();
}


