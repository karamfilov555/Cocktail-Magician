function removeRow(element) {

    const thisRow = element.closest('tr');
    //let startAsString = thisRow.name;
    ////thisRow.style.display = 'none';

    let cell = thisRow.childNodes[1];
    var name2 = cell.getElementsByTagName('select')[0].name.toString();
    var start = Number(name2.charAt(12));
    if (start > 0) {


        var table = document.getElementById('ingredients');
        thisRow.parentNode.removeChild(thisRow);
        var rows = table.getElementsByTagName('tr');
        console.log(rows);
        //debugger;
        var index = start;
        for (var i = start + 1; i < rows.length; i++) {

            var firstCell = rows[i].getElementsByTagName('td')[0];
            var selectField = firstCell.getElementsByTagName('select')[0];
            selectField.name = 'Ingredients[' + index + '].Ingredient';
            //var inputField = firstCell.getElementsByTagName('input')[0];
            //inputField.name = 'Ingredients[' + new_id + '].Ingredient';
            var secondCell = rows[i].getElementsByTagName("td")[1];
            var inputField2 = secondCell.getElementsByTagName('input')[0];
            inputField2.name = 'Ingredients[' + index + '].Quantity';
            var thirdCell = rows[i].getElementsByTagName("td")[2];
            var selectField2 = thirdCell.getElementsByTagName('select')[0];
            selectField2.name = 'Ingredients[' + index + '].Unit';
            index++;
        }

        $('#total_row').val(index - 1);

    }
    //console.log(Number(name2.charAt(12)));

    //console.log(cell.getElementsByTagName('select')[0].name);
    //console.log(cell);
    //let dropdown = thisRow.getElementsByTagName('select')[0];
    //dropdown.selectedIndex = 0;
};