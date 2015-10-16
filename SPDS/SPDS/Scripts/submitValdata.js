
            
function getExtension(filename) {
    var parts = filename.split('.');
    return parts[parts.length - 1];
}

function isxml() {
    var filename =document.getElementById("upfile").value;
    var ext = getExtension(filename);
    if (ext == 'xml')
    {
        //alert("XML file is valid: ");
        document.getElementById("fileok").innerHTML = " XML file status:<valid> Valid! </valid>";
        return true;
    }
    else
    {
        //alert("Not a Valid XML file: ");
        document.getElementById("fileok").innerHTML = " XML file status:<invalid> Invalid! <invalid>";
        return false;
    }
}       
            
function validateInputs()
{
return validatexml();
}
function validatexml()
{
     if(isxml())
    {
        return true;
    }
    else
    {
        alert("Please check your XML file and try again!");
        return false;
    }
}