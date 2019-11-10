

$('#loadMore').on('click', function (event) {
    event.preventDefault();
    console.log($('#loadMore'))
  
    const data = $(this).attr('data-sortOrder');
    const data2 = $(this).attr('data-currPage');
    let currentPage = parseInt(data2);
    currentPage++;
    const data3 = $(this).attr('data-orderByModel');

    $(this).attr('data-currPage',currentPage)

    console.log($('#pageCurrent'))
    //console.log($('#pageCurrent')
    $.ajax({
        url: "https://localhost:44344/Cocktails/Cocktails/ListCocktails",
        data: { 'sortOrder': data, 'currPage': currentPage, 'orderByModel': data3 },
        type: "get",
        cache: false,
        success: function (result) {
            console.log(result)
            $(result).appendTo($('#tbody'));
        }
    })
});