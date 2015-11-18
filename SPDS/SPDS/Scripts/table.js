function TableClass() {
}
TableClass.prototype.createTable = function (data) {

    var len = data[0].length;
    var hei = data.length;

    document.write("<table border='0'>");

    for (i = 0; i < hei ; i++) {
        document.write("<tr>");
        for (j = 0; j < len; j++) {
            document.write("<td>" + data[i][j] + "</td>");
        }
        document.write("</tr>");

    }
    document.write("</table>");
};