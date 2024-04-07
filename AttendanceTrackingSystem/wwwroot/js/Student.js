function deletePermission(permissionId) {
    console.log(permissionId)
    Swal.fire({
        title: 'Are you sure?',
        text: 'You want delete this permission',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#c66565',
        cancelButtonColor: '#198754',
        confirmButtonText: 'Yes, delete it!',
        cancelButtonText: 'Cancel'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: '/Student/DeletePermission',
                type: 'POST',
                data: { permissionId: permissionId },
                success: function (response) {
                    if (response.success) {
                        window.location.href = '/Student/Home';
                    } else {

                        Swal.fire({
                            icon: 'error',
                            title: 'Oops...',
                            text: response.message
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

