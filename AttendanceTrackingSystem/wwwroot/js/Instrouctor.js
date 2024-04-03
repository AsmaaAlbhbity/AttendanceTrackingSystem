// Pagination variables
var currentPage = 1;
var itemsPerPage = 3;

// Function to display the current page
function displayPage() {
    var startIndex = (currentPage - 1) * itemsPerPage;
    var endIndex = startIndex + itemsPerPage;
    var tableRows = $("#instructorTable tbody tr");

    tableRows.hide(); // Hide all rows

    // Show rows for the current page
    for (var i = startIndex; i < endIndex && i < tableRows.length; i++) {
        $(tableRows[i]).show();
    }

    // Update pagination buttons
    updatePaginationButtons();
}

// Function to navigate to a specific page
function goToPage(page) {
    currentPage = page;
    displayPage();
}

// Function to navigate to the previous page
function previousPage() {
    if (currentPage > 1) {
        currentPage--;
        displayPage();
    }
}

// Function to navigate to the next page
function nextPage() {
    var totalRows = $("#instructorTable tbody tr").length;
    var totalPages = Math.ceil(totalRows / itemsPerPage);

    if (currentPage < totalPages) {
        currentPage++;
        displayPage();
    }
}

// Function to search the table by email or ID
function searchTable() {
    var searchText = $("#searchInput").val().toLowerCase().trim();
    var tableRows = $("#instructorTable tbody tr");

    if (searchText === "") {
        // Show all rows when search input is empty
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

    // Reset pagination to the first page when searching
    currentPage = 1;

    // Update pagination buttons
    updatePaginationButtons();
}

// Add event listener for keyup event on the search input field
document.getElementById("searchInput").addEventListener("keyup", function (event) {
    // Check if the search input is empty after releasing the delete key
    if (event.keyCode === 8 && this.value.trim() === "") {
        // If it's empty, call the function to reset the page state
        resetPageState();
    }
});

// Function to reset the page state to initial state
function resetPageState() {
    // Reset pagination variables
    currentPage = 1;

    // Show only the rows for the first page
    $("#instructorTable tbody tr").each(function (index) {
        if (index < itemsPerPage) {
            $(this).show();
        } else {
            $(this).hide();
        }
    });

    // Update pagination buttons
    updatePaginationButtons();
}

// Function to populate modal
function populateModal(userId, name, imgUrl, email, salary, supervisorTrack) {
    $('#modalUserId').text(userId);
    $('#modalUserName').text(name);
    $('#modalUserEmail').text(email);
    $('#modalUserSalary').text(salary);
    if (supervisorTrack) {
        $('#modalSupervisorTrack').text(supervisorTrack);
        $('#trackName').show(); // Show the paragraph if supervisorTrack is not null
    } else {
        $('#trackName').hide(); // Hide the paragraph if supervisorTrack is null
    }
    $('#modalInstructorImage').attr('src', imgUrl);
}

// Call displayPage initially to show the first page
displayPage();

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

