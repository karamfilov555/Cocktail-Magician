$('#loadMoreBars').on('click', function (e) {
    e.preventDefault();
    debugger;
    console.log('vleznah');

    const data = $(this).attr('data-sortOrder');
    const data2 = $(this).attr('data-pageIndex');
    let currentPage = parseInt(data2) + 1;
    debugger;
    //const data3 = $(this).attr('data-orderByModel');

    $(this).attr('data-pageIndex', currentPage);


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
        url: "https://localhost:44344/Bars/Bars/ListBars",
        data: { 'sortOrder': data, 'pageNumber': currentPage},
        type: "get",
        cache: false,
        success: function (result) {
            console.log(result);
            animation.insertBefore($('#loadMoreBars'));
            $('#loadMoreBars').attr('class', 'btn btn-default btn-sm disabled');
            setTimeout(function () {
                animation.remove();
                $('#loadMoreBars').attr('class', 'btn btn-default btn-sm');
                console.log($('#tbodyBars'));
                $(result).appendTo($('#tbodyBars'));
            }, 600);
        }
    })
});