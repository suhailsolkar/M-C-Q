//$.session.set('username', 'test');
debugger;
var animate = ".admin-menu-content";
var speed = 400;
$(document).ready(function () {

    //Pagination(1);

    $(".right-arrow").show();
    $(".left-arrow").hide();
    $(".nav-text").hide();
    $(".user-name").hide();
    $(".sidebar").animate({
        width: '83'
    });
    $(animate).animate({ "width": "+=163px" }, 0);
    //$(animate).width('+=140px');
    //$('#hdnView').val('Minimize');

    /* ===== Side Menu (Start) ===== */

    $(".sidebar").hover(function () {
        $(".right-arrow").hide();
        $(".left-arrow").show();
        $(".nav-text").show();
        $(".user-name").show();
        $(".sidebar").animate({

            width: '250'
        });
        $(animate).animate({ "width": "-=163px" }, speed);
    }, function () {
        $(".right-arrow").show();
        $(".left-arrow").hide();
        $(".nav-text").hide();
        $(".user-name").hide();
        $(".sidebar").animate({
            width: '83'
        });
        $(animate).animate({ "width": "+=163px" }, speed);
    });

    ManageUsersValidations();

    ManageSubjectsValidations();

    //$(".txtOption").focus(function () {
    //    $(".lblOption").css("border-bottom", "1px solid red");
    //});
});


function ManageUsersValidations() {
    $(".txtEmailID").change(function () {
        debugger;
        var inputvalues = $(this).val();
        var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
        if (!regex.test(inputvalues)) {
            $(".txtEmailID").val('');
            //$('.warEmailID').show();
            alert("Invalid EmailID");
            //return regex.test(inputvalues);
        }
    });

    $('.txtEmailID').on('input', function (e) {
        $('.warEmailID').hide();
    });
    $('.txtFirstname').on('input', function (e) {
        $('.warFirstName').hide();
    });
    $('.txtEmailID').on('input', function (e) {
        $('.warEmailID').hide();
    });
    $('.txtDOB').on('input', function (e) {
        $('.warDOB').hide();
    });
    $('.txtPhoneno').on('input', function (e) {
        $('.warPhoneno').hide();
    });
    $('.ddlCity').change(function () {
        $('.warCity').hide();
    });
    $('.ddlGender').change(function () {
        $('.warGender').hide();
    });
    $('.ddlRole').change(function () {
        $('.warRole').hide();
    });
}

function ManageSubjectsValidations() {

    $('.txtSubejctName').on('input', function (e) {
        $('.warSubejctName').hide();
    });

    $('.ddlSubjectTeacher').change(function () {
        $('.warSubjectTeacher').hide();
    });

    $('.txtNoofquestions').on('input', function (e) {
        $('.warNoofQuestions').hide();

        if (parseInt($('.txtNoofquestions').val()) > 50) {
            $('.txtNoofquestions').val('');
            $('.txtTotakmarks').val('');
            alert('No of questions should be less then 50');
        }
        else if ($('.txtNoofquestions').val() == "") {
            $('.txtTotakmarks').val('');
        }
        else {

            $('.txtTotakmarks').val(parseInt($('.txtNoofquestions').val()) * 2);
        }
    });

    $('.txtTotakmarks').on('input', function (e) {
        $('.warTotalMarks').hide();
    });

    $('.txtSubjectDesc').on('input', function (e) {
        $('.warSubjectDesc').hide();
    });
}
