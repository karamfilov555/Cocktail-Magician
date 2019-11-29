﻿$('#loadMoreBarResults').on('click', function (e) {
    e.preventDefault();
    console.log('vleznah');

    const data = $(this).attr('data-searchCriteria');
    const data2 = $(this).attr('data-pageIndex');
    const data3 = $(this).attr('data-entity');
    let currentPage = parseInt(data2) + 1;
    debugger;

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
        url: "https://localhost:44344/Search/Search/SearchResults",
        data: { 'searchString': data, 'pageIndex': currentPage, 'entity': data3 },
        type: "get",
        cache: false,
        success: function (result) {
            console.log(result);
            animation.insertBefore($('#loadMoreBarResults'));
            $('#loadMoreBarResults').attr('class', 'btn btn-default btn-sm disabled');
            setTimeout(function () {
                animation.remove();
                $('#loadMoreBarResults').attr('class', 'btn btn-default btn-sm');
                //console.log($('#tbodyResults'));
                $(result).appendTo($('#putBarResults'));
            }, 1000);
        }
    })
});