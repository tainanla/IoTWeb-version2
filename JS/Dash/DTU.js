$(function() {

	var myChart = echarts.init(document.getElementById('container7'));

	// 指定图表的配置项和数据
	var option = {
		title: {
			text: '业务指标', //标题文本内容
		},
		toolbox: { //可视化的工具箱
			show: true,
			feature: {
				restore: { //重置
					show: true
				},
				saveAsImage: {//保存图片
					show: true
				}
			}
		},
		tooltip: { //弹窗组件
			formatter: "{a} <br/>{b} : {c}%"
		},
		series: [{
			name: '业务指标',
			type: 'gauge',
			axisLine: {            // 坐标轴线  
				lineStyle: {       // 属性lineStyle控制线条样式  
					color: [[0.2, '#c23531'], [0.8, '#63869e'], [1, '#91c7ae']]
				}
			},
			detail: { formatter: '{value}%' },
			data: [{ value: 3.082, name: '电压值' }]
		}]

	};

	// 使用刚指定的配置项和数据显示图表。
	myChart.setOption(option);

	setInterval(function () {//把option.series[0].data[0].value的值使用random()方法获取一个随机数
		option.series[0].data[0].value = (Math.random() * 0.2+2).toFixed(2) - 0;
		myChart.setOption(option, true);
	}, 2000);
	
});