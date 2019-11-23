
$(document).on("click", '.like-2', function () {
    event.preventDefault();
    debugger;
    const data = $(this).attr('data-barId');
    const data2 = $(this).attr('data-barReviewID');
    const data3 = $(this).attr('data-name');

    const url2 = "/Reviews/BarReviews/LikeBarReview?barReviewID=" + data2 + "&barId=" + data;

    const a = $(this);
    debugger;
    $.ajax({
        url: "https://localhost:44344/Reviews/BarReviews/LikeBarReview",
        data: { 'barId': data, 'barReviewID': data2, 'data-name': data3 },
        type: "post",
        cache: false,
        success: function (result) {

            a.html('<i class="fa fa-heart fa-lg text-danger"></i > <span>' + result + '</span>').removeClass("like-2").addClass("unlike-2");

        }, 
        fail: function (xhr, textStatus, errorThrown) {
            alert('request failed');
            window.location = "/Error/CustomError";
        }

    })


});

$(document).on("click", '.unlike-2', function () {
    event.preventDefault();
    debugger;
    const data = $(this).attr('data-barId');
    const data2 = $(this).attr('data-barReviewID');
    debugger;
    const url2 = "/Reviews/BarReviews/RemoveLikeBarReview?barReviewID=" + data2 + "&barId=" + data;
    const a = $(this);
    debugger;
    $.ajax({
        url: url2,
        data: { 'barId': data, 'barReviewID': data2 },
        type: "post",
        cache: false,
        success: function (result) {

            a.html('<i class="fa fa-heart fa-lg text-danger"></i > <span>' + result + '</span>').removeClass("unlike-2").addClass("like-2");

        },
        fail: function (xhr, textStatus, errorThrown) {
            alert('request failed');
            window.location = "/Error/CustomError";
        }
    })

});



    //const data4 = $('.data-count').attr('data-count').val;

    // console.log(data4);

    //const url2 = $(this).attr('action');
    //const url2 = "/Reviews/BarReviews/LikeBarReview/";
    //console.log(url2)
//    $.ajax({
//        url: "https://localhost:44344/Reviews/BarReviews/RemoveLikeBarReview",
//        data: { 'barId': data, 'barReviewID': data2, 'data-name': data3 },
//        type: "post",
//        cache: false,
//        success: function (result) {
//            $("#unlikeSpan").html(result);
//        }
//    })
//});