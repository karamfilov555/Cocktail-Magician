//$('btn btn-default btn-sm notificationBtn').on('click', function (e) {
//    e.preventDefault();

//            console.log("kliknah");
//    const thisForm = $(this).closest('form');

//    $.post(thisForm.attr('action'), {}, response => thisForm.closest('tr').replaceWith(response));

//            let notificationsElem = document.getElementById("notificationsCount");

//            console.log(notificationsElem);
//            $.ajax({
//                url: "https://localhost:44344/Notifications/Notifications/GetNotificationsCount",
//                data: {},
//                type: "get",
//                cache: false,
//                success: function (result) {
//                    console.log(result);
//                    notificationsElem.text.replaceWith(result)
//                }
//            })
//    //TODO !!
//});

$('.notificationBtn').on('click', function (e) {
    e.preventDefault();

    const thisForm = $(this).closest('form');

    $.post(thisForm.attr('action'), {}, response => thisForm.closest('tr').replaceWith(response));

    let notificationsElem = document.getElementById("notificationsCount");
    let notificationsBell = document.getElementById("bellNot");

    $.ajax({
        url: "https://localhost:44344/Notifications/Notifications/GetNotificationsCount",
        data: {},
        type: "get",
        cache: false,
        success: function (result) {
            if (Number(result) - 1 === 0) {
                var bell = '<div class="buttonNot"><a asp-area="Notifications" asp-controller="Notifications" asp-action="Index"><i class="fa fa-bell fa-lg" style="color:white;"></i></a></div>'
                notificationsBell.innerHTML = bell;
            }
            else {
                var spanElement = '<span id="notificationsCount class=" button__badge"><font style="padding-right:1px">' + (Number(result) - 1) + '</font></span > ';
                notificationsElem.innerHTML = spanElement;
            }
        }
    })
});

//$('#notificationBtn').on('click', function (e) {
//    e.preventDefault();

//});