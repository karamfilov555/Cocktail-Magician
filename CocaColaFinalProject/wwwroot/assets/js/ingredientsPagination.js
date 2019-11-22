$('#loadMoreIngredients').on('click', function (e) {
    e.preventDefault();
    console.log('vleznah');

    const data2 = $(this).attr('data-currPage');
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
        url: "https://localhost:44344/Ingredients/Ingredients/Index",
        data: {'currPage': currentPage },
        type: "get",
        cache: false,
        success: function (result) {
            animation.insertBefore($('#loadMoreIngredients'));
            $('#loadMoreIngredients').attr('class', 'btn btn-default btn-sm disabled')
            //animation.appendTo($('#tbody'));
            setTimeout(function () {
                animation.remove();
                $('#loadMoreIngredients').attr('class', 'btn btn-default btn-sm')
                console.log(result)
                $(result).appendTo($('#tbodyInredients'));
            }, 600);
        }
    })
});