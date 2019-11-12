$('btn btn-default btn-sm notificationBtn').on('click', function (e) {
    e.preventDefault();

            console.log("kliknah");
    const thisForm = $(this).closest('form');

    $.post(thisForm.attr('action'), {}, response => thisForm.closest('tr').replaceWith(response));

            let notificationsElem = document.getElementById("notificationsCount");

            console.log(notificationsElem);
            $.ajax({
                url: "https://localhost:44344/Notifications/Notifications/GetNotificationsCount",
                data: {},
                type: "get",
                cache: false,
                success: function (result) {
                    console.log(result);
                    notificationsElem.text.replaceWith(result)
                }
            })
    //TODO !!
});

//$('#notificationBtn').on('click', function (e) {
//    e.preventDefault();

//});