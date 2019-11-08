
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
  
    //const url2 = $(this).attr('action');
    //const url2 = "/Reviews/BarReviews/LikeBarReview/";
    //console.log(url2)
    $.ajax({
        url: "https://localhost:44344/Reviews/BarReviews/LikeBarReview",
        data: { 'barId': data, 'barReviewID': data2, 'data-name': data3 },
        type: "post",
        cache: false,
        success: function (result) {
            $("#likeSpan").html(result); 
        }
    })
});


$('.unlike').on('click', function (event) {
    event.preventDefault();
    const data = $(this).attr('data-barId');
    const data2 = $(this).attr('data-barReviewID');
    const data3 = $(this).attr('data-name');

    //console.log(($("#countID").attr("data-count")));
    //const new2 = $('#countID').attr('data-count').val;
    //console.log(new2);

    //const data4 = $('.data-count').attr('data-count').val;

    // console.log(data4);

    //const url2 = $(this).attr('action');
    //const url2 = "/Reviews/BarReviews/LikeBarReview/";
    //console.log(url2)
    $.ajax({
        url: "https://localhost:44344/Reviews/BarReviews/RemoveLikeBarReview",
        data: { 'barId': data, 'barReviewID': data2, 'data-name': data3 },
        type: "post",
        cache: false,
        success: function (result) {
            $("#unlikeSpan").html(result);
        }
    })
});