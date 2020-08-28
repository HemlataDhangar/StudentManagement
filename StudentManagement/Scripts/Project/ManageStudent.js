
$(function () {

    // On change Date of birth
    $('#DateOfBirth').change(function () {
        var rgt = $(this).val();

        if (calculate_age(new Date(rgt)) < 9) {
            $('#DateOfBirth').val("");
        }
    });
});

//Course multiselect dropdown
$(function () {

    // Enable Live Search.
    $('.CountryList').attr('data-live-search', true);

    // Enable multiple select.
    $('.CountryList').attr('multiple', true);
    $('.CountryList').attr('data-selected-text-format', 'count');

    //Style and title
    $('.CountryList').selectpicker(
        {
            title: 'Select multiple courses',
            style: 'btn-default',
        });
});

//Calculate age
function calculate_age(dob) {
    var diff_ms = Date.now() - dob.getTime();
    var age_dt = new Date(diff_ms);

    return Math.abs(age_dt.getUTCFullYear() - 1970);
}