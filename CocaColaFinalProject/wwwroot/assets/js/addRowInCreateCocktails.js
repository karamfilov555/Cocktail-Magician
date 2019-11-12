function addRow() {
    var new_id = Number($("#total_row").val()) + 1;
    $("#total_row").val(new_id);
    if (new_id < 10) {
        var table = document.getElementById('ingredients');
        var row = document.getElementsByTagName('tr')[1];
        var newRow = row.cloneNode(true);
        var firstCell = newRow.getElementsByTagName('td')[0];
        var selectField = firstCell.getElementsByTagName('select')[0];
        selectField.name = 'Ingredients[' + new_id + '].Ingredient';
        var inputField = firstCell.getElementsByTagName('input')[0];
        inputField.name = 'Ingredients[' + new_id + '].Ingredient';
        var secondCell = newRow.getElementsByTagName("td")[1];
        var inputField2 = secondCell.getElementsByTagName('input')[0];
        inputField2.name = 'Ingredients[' + new_id + '].Quantity';
        var thirdCell = newRow.getElementsByTagName("td")[2];
        var selectField2 = thirdCell.getElementsByTagName('select')[0];
        selectField2.name = 'Ingredients[' + new_id + '].Unit';


        table.appendChild(newRow);

    }
    //var currentRow = $("#row_0").clone();
    //var inputs = $('currentRow th');

    //console.log(inputs);
    //console.log(firstCell);
    //firstCell.attr('select').name = 'Ingredients[' + new_id + '].Ingredient';
    //console.log(firstCell.attr('select').name);
    //var thirdCell = currentRow.getElementsByTagName("td")[2];
    //table.appendChild(newRow);


    //var html = '';
    //html += '<tr>'
    //html += '<td><select id="selectmenu" asp-for="@Model.Ingredients['+ new_id+'].Ingredient" class="select-editable form-control" asp - items="@Model.IngredientsNames" input = "text" placeholder = "Choose Ingredients..." ></select >'+
    //    '<input asp-for="@Model.Ingredients['+new_id+'].Ingredient" class="form-control" type="text" placeholder="Or add a new one:" /></td >';
    //html += '<td><input asp-for="@Model.Ingredients[0].Quantity" class="form-control" /></td>';
    //html += '<td><select class="form-control" asp -for= "@Model.Ingredients[' + new_id + '].Unit" asp - items= "Html.GetEnumSelectList<Unit>()" >' +
    //    '<option hidden disabled selected value="">Choose Unit Type:</option></select ></td >';
    //html += '</tr>'
    //$("#ingredients").append(html);
}