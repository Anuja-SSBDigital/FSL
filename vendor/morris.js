$(function() {
  'use strict';
  if ($('#morris-area-example').length) {
    Morris.Area({
      element: 'morris-area-example',
      lineColors: ['#76C1FA', '#F36368', '#63CF72', '#FABA66'],
      data: [{
          y: '2006',
          a: 100,
          b: 90
        },
        {
          y: '2007',
          a: 75,
          b: 105
        },
        {
          y: '2008',
          a: 50,
          b: 40
        },
        {
          y: '2009',
          a: 75,
          b: 65
        },
        {
          y: '2010',
          a: 50,
          b: 40
        },
        {
          y: '2011',
          a: 75,
          b: 65
        },
        {
          y: '2012',
          a: 100,
          b: 90
        }
      ],
      xkey: 'y',
      ykeys: ['a', 'b'],
      labels: ['Series A', 'Series B']
    });
  }
  
    
  if ($("#morris-bar-example").length) {
      
 var ABS=document.getElementById("hdn_ABS").value;
 var ACB=document.getElementById("hdn_ACB").value;
 var ACM=document.getElementById("hdn_ACM").value;
 var ACP=document.getElementById("hdn_ACP").value;
 var ACV=document.getElementById("hdn_ACV").value;
 var AFT=document.getElementById("hdn_AFT").value;
 var AGR=document.getElementById("hdn_AGR").value;
 var AJI=document.getElementById("hdn_AJI").value;
 var AOM=document.getElementById("hdn_AOM").value;
 var ATJ=document.getElementById("hdn_ATJ").value;
 var ATL=document.getElementById("hdn_ATL").value;
 var ATO=document.getElementById("hdn_ATO").value;
 var ATP=document.getElementById("hdn_ATP").value;
 var AUW=document.getElementById("hdn_AUW").value;
 var AUR=document.getElementById("hdn_AUR").value;
 var AWS=document.getElementById("hdn_AWS").value;
 
 
 var PBS=document.getElementById("hdn_PBS").value;
 var PCB=document.getElementById("hdn_PCB").value;
 var PCM=document.getElementById("hdn_PCM").value;
 var PCP=document.getElementById("hdn_PCP").value;
 var PCV=document.getElementById("hdn_PCV").value;
 var PFT=document.getElementById("hdn_PFT").value;
 var PGR=document.getElementById("hdn_PGR").value;
 var PJI=document.getElementById("hdn_PJI").value;
 var POM=document.getElementById("hdn_POM").value;
 var PTJ=document.getElementById("hdn_PTJ").value;
 var PTL=document.getElementById("hdn_PTL").value;
 var PTO=document.getElementById("hdn_PTO").value;
 var PTP=document.getElementById("hdn_PTP").value;
 var PUW=document.getElementById("hdn_PUW").value;
 var PUR=document.getElementById("hdn_PUR").value;
 var PWS=document.getElementById("hdn_PWS").value;
 
 
    Morris.Bar({
      element: 'morris-bar-example',
      barColors: ['#63CF72', '#F36368', '#76C1FA', '#FABA66'],
      data: [{
          y: 'Buffer Stop Inspection',
          a: PBS,
          b: ABS
        },
        {
          y: 'Cab Inspection',
          a: PCB,
          b: ACB
        },
        {
          y: 'Creep Inspection',
          a: PCP,
          b: ACP
        },
        {
          y: 'Curve Inspection',
          a: PCV,
          b: ACV
        },
        {
          y: 'Foot Inspection',
          a: PFT,
          b: AFT
        },
        {
          y: 'Curve Greasing',
          a: PGR,
          b: AGR
        },
        {
          y: 'Joint Inspection',
          a: PJI,
          b: AJI
        },
        {
          y: 'OMS Inspection',
          a: POM,
          b: AOM
        },
//        {
//          y: 'Turnout Joint with signal',
//          a: PTJ,
//          b: ATJ
//        },
        {
          y: 'Toe load',
          a: PTL,
          b: ATL
        },
        {
          y: 'Turnout Inspection',
          a: PTO,
          b: ATO
        },
        {
          y: 'Track Patrolling',
          a: PTP,
          b: ATP
        },
        {
          y: 'USFD of rails',
          a: PUW,
          b: AUW
        },
        {
          y: 'USFD of weld',
          a: PUR,
          b: AUR
        }
//        ,{
//          y: 'Wheel stop',
//          a: PWS,
//          b: AWS
//        }
      ],
      xkey: 'y',
      ykeys: ['a', 'b'],
      labels: ['Planned', 'Actual']
    });
  }
  if ($("#morris-donut-example").length) {
    Morris.Donut({
      element: 'morris-donut-example',
      colors: ['#76C1FA', '#F36368', '#63CF72', '#FABA66'],
      data: [{
          label: "Download Sales",
          value: 12
        },
        {
          label: "In-Store Sales",
          value: 30
        },
        {
          label: "Mail-Order Sales",
          value: 20
        }
      ]
    });
  }
  if ($('#morris-dashboard-taget').length) {
    Morris.Area({
      element: 'morris-dashboard-taget',
      parseTime: false,
      lineColors: ['#76C1FA', '#F36368', '#63CF72', '#FABA66'],
      data: [{
          y: 'Jan',
          Revenue: 190,
          Target: 170
        },
        {
          y: 'Feb',
          Revenue: 60,
          Target: 90
        },
        {
          y: 'March',
          Revenue: 100,
          Target: 120
        },
        {
          y: 'Apr',
          Revenue: 150,
          Target: 140
        },
        {
          y: 'May',
          Revenue: 130,
          Target: 170
        },
        {
          y: 'Jun',
          Revenue: 200,
          Target: 160
        },
        {
          y: 'Jul',
          Revenue: 150,
          Target: 180
        },
        {
          y: 'Aug',
          Revenue: 170,
          Target: 180
        },
        {
          y: 'Sep',
          Revenue: 140,
          Target: 90
        }
      ],
      xkey: 'y',
      ykeys: ['Target', 'Revenue'],
      labels: ['Monthly Target', 'Monthly Revenue'],
      hideHover: 'auto',
      behaveLikeLine: true,
      resize: true,
      axes: 'x'
    });
  }
});