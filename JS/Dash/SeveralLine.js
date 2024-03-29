﻿function loadTwoLine() {
    var myChart = echarts.init(document.getElementById('SeveralLine'));
    // 显示标题，图例和空的坐标轴
    myChart.setOption({
        title: {
            text: '异步数据加载示例'
        },
        tooltip: {
            trigger: 'axis'
        },
        legend: {
            data: ['进件', '办结']
        },
        toolbox: {
            show: true,
            feature: {
                mark: { show: true },
                dataView: { show: true, readOnly: false },
                magicType: { show: true, type: ['line', 'bar'] },
                restore: { show: true },
                saveAsImage: { show: true }
            }
        },
        calculable: true,
        xAxis: {
            type: 'category',
            boundaryGap: false, //取消左侧的间距
            data: []
        },
        yAxis: {
            type: 'value',
            splitLine: { show: false },//去除网格线
            name: ''
        },
        series: [{
            name: '进件',
            type: 'line',
            symbol: 'emptydiamond',    //设置折线图中表示每个坐标点的符号 emptycircle：空心圆；emptyrect：空心矩形；circle：实心圆；emptydiamond：菱形
            data: []
        },
        {
            name: '办结',
            type: 'line',
            symbol: 'emptydiamond',    //设置折线图中表示每个坐标点的符号 emptycircle：空心圆；emptyrect：空心矩形；circle：实心圆；emptydiamond：菱形
            data: []
        }]
    });
    myChart.showLoading();    //数据加载完之前先显示一段简单的loading动画
    var names = [];    //类别数组（实际用来盛放X轴坐标值）    
    var series1 = [];
    var series2 = [];
    $.ajax({
        type: 'get',
        url: 'json/echarts/line/lineTwo.txt',//请求数据的地址
        dataType: "json",        //返回数据形式为json
        success: function (result) {
            //请求成功时执行该函数内容，result即为服务器返回的json对象           
            $.each(result.jinJian, function (index, item) {
                names.push(item.AREA);    //挨个取出类别并填入类别数组
                series1.push(item.LANDNUM);
            });
            $.each(result.banJie, function (index, item) {
                series2.push(item.LANDNUM);
            });
            myChart.hideLoading();    //隐藏加载动画
            myChart.setOption({        //加载数据图表
                xAxis: {
                    data: names
                },
                series: [{
                    data: series1
                },
                {
                    data: series2
                }]
            });
        },
        error: function (errorMsg) {
            //请求失败时执行该函数
            alert("图表请求数据失败!");
            myChart.hideLoading();
        }
    });
};
loadSeveralLine();