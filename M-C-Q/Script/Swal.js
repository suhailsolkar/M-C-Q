////debugger;
////function swal(Status, Message, Url) {

////    Swal.fire({
////        title: (Status),
////        text: (Message),
////        icon: (Status),
////        confirmButtonText: 'Okay',
////        //confirmButtonColor: '#97144d',
////        //showCancelButton: true,
////        //cancelButtonText: 'Custom Cancel',
////        //customClass: {
////        //    confirmButton: customButtonClass,
////        //    cancelButton: 'custom-cancel-button-class'
////        //}
////    }).then(function () {
////        if (Url != "") {
////            window.location.href = Url;
////        }
////    });
////    $('.nav-links').css("background-color", "purple");
////}

debugger;
function swal(Title, Message, Status, Url) {

    Swal.fire({
        title: (Title),
        text: (Message),
        icon: (Status),
        confirmButtonText: 'Okay',
        //confirmButtonColor: '#97144d',
        //showCancelButton: true,
        //cancelButtonText: 'Custom Cancel',
        //customClass: {
        //    confirmButton: customButtonClass,
        //    cancelButton: 'custom-cancel-button-class'
        //}
    }).then(function () {
        if (Url != "") {
            window.location.href = Url;
        }
    });
    $('.nav-links').css("background-color", "purple");
}
