$('#loadMoreBarComments').on('click', function (e) {
    e.preventDefault();
    debugger;
    console.log('vleznah');

    const data = $(this).attr('data-id');
    const data2 = $(this).attr('data-name');
    const data3 = $(this).attr('data-rating');
    const data4 = $(this).attr('data-pageNumber');
    let currentPage = parseInt(data4) + 1;
    currentPage++;
    debugger;
    //const data3 = $(this).attr('data-orderByModel');
    $(this).attr('data-pageNumber', currentPage);


    var animation = $('<img id="dynamic">');
    animation.attr('src', '/assets/images/content/wizard_doom.gif');
    animation.attr('alt', 'gif image');
    animation.attr('width', '200');
    animation.attr('height', '200');
    //animation.attr('padding-bottom', '150px');
    animation.css('text-align', 'center');
    //console.log($('animation'))
    //console.log($('#pageCurrent')
    $.ajax({
        url: "https://localhost:44344/Reviews/BarReviews/BarReviews",
        data: { 'id': data, 'name': data2, 'rating': data3, 'pageNumber': currentPage  },
        type: "get",
        cache: false,
        success: function (result) {
            console.log(result);
            animation.insertAfter($('#loadMoreBarComments'));
            $('#loadMoreBarComments').attr('class', 'btn btn-default btn-sm disabled');
            setTimeout(function () {
                animation.remove();
                $('#loadMoreBarComments').attr('class', 'btn btn-default btn-sm');
                $(result).appendTo($('#putMoreReviewsHere'));
            }, 1000);
        }
    })
});