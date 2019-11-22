$('#loadComments').on('click', function (e) {
    e.preventDefault();
    console.log('vleznah');

    const data2 = $(this).attr('data-currPage');
    const data = $(this).attr('data-Id');
    let currentPage = parseInt(data2);
    currentPage++;

    $(this).attr('data-currPage', currentPage)

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
        url: "https://localhost:44344/Reviews/CocktailReviews/RateCocktail",
        data: {'Id': data , 'currPage': currentPage},
        type: "get",
        cache: false,
        success: function (result) {
            animation.insertAfter($('#loadComments'));
            $('#loadComments').attr('class', 'btn btn-danger disabled')
            //animation.appendTo($('#tbody'));
            setTimeout(function () {
                animation.remove();
                $('#loadComments').attr('class', 'btn btn-danger')
                console.log(result)
                $(result).appendTo($('#putCommentsHere'));
            }, 600);
        }
    })
});