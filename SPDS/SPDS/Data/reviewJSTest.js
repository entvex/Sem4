

function createCol (datatype,author)
{
    var datatype1 = datatype;
    document.write("<tr><td><data onclick='showdata(\"" + datatype + "\",\"" +author +"\")'>"+datatype1+"</data></td><td>"+author+"</td><td><a href=''>Approve!</a></td><td><a href=''>Reject!</a></td></tr>");
    //logic needed for links to be directed to specific area which approves contenct or linked to a clickable javascript
}

function showdata(data,author)
{
var popupWindow =window.open('DataView.html', 'DataView','height=850,width=450');
popupWindow.datatype = data;
popupWindow.author = author;
}

 /*
 This section of the Javascript is purely for the Dataview to obtain the data sent from the Review site
 
 

 */
function DataView()
{
document.write(window.datatype + " was submitted by: " + window.author + "<br />");
SplitPro(window.datatype);
SplitMat(window.datatype);
}


function Randomdata()
{
return Math.floor((Math.random() * 100) + 1);
}

function Randomdatamax5()
{
return Math.floor((Math.random() * 5) + 1);
}

function SplitMat(Material) {
    var parts = Material.split('->');
    document.write("<br />Target Material: "+ parts[1]);
}

function SplitPro(Projectile) {
    var parts = Projectile.split('->');
    document.write("<br />Projectile Type: "+ parts[0]);
}














































