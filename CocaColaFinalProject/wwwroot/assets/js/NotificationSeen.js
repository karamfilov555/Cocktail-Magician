$('btn btn-default btn-sm notificationBtn').on('click', function (e) {
    e.preventDefault();

            console.log("kliknah");
            e.preventDefault();
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