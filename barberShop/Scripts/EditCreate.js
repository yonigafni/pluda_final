function combineDate() {
    var dateOnly = document.getElementById('day').value;
    var timeOnly = document.getElementById('time').value;
    var combined = dateOnly + " " + timeOnly;
    document.getElementById('AppointmentDateTime').value = combined;
    return true;
}