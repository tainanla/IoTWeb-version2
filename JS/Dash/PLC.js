$(function() {
	var dom = document.getElementById("container1");
	var myChart = echarts.init(dom);
	var app = {};
	option = null;
	app.title = '环形图';

	option = {
	    tooltip: {
	        trigger: 'item',
	        formatter: "{a} <br/>{b}: {c} ({d}%)"
	    },
	    color:['#37a2da','#ffd85c'],
	    series: [
	        {
	            name:'DTU',
	            type:'pie',
	            radius: ['50%', '70%'],
	            avoidLabelOverlap: false,
	            label: {
	                normal: {
	                    show: false,
	                    position: 'center'
	                },
	                emphasis: {
	                    show: true,
	                    textStyle: {
	                        fontSize: '24',
	                        color:'#00fcff',
	                        fontWeight: 'bold'
	                    }
	                }
	            },
	            labelLine: {
	                normal: {
	                    show: false
	                }
	            },
	            data:[
	                {value:1, name:'在线'},/*vm.plc.on*/
	                {value:10, name:'离线'}
	            ]
	        }
	    ]
	};

	;
	if (option && typeof option === "object") {
	    myChart.setOption(option, true);
	}
});