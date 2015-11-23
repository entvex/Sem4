function DeleteRowFunction(btndel) {

    var result = window.confirm("Are you sure?");

    if (typeof (btndel) == "object" && result == true) {
        
        var email = $(btndel).closest("tr").find('td:eq(2)').text();
        var request = new XMLHttpRequest();
        request.open("POST", "Administrator", true);
        request.setRequestHeader('Content-type', 'application/x-www-form-urlencoded');

        request.addEventListener("load", function (evt) {
            if (request.status != 200) {
                alert("Your change was not made because of an error, you will now be sent to the landing page");
                window.location.replace(window.location.host);
            }
        });

        request.send('email=' + 'delete;' + email);

        $(btndel).closest("tr").remove();

    } else {
        return false;
    }
}

function Promote(btndel) {

    var result = window.confirm("Are you sure?");

    if (typeof (btndel) == "object" && result == true) {

        var email = $(btndel).closest("tr").find('td:eq(2)').text();
        var request = new XMLHttpRequest();
        request.open("POST", "Administrator", true);
        request.setRequestHeader('Content-type', 'application/x-www-form-urlencoded');

        request.addEventListener("load", function (evt) {
            if (request.status != 200) {
                alert("Your change was not made because of an error, you will now be sent to the landing page");
                window.location.replace(window.location.host);
            }
        });

        request.send('email=' + 'promote;' + email);

        $(btndel).closest("tr").find('td:eq(3)').text("Reviewer");

    } else {
        return false;
    }
}

function Demote(btndel) {

    var result = window.confirm("Are you sure?");

    if (typeof (btndel) == "object" && result == true) {

        var email = $(btndel).closest("tr").find('td:eq(2)').text();
        var request = new XMLHttpRequest();
        request.open("POST", "Administrator", true);
        request.setRequestHeader('Content-type', 'application/x-www-form-urlencoded');

        request.addEventListener("load", function (evt) {
            if (request.status != 200) {
                alert("Your change was not made because of an error, you will now be sent to the landing page");
                window.location.replace(window.location.host);
            }
        });

        request.send('email=' + 'demote;' + email);

        $(btndel).closest("tr").find('td:eq(3)').text("Submitter");

    } else {
        return false;
    }
}