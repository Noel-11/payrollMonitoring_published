
function clickEnterSearch(ctrl) {
    if (event.keyCode === 13) {
        //event.preventDefault();
        //alert(ctrl + "asdad");
        //document.getElementById(ctrl).click();
        var button = document.getElementById(ctrl);

        button.click();

        return false
    }
};


function myNotification(infoText) {
    //var myDiv = document.getElementById("infoContent");
    var notification = document.getElementById("notification");
    var autoHideTime = 5000; // Time in milliseconds (5 seconds)
    //myDiv.textContent = infoText;
    setTimeout(function () {
        notification.style.display = "block"
        notification.style.opacity = "1"; // Show notification
        setTimeout(function () {
            notification.style.display = "none"
            notification.style.opacity = "0"; // Hide notification
        }, autoHideTime);
    }, 900); // Delay before showing the notification (1 second)
    return false


}







