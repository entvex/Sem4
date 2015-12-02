
function graph() {

}

graph.prototype.drawGraph = function (graphdata, containername, title, comment) {
    var chart = new Highcharts.Chart({
              chart: {
                  width: 800,
                  height: 450,
                  spacingRight: 20,
                  plotBackgroundColor: null,
                renderTo: containername
            },
            title: {
                text: title
            },
            subtitle: {
                text: comment
            },

            xAxis: {
                type: 'logarithmic',
                min: 1,
                tickInterval: 1,
                minorTickInterval: 0.1,
                gridLineWidth: 1,
                title: {
                    text: 'Energy (MeV)'
                }
            },

            yAxis: {
                type: 'logarithmic',
                min: 1,
                tickInterval: 1,
                minorTickInterval: 0.1,
                gridLineWidth: 1,
                title: {
                    text: 'Stopping Power (MeV cm^2/g)'
                }
            },
            tooltip: {
                headerFormat: '<b>{series.name}</b><br>',
                pointFormat: '{point.x:.2f} cm : {point.y:.2f} kVm'
            },


            legend: {
                enabled: false
            },

            series: [{
                name: 'Experimental Data',
                data: graphdata,
                type: 'scatter'
            }]

        });
}

graph.prototype.drawGraphDoublePlots = function (graphdata, graphdata2, containername, title, comment) {
    var chart = new Highcharts.Chart({

            chart: {
                width: 800,
                height: 450,
                spacingRight: 20,
                renderTo: containername
            },
            title: {
                text: title
            },
            subtitle: {
                text: comment
            },

            xAxis: {
                type: 'logarithmic',
                min: 1,
                tickInterval: 1,
                minorTickInterval: 0.1,
                gridLineWidth: 1,
                title: {
                    text: 'Length (cm)'
                }
            },

            yAxis: {
                type: 'logarithmic',
                min: 1,
                tickInterval: 1,
                minorTickInterval: 0.1,
                title: {
                    text: 'Power (kVm)'
                }
            },
            tooltip: {
                headerFormat: '<b>{series.name}</b><br>',
                pointFormat: '{point.x:.2f} cm : {point.y:.2f} kVm'
            },


            legend: {
                enabled: false
            },

            series: [{
                name: 'Experimental Data',
                data: graphdata,
                type: 'scatter'
            }, {
                name: 'Calculated Data',
                data: graphdata2,
                type: 'scatter'
            }]

        });

}