
$('#loadMore').on('s', function (event) {
    event.preventDefault();
    console.log($('#loadMore'))
  
    const data = $(this).attr('data-sortOrder');
    const data2 = $(this).attr('data-currPage');
    let currentPage = parseInt(data2);
    currentPage++;
    const data3 = $(this).attr('data-orderByModel');

    $(this).attr('data-currPage',currentPage)

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
        url: "https://localhost:44344/Cocktails/Cocktails/ListCocktails",
        data: { 'sortOrder': data, 'currPage': currentPage, 'orderByModel': data3 },
        type: "get",
        cache: false,
        success: function (result) {
            animation.insertBefore($('#loadMore'));
            $('#loadMore').attr('class','btn btn-default btn-sm disabled')
            //animation.appendTo($('#tbody'));
            setTimeout(function () {
                animation.remove();
                $('#loadMore').attr('class', 'btn btn-default btn-sm')
            $(result).appendTo($('#tbody'));
            }, 1000);
        }
    })
});