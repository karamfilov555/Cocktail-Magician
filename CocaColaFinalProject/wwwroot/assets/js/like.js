
$('.like-2').on('click', function (event) {
    event.preventDefault();
    const data = $(this).attr('data-barId');
    const data2 = $(this).attr('data-barReviewID');
    const data3 = $(this).attr('data-name');
    //console.log(($("#countID").attr("data-count")));
    //const new2 = $('#countID').attr('data-count').val;
    //console.log(new2);

    //const data4 = $('.data-count').attr('data-count').val;

   // console.log(data4);
    debugger;
    const url2 = $(this).attr('action');
    //const url2 = "/Reviews/BarReviews/LikeBarReview/";
    debugger;
    $.ajax({
        url: url2,
        data: { 'barId': data, 'barReviewID': data2, 'data-name': data3 },
        type: "post",
        cache: false,
        success: function (result) {
            $(this).html('<i class="fa fa-heart fa-lg text-danger"></i> (' + result + ')').removeClass("like").addClass("unlike")
            //a.html(('<button id="countID" type="submit" title = "Love it" class= "btn-like btn-counter" data - count="@review.LikeCount">></button> )
            //$("#countID").attr("data-count").replace(result);
            console.log('result');
        }
    })
    
});



    //$.post(url, data, data2, data3, function (serverData) {
    //    toastr.success('Match created');
    //}).fail(function (error) {
    //    toastr.error(error.responseText);
    //});
























//$(document).ready(function () {
//    //LIKE
//    $("button.like").click(function () {
//        var barReviewId = $(this).data("barReviewID");
//        var barId = $(this).data("barId");
//        var name = $(this).data("name");
//        $.get('/Search/SearchResults?title=' + searchText +
//            '&author=' + searchAuthor +
//            '&year=' + searchYear +
//            '&category=' + searchCategory +
//            '&inclusive=' + searchInclusive, serverResponseHandler);

//        var link = "/Article/LikeThis/" + id;
//        var a = $(this);
//        $.ajax({
//            type: "GET",
//            url: link,
//            success: function (result) {
//                a.html('<i class="fa fa-heart fa-lg text-danger"></i> (' + result + ')').removeClass("like").addClass("unlike");
//            }
//        });
//    });
//    //UNLIKE
//    $("button.unlike").click(function () {
//        var id = $(this).data("id");
//        var link = "/Article/UnlikeThis/" + id;
//        var a = $(this);
//        $.ajax({
//            type: "GET",
//            url: link,
//            success: function (result) {
//                a.html('<i class="fa fa-heart fa-lg text-danger"></i> (' + result + ')');
//            }
//        });
//    });