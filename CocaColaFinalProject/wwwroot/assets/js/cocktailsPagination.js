
$('#loadMore').on('click', function (event) {
    event.preventDefault();
    console.log($('#loadMore'))
  
    const data = $(this).attr('data-sortOrder');
    const data2 = $(this).attr('data-currPage');
    let currentPage = parseInt(data2);
    currentPage++;
    const data3 = $(this).attr('data-orderByModel');

    $(this).attr('data-currPage',currentPage)

    var animation = $('<img id="dynamic">'); 
    animation.attr('src', '/assets/images/content/circle.gif');
    animation.attr('alt', 'gif image');
    animation.attr('width', '100');
    animation.attr('height', '100');
    animation.css('text-align', 'center');
    //console.log($('animation'))
    //console.log($('#pageCurrent')
    $.ajax({
        url: "https://localhost:44344/Cocktails/Cocktails/ListCocktails",
        data: { 'sortOrder': data, 'currPage': currentPage, 'orderByModel': data3 },
        type: "get",
        cache: false,
        success: function (result) {
            animation.appendTo($('#tbody'));
            setTimeout(function () {
                animation.hide();
            $(result).appendTo($('#tbody'));
            }, 1000);
        }
    })
});