$(document).on("click", '.reviewLike-2', function () {
    event.preventDefault();
    debugger;
    const data = $(this).attr('data-cocktailReviewID');
    const data2 = $(this).attr('data-cocktailId');
    //const data3 = $(this).attr('data-name');
    debugger;
    const url2 = "/Reviews/CocktailReviews/LikeCocktailReview?cocktailReviewID=" + data + "&cocktailId=" + data2;

    const a = $(this);
    debugger;
    $.ajax({
        url: "https://localhost:44344/Reviews/CocktailReviews/LikeCocktailReview",
        data: { 'cocktailReviewID': data, 'cocktailId': data2 },
        type: "post",
        cache: false,
        success: function (result) {

            a.html('<i style="color:red" class="fa fa-heart fa-lg text-danger"></i > <span style="color:gold">' + result + '</span>').removeClass("reviewLike-2").addClass("reviewUnlike-2");

        },
        fail: function (xhr, textStatus, errorThrown) {
            alert('request failed');
            window.location = "/Error/CustomError";
        }

    })

});


$(document).on("click", '.reviewUnlike-2', function () {
    event.preventDefault();
    debugger;
    const data = $(this).attr('data-cocktailReviewID');
    const data2 = $(this).attr('data-cocktailId');
    //const data3 = $(this).attr('data-name');
    debugger;

    const url2 = "/Reviews/CocktailReviews/RemoveLikeCocktailReview?cocktailReviewID=" + data + "&cocktailId=" + data2;

    const a = $(this);
    debugger;
    $.ajax({
        url: url2,
        data: { 'cocktailReviewID': data, 'cocktailId': data2 },
        type: "post",
        cache: false,
        success: function (result) {

            a.html('<i class="fa fa-heart fa-lg text-danger"></i > <span style="color:gold">' + result + '</span>').removeClass("reviewUnlike-2").addClass("reviewLike-2");

        },
        fail: function (xhr, textStatus, errorThrown) {
            alert('request failed');
            window.location = "/Error/CustomError";
        }
    })

});
