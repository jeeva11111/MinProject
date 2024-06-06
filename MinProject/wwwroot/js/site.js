
function logoutModelCaller() {
    $("#logoutModal").modal('show');
}

function logoutModelHider() {
    $("#logoutModal").modal('hide');

}


$(document).ready(function () {



})


$("#gen-form").submit(function (e) {
    e.preventDefault();

    var inputValue = $("input[name='numberInput']").val();

    // Perform validation or further processing here
    if (inputValue === '') {
        $("input[name='numberInput']").css('background', '#fdc1c1').attr('title', 'set the number');
        return;
    }

    console.log("Submitted value:", inputValue);

    while (inputValue > 0) {

        // Create input elements
        var data_Type_input = $('<input>').attr({ type: 'text', placeholder: 'Data Type' }).addClass("form-control");
        var column_Name_input = $('<input>').attr({ type: 'text', placeholder: 'Column Name' }).addClass("form-control");
        var examples_input = $('<input>').attr({ type: 'text', placeholder: 'Examples' }).addClass("form-control");
        var remove_button = $('<button>').addClass('remove-btn btn-close');
        var options = $("<section>").addClass('form-control').append('option').val('').text('code').addClass("form-control")
        // Create table row and append input elements
        var row = $('<tr>').append(
            $('<td>').append(data_Type_input),
            $('<td>').append(column_Name_input),
            $('<td>').append(examples_input),
            $('<td>').append(remove_button),
            $('<td>').append(options),

        );

        // Append row to table body
        $("#gen-table tbody").append(row);

        inputValue--;
    }
});
$('#gen-table').on('click', '.remove-btn', function () {
    $(this).closest('tr').remove();
});