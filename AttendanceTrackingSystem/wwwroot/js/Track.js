function changeSupervisour(id,trackid)
{
    $.ajax({
        url: '/Admin/ChangeSupervisourForTrack/',
        type: 'GET',
        data: { id: id, Trackid: trackid },
        success: function (result) {
            Swal.fire('Success!', 'Supervisor changed successfully', 'success');

        },
        error: function (xhr, status, error) {
            console.error('AJAX Error:', status, error);
            alert('Error occurred while fetching data.');
        }
    });
}
function ChangeTrakState(trackid) {
    $.ajax({
        url: '/Admin/ChangeStateForTrack/',
        type: 'GET',
        data: { id: trackid },
        success: function (result) {
            Swal.fire('Success!', 'State changed successfully', 'success');

        },
        error: function (xhr, status, error) {
            console.error('AJAX Error:', status, error);
            alert('Error occurred while fetching data.');
        }
    });
}


$(document).ready(function () {
    // Attach change event listener to the select element
$("#searchInput").on("keyup", function () {
    var value = $(this).val().toLowerCase();
    $("#instructorTable tbody tr").filter(function () {
        $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
    });
    $("#attendanceList .attendance-item").filter(function () {
        $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
    });
});

    $('.selectSupervisour').change(function () {
        var selectedValue = $(this).val();
        console.log('Selected value:', selectedValue);

        var trackId = $(this).data('trackid'); 

        changeSupervisour(selectedValue, trackId);

    });
    $('input[type="checkbox"]').change(function () {
        var isChecked = $(this).prop('checked');
        var trackId = $(this).data('trackid'); // Get the data-trackid attribute value

        ChangeTrakState(trackId);
        
    });
});
