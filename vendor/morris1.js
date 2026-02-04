$(function () {

    var Jan = document.getElementById("Hdn_Jan").value;
    var Feb = document.getElementById("Hdn_Feb").value;
    var Mar = document.getElementById("Hdn_Mar").value;
    var Apr = document.getElementById("Hdn_Apr").value;
    var May = document.getElementById("Hdn_May").value;
    var Jun = document.getElementById("Hdn_Jun").value;
    var Jul = document.getElementById("Hdn_Jul").value;
    var Aug = document.getElementById("Hdn_Aug").value;
    var Sep = document.getElementById("Hdn_Sep").value;
    var Oct = document.getElementById("Hdn_Oct").value;
    var Nov = document.getElementById("Hdn_Nov").value;
    var Dec = document.getElementById("Hdn_Dec").value;

    'use strict';
    if ($('#morris-line-example').length) {
        Morris.Line({
            element: 'morris-line-example',
            lineColors: ['#63CF72', '#F36368', '#76C1FA', '#FABA66'],
            data: [{
                y: 'Jan',
                a: Jan
            },
            {
                y: 'Feb',
                a: Feb
            },
            {
                y: 'Mar',
                a: Mar
            },
            {
                y: 'Apr',
                a: Apr
            },
            {
                y: 'May',
                a: May
            },
            {
                y: 'Jun',
                a: Jun
            },
            {
                y: 'Jul',
                a: Jul
            },
            {
                y: 'Aug',
                a: Aug
            },
            {
                y: 'Sep',
                a: Sep
            },
            {
                y: 'Oct',
                a: Oct
            },
            {
                y: 'Nov',
                a: Nov
            },
            {
                y: 'Dec',
                a: Dec
            }
            ],
            xkey: 'y',
            ykeys: ['a'], parseTime: false,
            labels: ['Cases']
        });
    }

});