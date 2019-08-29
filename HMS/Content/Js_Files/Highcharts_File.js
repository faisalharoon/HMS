
// Radialize the colors
const colors = ['#5E97CE', '#58BEBD', '#66C353', '#6d00f6', 'red', '#ff8b12', '#FFBE0B', '#1EFFBC']
const gradients = ['red', 'orange', 'blue', '#852cf5', 'red', '#FFD41F', '#7CFFD8', '#FFB364']

Highcharts.setOptions({
    colors: Highcharts.map(colors, (color, i) => ({
        radialGradient: {
            cx: 0.5,
            cy: 0.3,
            r: 0.7
        },
        stops: [
            [0, color],
            [1, Highcharts.Color(color).brighten(-0.3).get('rgb')] // darken
        ]
    }))
});


$(function () {
    $.ajax({
        url: "/WebApi/AdmissionsProgress",
        cache: false,
        success: function (data) {
            var xx = [];
            for (i = 0; i < data.length; i++) {

                date = new Date(data[i].Admission_Date);
                year = date.getFullYear();
                month = date.getMonth() + 1;
                dt = date.getDate();

                x = [Date.UTC(year, month - 1, dt), data[i].Total_Patients];
                xx.push(x);
            }
            //For highcharts data should be formatted as [[x,y],[x,y],...]
            seriesData = []
            xx.forEach(function (item) {
                seriesData.push([item[0], item[1]])
            })

            var today = new Date();
            var Currentdate = today.getFullYear() + '/' + (today.getMonth() + 1) + '/' + today.getDate();

            Highcharts.chart('hospitalSurvey', {
                chart: {
                    type: 'area'
                },
                title: {
                    text: 'Daily Patient Admissions'
                },
                subtitle: {
                    text: "Progress as of:" + Currentdate
                },

                //legend: {
                //    align: 'right',
                //    verticalAlign: '',
                //    borderWidth: 0
                //},


                //legend: {
                //    layout: 'vertical',
                //    align: 'bottom',
                //    verticalAlign: 'top',
                //    x: 150,
                //    y: 100,
                //    floating: true,
                //    borderWidth: 1,
                //    backgroundColor:
                //        Highcharts.defaultOptions.legend.backgroundColor || '#FFFFFF'
                //},
                xAxis: {
                    type: 'datetime',
                    gridLineWidth: 2,
                    //    labels: {
                    //        formatter: function () {
                    //            return Highcharts.dateFormat('%b %Y %d', this.value);
                    //        }
                    //    }
                },
                yAxis: {
                    title: {
                        text: ''
                    }
                },
                tooltip: {
                    shared: true,
                    valueSuffix: ' '
                },
                credits: {
                    enabled: false
                },
                plotOptions: {
                    area: {
                        //animation: false,
                        //color: "#c0c0c0",
                        //lineWidth: 1,
                        turboThreshold: 0,
                        marker: {
                            enabled: true,
                            radius: 4,
                            states: {
                                hover: {
                                    fillColor: "#666666",
                                    radius: 6
                                }
                            },
                            symbol: "circle"
                        },
                        shadow: false,
                        states: {
                            hover: {
                                enabled: true,
                                lineWidth: 3
                            }
                        }
                    }


                    //line: {
                    //    fillOpacity: 0.5
                    //}
                },
                series: [{
                    name: 'Patients Admitted',
                    data: seriesData,
                   // marker: { symbol: 'diamond', radius: 12 }
                }]
            });
        }
    });
});

$.getJSON("/WebApi/PatientDetails", function (data) {
$(document).ready(function () {
    // Build the chart
    Highcharts.chart('MonthlyAdmissions', {
        title: {
            text: ''
        },
        tooltip: {
            pointFormat: '{series.name} <b>{point.y}</b>'
        },
        plotOptions: {
            pie: {
                allowPointSelect: true,
                cursor: 'pointer',
                dataLabels: {
                    enabled: false,
                    format: '{point.y}',
                    style: {
                        color: (Highcharts.theme && Highcharts.theme.contrastTextColor) || 'black'
                    },
                    connectorColor: 'silver'
                }
            }
        },
        series: [{
            name: 'Total',
            type: 'pie',
            allowPointSelect: true,
            keys: ['name', 'y', 'selected', 'sliced'],
            data: [{ name: 'Temporary', y: data[0].TemporrayPatients },
            {
                name: 'Permanent',
                y: data[0].PermanentPatients,
                sliced: false,
                selected: true
            }
            ],
            showInLegend: true
        }]
    });
});
});

