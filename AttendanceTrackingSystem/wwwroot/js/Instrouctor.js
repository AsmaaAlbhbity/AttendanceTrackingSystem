
// Function to search the table by email or ID
function searchTable() {
    var searchText = $("#searchInput").val().toLowerCase().trim();
    var tableRows = $("#instructorTable tbody tr");

    if (searchText === "") {

        tableRows.show();
    } else {
        tableRows.each(function (index) {
            var email = $(this).find("td:eq(2)").text().toLowerCase();
            var id = $(this).find("td:eq(0)").text().toLowerCase();
            var isVisible = email.startsWith(searchText) || id.startsWith(searchText);

            if (isVisible && index >= (currentPage - 1) * itemsPerPage && index < currentPage * itemsPerPage) {
                $(this).show();
            } else {
                $(this).hide();
            }
        });
    }

}




// Function to populate modal
function populateModal(userId, name, imgUrl, email, salary, supervisorTrack) {
    $('#modalUserId').text(userId);
    $('#modalUserName').text(name);
    $('#modalUserEmail').text(email);
    $('#modalUserSalary').text(salary);
    if (supervisorTrack) {
        $('#modalSupervisorTrack').text(supervisorTrack);
        $('#trackName').show(); 
    } else {
        $('#trackName').hide(); 
    }
    $('#modalInstructorImage').attr('src', imgUrl);
}



function deleteInstructor(userId, userName) {
    Swal.fire({
        title: 'Are you sure?',
        text: 'You are about to delete ' + userName + '. This action cannot be undone.',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!',
        cancelButtonText: 'Cancel'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: '/Admin/DeleteInstructor',
                type: 'POST',
                data: { userId: userId },
                success: function (response) {
                    if (response.success) {
                        window.location.href = response.redirectUrl;
                    } else {
                        // Display an error message
                        Swal.fire({
                            icon: 'error',
                            title: 'Oops...',
                            text: response.message
                        }).then((result) => {
                            // Check if deletion failed due to instructor being a supervisor
                            if (response.message.includes("Cannot delete instructor")) {
                                window.location.href = '/Admin/ChooseSupervisor?userId=' + userId; // Redirect to ChooseSupervisor view with userId parameter
                            }
                        });
                    }
                },
                error: function (xhr, status, error) {
                    console.error(error);
                }
            });
        }
    });
}

