$(document).ready(function () {
    $("#gen-form").submit(function (e) {
        e.preventDefault();


        let isValid = true;
        var inputValue = $("input[name='numberInput']").val();

        if (inputValue === '' || inputValue == 0) {
            $("input[name='numberInput']").css('background', '#fdc1c1').attr('title', 'set the number');
            $("#number-validNumber").css('display', 'block');
            return;
        } else {
            $("input[name='numberInput']").css('background', '#fff');
            $("#number-validNumber").css('display', 'none');
        }

        console.log("Submitted value:", inputValue);

        while (inputValue > 0) {
            var data_Type_input = $('<input>').attr({ type: 'text', placeholder: 'Data Type' }).addClass("form-control").attr('name', 'DataType');
            var column_Name_input = $('<input>').attr({ type: 'text', placeholder: 'Column Name' }).addClass("form-control").attr('name', 'ColumnName');
            var examples_input = $('<input>').attr({ type: 'text', placeholder: 'Examples' }).addClass("form-control").attr('name', 'Examples');
            var remove_button = $('<button>').addClass('remove-btn btn-close');

            var row = $('<tr>').append(
                $('<td>').append(data_Type_input),
                $('<td>').append(column_Name_input),
                $('<td>').append(examples_input),
                $('<td>').append('<select class="form-control country-dropdown" multiple="multiple"></select>'),
                $('<td>').append(remove_button)
            );

            $("#gen-table tbody").append(row);
            inputValue--;
        }

        $("#submit-data").css('display', 'block');
        GetCountryList(); // Call the function after the rows are appended
    });

    $('#gen-table').on('click', '.remove-btn', function () {
        $(this).closest('tr').remove();
    });

    $('#submit-data').click(function () {
        let dynamicDataList = [];

        $("#gen-table tbody tr").each(function () {
            var dataType = $(this).find('input[placeholder="Data Type"]').val();
            var columnName = $(this).find('input[placeholder="Column Name"]').val();
            var examples = $(this).find('input[placeholder="Examples"]').val();
            var options = $(this).find('.country-dropdown').val();

            if (dataType.trim() === '' || dataType.length === 0) {
                $(this).find('input[placeholder="Data Type"]').css('background', '#fdc1c1').attr('placeholder', 'Enter the value');
                isValid = false;
            }

            if (columnName.trim() === '' || columnName.length === 0) {
                $(this).find('input[placeholder="Column Name"]').css('background', '#fdc1c1').attr('placeholder', 'Enter the value');
                isValid = false;
            }
            if (examples.trim() === '' || columnName.length === 0) {
                $(this).find('input[placeholder="Examples"]').css('background', '#fdc1c1').attr('placeholder', 'Enter the value');
                isValid = false;
            }


            dynamicDataList.push({
                Data_Type: dataType,
                ColumnName: columnName,
                Examples: examples,
                optionts: options
            });
        });
        if (isValid) {

            $.ajax({
                url: '/Generate/DynamicFormITems',
                type: 'POST',
                contentType: 'application/json',
                data: JSON.stringify(dynamicDataList),
                success: function (response) {
                    alert('Data saved successfully!');
                },
                error: function (error) {
                    alert('Error saving data!');
                }
            });
        }
    });
});

function GetCountryList() {
    $.ajax({
        url: '/Generate/CountryResult',
        type: 'GET',
        success: function (res) {
            var response = res.response;
            $(".country-dropdown").each(function () {
                var select = $(this);
                select.append($('<option>').val('').text('Select Option'));
                $.each(response, function (idx, val) {
                    select.append($('<option>').val(val.id).text(val.name));
                });
                select.select2({
                    placeholder: "Select countries",
                    allowClear: true
                }); // Initialize Select2 on the dropdown
            });
        }
    });
}



function showModel(action, id) {
    //   $("#itemForm")[0].reset()
    if (action === 'create') {
        $("#itemModal").modal('show')
        $(".modal-title").text('Add model');
        $("#saveButton").text('save')
    }
         
}

function HideModel() {
    $("#itemModal").modal('hide')

}