//Rooms Occupied
$.getJSON("/WebApi/RoomOccupied", function (data) {
$(document).ready(function () {
    Highcharts.chart('RoomsOccupied', {
    chart: {
        plotBackgroundColor: null,
        plotBorderWidth: null,
        plotShadow: false,
        type: 'pie'
    },
    title: {
        text: ''
    },
    tooltip: {
        pointFormat: '{series.name} <b>{point.y}</b>'
    },
    plotOptions: {
        pie: {
            allowPointSelect: true,
            cursor: 'pointer',
            dataLabels: {
                enabled: false,
                format: '<b>{point.name} </b>{point.y}'
            },
             showInLegend: true,
        }
    },
    series: [{
        name: '',
        colorByPoint: true,
        data: [{
            name: 'Private',
            y: data[0].PrivateRoom,
            sliced: true,
            selected: true
        }, {
            name: 'Semi Private',
                y: data[0].SemiPrivateRoom,
        }, {
            name: 'VIP Suite',
                y: data[0].VIPSuite,
        }, {
            name: 'Single Deluxe',
                y: data[0].SingleDeluxeRoom,
        }, {
            name: 'Two Bedded',
                y: data[0].TwoBeddedRoom,
        }, {
            name: 'Four Bedded',
                y: data[0].FourBeddedRoom,
        }, {
            name: 'Intensive Care Unit',
                y: data[0].IntensiveCareUnit,
        }, {
            name: 'Isolation',
                y: data[0].IsolationRoom,
        }, {
            name: 'Labour & Delivery',
                y: data[0].LabourDeliverySuite,
            },
            {
                name: 'Nursery',
                y: data[0].Nursery,
            }]
    }]
    });
    });
});


//$.getJSON("/WebApi/StaffSpeciality", function (data) {
//    $(document).ready(function () {
//        Highcharts.chart('StaffSpeciality', {
//            chart: {
//                plotBackgroundColor: null,
//                plotBorderWidth: null,
//                plotShadow: false,
//                type: 'pie'
//            },
//            title: {
//                text: ''
//            },
//            tooltip: {
//                pointFormat: '{series.name} <b>{point.y}</b>'
//            },
//            plotOptions: {
//                pie: {
//                    allowPointSelect: true,
//                    cursor: 'pointer',
//                    dataLabels: {
//                        enabled: false,
//                        format: '<b>{point.name} </b>{point.y}'
//                    },
//                    showInLegend: true,
//                }
//            },
//            series: [{
//                name: '',
//                colorByPoint: true,
//                data: [ 
//                    {
//                    name: 'Nursery',
//                    y: data[0].Nursery,
//                }]
//            }]
//        });
//    });
//});













//Tests Availibility


//$.getJSON("/WebApi/TestCategories", function (data) {
//$(function () {
//    $.ajax({
//        url: "/WebApi/StaffSpeciality",
//        cache: false,
//        success: function (data) {
//            var xx = [];
//            for (i = 0; i < data.length; i++) {

//                x = [data[i].TotalCount, data[i].Speciality ];
//                xx.push(x);
//            }
//            //For highcharts data should be formatted as [[x,y],[x,y],...]
//            seriesData = []
//            xx.forEach(function (item) {
//                seriesData.push([item[0], item[1]])
//            })

//            Highcharts.chart('StaffSpeciality', {
//                chart: {
//                    plotBackgroundColor: null,
//                    plotBorderWidth: null,
//                    plotShadow: false,
//                    type: 'pie'
//                },
//                title: {
//                    text: ''
//                },
//                tooltip: {
//                    pointFormat: '{series.name}: <b>{point.y}</b>'
//                },
//                plotOptions: {
//                    pie: {
//                        allowPointSelect: true,
//                        cursor: 'pointer',
//                        dataLabels: {
//                            enabled: false,
//                            format: '<b>{point.name}</b>: {point.y}'
//                        },
//                        showInLegend: true,
//                    }
//                },
//                series: [{
//                    name: '',
//                    colorByPoint: true,
//                    data: [
//                        {
//                            //  name: 'QQ',
//                            y: seriesData
//                        }]
//                }]
//            });
//        }
//    });
//});

//});

 

