
// sign up js 
function checkCapacity() {
    var selectedTrackId = document.getElementById("trackId").value;
    var selectedTrackCapacity = parseInt(document.getElementById("trackId").options[document.getElementById("trackId").selectedIndex].getAttribute("data-capacity"));
    var selectedTrackStudentCount = parseInt(document.getElementById("trackId").options[document.getElementById("trackId").selectedIndex].getAttribute("data-studentCount"));

    if (selectedTrackStudentCount >= selectedTrackCapacity) {
        capacityErrorMessage.innerText = "Track capacity exceeded!";
    } else {
        capacityErrorMessage.innerText = "";
    }
}
