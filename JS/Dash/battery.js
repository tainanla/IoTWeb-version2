$(function () {
	var dom = document.getElementById("container");
	var myChart = echarts.init(dom);	
	option = null;
	//var myChart = echarts.init(document.getElementById('container'));
	// 指定图表的配置项和数据
	var option = {		
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
			formatter: "{a} <br/>{b} : {c}V"
		},
		series: [{
			min: 0,
			max: 15,    //设置刻度盘内数值区间
			splitNumber: 1, 
			name: '电压',
			color: '#00f6ff',
			type: 'gauge',
			axisLine: {            // 坐标轴线  
				textStyle: {       // 属性lineStyle控制线条样式
					color: '#000',
					fontSize: 9,   //改变仪表盘内刻度数字的大小
					shadowColor: '#000',
				},
	
				lineStyle: {       // 属性lineStyle控制线条样式  
					width: 5,
					
					//color: [[5, '#c23531'], [10, '#63869e'], [15, '#91c7ae']]
				}
			},
			detail: { formatter: '{value}V' },
			data: [{ value: 3.082, name: '电压值' }]
		}]

	};

	// 使用刚指定的配置项和数据显示图表。
	myChart.setOption(option);

/*	setInterval(function () {//把option.series[0].data[0].value的值使用random()方法获取一个随机数
		option.series[0].data[0].value = (Math.random() * 0.2 + 2).toFixed(2) - 0;
		myChart.setOption(option, true);
	}, 2000);
	*/
});