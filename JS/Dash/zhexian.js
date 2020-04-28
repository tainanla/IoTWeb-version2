$(function () {
    var myChart = echarts.init(document.getElementById('main'));

    // 显示标题，图例和空的坐标轴
    myChart.setOption({
        color: '#005eff',
        tooltip: {},
        toolbox: {
            feature: {
                dataView: { show: true, readOnly: false },
                //magicType: { show: true, type: ['line'] },
                restore: { show: true },
                saveAsImage: { show: true }
            }
        },
        legend: {
            data: ['状态']
        },
        xAxis: {
            color: '#00f6ff',
            data: []
        },
        yAxis: {
            names: '状态',
            min: 0,
            max: 1,
            minInterval: 1,
            color:'#00f6ff',
            axisLabel: {
                formatter: function (value) {
                    var texts = [];
                    if (value == 0) {
                        texts.push('关');
                    }
                    else if (value = 1) {
                        texts.push('开');
                    }
                    return texts;
                }
            }
        },
        series: [{
            name: '状态',
            type: 'line',
            data: []
        }]
    });

    myChart.showLoading();    //数据加载完之前先显示一段简单的loading动画

    var time = [];    //时间数组（实际用来盛放X轴坐标值）
    var value = [];    //状态数组（实际用来盛放Y坐标值）

    /*   $.get('data.json').done(function (data) {
    // 填入数据
    myChart.setOption({
    xAxis: {
       data: data.categories
    },
    series: [{
       // 根据名字对应到相应的系列
       name: '销量',
       data: data.data
    }]
    });
    });*/
    $.ajax({
        type: "post",
        async: true,            //异步请求（同步请求将会锁住浏览器，用户其他操作必须等待请求完成才可以执行）
        url: "http://localhost:5000/Display/GetData",    //请求发送到Getdata处
        data: {},
        dataType: "json",        //返回数据形式为json
        success: function (result) {
            //请求成功时执行该函数内容，result即为服务器返回的json对象
            if (result) {
                for (var i = 0; i < result.length; i++) {
                    time.push(result[i].time);    //挨个取出时间并填入时间数组
                }
                for (var i = 0; i < result.length; i++) {
                    value.push(result[i].value);
                    //挨个取出value并填入状态数组
                }

                myChart.hideLoading();    //隐藏加载动画
                myChart.setOption({        //加载数据图表
                    xAxis: {
                        name: '时间',
                        data: time
                    },

                    series: [{
                        // 根据名字对应到相应的系列
                        
                        name: '状态:',
                        data: value

                    }]
                });

            }

        },
        error: function (errorMsg) {
            //请求失败时执行该函数
            alert("图表请求数据失败!");
            myChart.hideLoading();
        }
    })
})