//code for showing the modal for employee details on clicking the details button in the employee table in the admin dashboard with console log for debugging in every step 
//of the process
$(document).ready(function () {
    console.log("document ready");
    $(".details").click(function () {
        console.log("details button clicked");
        var id = $(this).attr("data-id");
        console.log("id: " + id);
        $.ajax({
            url: "/Admin/GetEmployeeDetails",
            type: "GET",
            data: { id: id },
            success: function (data) {
                console.log("ajax success");
                $("#employeeModal .modal-body").html(data);
                $("#employeeModal").modal("show");
            }
        });
    });
});



    







