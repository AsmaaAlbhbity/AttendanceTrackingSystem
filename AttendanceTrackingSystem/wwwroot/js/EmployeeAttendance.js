markAttendanceInstructor
/*==========================================================*/
/* Add */
/*==========================================================*/
function markAttendanceInstructor(checkbox) {
    var row = checkbox.parentNode.parentNode;
    var id = row.cells[0].innerText.trim();
    var name = row.cells[1].innerText.trim();
    var currentTime = new Date();

    var hours = currentTime.getHours();
    var minutes = currentTime.getMinutes();
    var ampm = hours >= 12 ? 'PM' : 'AM';

    hours = hours % 12;
    hours = hours ? hours : 12;

    minutes = minutes < 10 ? '0' + minutes : minutes;
    var formattedTime = hours + ':' + minutes + ' ' + ampm;


    if (checkbox.checked) {
        $.ajax({
            url: '/Attendance/AddEmployeeAttendance',
            type: 'Post',
            data: { id: id },
            success: function (response) {
                console.log('Attendance added successfully');
                Swal.fire('Success!', 'Attendance recorded successfully.', 'success');
                var randomColor = getRandomLightColor();
                var attendanceItem = document.createElement('div');
                attendanceItem.classList.add('attendance-item');
                attendanceItem.setAttribute('data-id', id);
                attendanceItem.style.backgroundColor = randomColor;


                var badge = document.createElement('span');
                badge.innerText = id + ' - ' + name;

                attendanceItem.appendChild(badge);
                attendanceItem.innerHTML += ' ' + formattedTime + ' <span class="remove-btn" onclick="removeUserAttendance(this)">x</span>';
                document.getElementById('attendanceList').appendChild(attendanceItem);

                checkbox.disabled = true;
                var checkoutCheckbox = row.cells[3].getElementsByTagName('input')[0];
                checkoutCheckbox.disabled = false;


            },
            error: function (xhr, status, error) {
                console.error('Error adding attendance: ' + error);

            }
        });

    }
}
/*==========================================================*/
/* Update */
/*==========================================================*/
function UpdateUserAttendance(checkbox) {
    var row = checkbox.parentNode.parentNode;
    var id = row.cells[0].innerText.trim();
    var name = row.cells[1].innerText.trim();
    var time = new Date().toLocaleTimeString();

    if (checkbox.checked) {
        $.ajax({
            url: '/Attendance/UpdateUserAttendanceRecourd',
            type: 'POST',
            data: { id: id },
            success: function (response) {
                console.log('Attendance Update successfully');
                Swal.fire('Success!', 'Attendance Update successfully.', 'success');

                checkbox.disabled = true;

            },
            error: function (xhr, status, error) {
                console.error('Error adding attendance: ' + error);

            }
        });

    }
}
function toggleCheckout(checkbox) {
    var row = checkbox.parentNode.parentNode;
    var checkinCheckbox = row.cells[2].getElementsByTagName('input')[0]; // Check-in checkbox in the third cell

    checkinCheckbox.disabled = checkbox.checked; // Disable Check In if Check Out is checked
}
/*==========================================================*/
/* delete */
/*==========================================================*/
function removeUserAttendance(removeBtn) {
    var item = removeBtn.parentNode;
    var id = item.getAttribute('data-id');
    console.log(id, item);
    var name = item.innerText.split(' ')[0].trim(); // Extracting the student name

    Swal.fire({
        title: 'Are you sure?',
        text: 'You are about to remove the attendance record!',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Yes, remove it!',
        cancelButtonText: 'Cancel'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: '/Attendance/DeleteUserAttendanceRecord',
                type: 'GET',
                data: { id: id },
                success: function (response) {

                    Swal.fire('Deleted!', 'Attendance record has been removed.', 'success');

                    item.closest('.attendance-item').remove();

                    console.log(id);
                    var tableRow = document.querySelector('td[data-id="' + id + '"]');
                    console.log(tableRow);
                    if (tableRow) {
                        var checkboxes = tableRow.parentElement.querySelectorAll('input[type="checkbox"]');
                        console.log(checkboxes);
                        checkboxes[0].disabled = false; // Enable Check In
                        checkboxes[0].checked = false; // Uncheck Check In
                        checkboxes[1].disabled = true; // Disable Check Out
                        checkboxes[1].checked = false; // Uncheck Check Out
                    }


                },
                error: function (xhr, status, error) {
                    console.error('Error adding attendance: ' + error);
                    // Handle error
                }
            });



        }
    });
}

