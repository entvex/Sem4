function graph() {

}

graph.prototype.drawGraph = function (graphdata, containername) {
    $(function () {
        $(containername).highcharts({

            chart: {
                width: 900,
                height: 600,
                spacingRight: 20
            },
            title: {
                text: 'Atom# -> Atom#'
            },
            subtitle: {
                text: '#Comment or something here#'
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
            }]

        });
    });
}

graph.prototype.drawGraphDoublePlots = function (graphdata, graphdata2, containername) {
    $(function () {
        $(containername).highcharts({

            chart: {
                width: 900,
                height: 600,
                spacingRight: 20
            },
            title: {
                text: 'Atom# -> Atom#'
            },
            subtitle: {
                text: '#Comment or something here#'
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
    });
}