function chartline () {
}


chartline.prototype.drawLineGraph = function (array1,array2,canvasID) {
    var lineChartData = {
			labels : array1,
			datasets : [
				{
					label: "Stopping Power",
					fillColor : "rgba(220,220,220,0.2)",
					strokeColor : "rgb(3, 13, 255)",
					pointColor : "rgb(21, 16, 83)",
					pointStrokeColor : "#fff",
					pointHighlightFill : "#fff",
					pointHighlightStroke : "rgba(220,220,220,1)",
					data : array2
				}
			]

		}

	window.onload = function() {
		var ctx = document.getElementById(canvasID).getContext("2d");
		window.myLine = new Chart(ctx).Line(lineChartData, {
			responsive: true,bezierCurve: true 
		});
	}
};
chartline.prototype.drawDoubleLineGraph = function (array1,array2,array3,canvasID) {
    var lineChartData = {
			labels : array1,
			datasets : [
				{
					label: "Stopping Power",
					fillColor : "rgba(220,220,220,0.2)",
					strokeColor : "rgb(3, 13, 255)",
					pointColor : "rgb(21, 16, 83)",
					pointStrokeColor : "#fff",
					pointHighlightFill : "#fff",
					pointHighlightStroke : "rgba(220,220,220,1)",
					data : array2
				},
                {
					label: "Stopping Power",
					fillColor : "rgba(220,220,220,0.2)",
					strokeColor : "rgba(255,55,0,1)",
					pointColor : "rgba(153,0,0,1)",
					pointStrokeColor : "#fff",
					pointHighlightFill : "#fff",
					pointHighlightStroke : "rgba(220,220,220,1)",
					data : array3
				}
			]

		}

	window.onload = function() {
		var ctx = document.getElementById(canvasID).getContext("2d");
		window.myLine = new Chart(ctx).Line(lineChartData, {
			responsive: true,bezierCurve: true 
		});
	}
};