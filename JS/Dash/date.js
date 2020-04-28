$(function () {
    var heatChart = echarts.init(document.getElementById("container6"));
   // var pieId = document.getElementById('container6');
    
  //  var heatChart = echarts.init(pieId)
    option = null;
    var hours = ['0', '1', '2', '3', '4', '5', '6',
        '7', '8', '9', '10', '11',
        '12', '13', '14', '15', '16', '17',
        '18', '19', '20', '21', '22', '23'];
    var days = ['星期日', '星期六', '星期五',
        '星期四', '星期三', '星期二', '星期一'];
    
    option = {
        
        title: {
            //text: '测试门数据点统计'
        },
        tooltip: {
            position: 'top'
        },
        animation: false,
        grid: {
            height: '50%',
            top: '10%'
        },
        xAxis: {
        
            type: 'category',
            data: hours,
            splitArea: {
                show: true
            }
        },
        yAxis: {
            type: 'category',
            data: days,
            splitArea: {
                show: true
            }
        },
        visualMap: {
            min: 0,
            max: 10,
            calculable: true,
            orient: 'horizontal',
            left: 'center',
            bottom: '15%'
        },
        series: [{
            name: '',
            type: 'heatmap',
            data: [],
            label: {
                show: true
            },
            emphasis: {
                itemStyle: {
                    shadowBlur: 10,
                    shadowColor:  'rgba(0, 0, 0, 0.5)'
                }
            }
        }]
    };
    heatChart.setOption(option);
    //heatChart.showLoading();

    $.ajax({
        type: "post",
        async: true,            //异步请求（同步请求将会锁住浏览器，用户其他操作必须等待请求完成才可以执行）
        url: "/Display/GetDataWeekHour",    //请求发送到GetDataWeekHour处
        data: {},
        dataType: "json",        //返回数据形式为json
        success: function (rsq) {
            //请求成功时执行该函数内容，result即为服务器返回的json对象
            //console.log("rsq", rsq);
            if (rsq.state == true) {
                heatChart.hideLoading();
                //隐藏加载动画
                var dataBody = $.parseJSON(rsq.message);
                //console.log("dataBody", dataBody);
                var dataLoaded = new Array();
                //console.log("dataLoaded", dataLoaded);
                var max = 0;
                for (var i = 0; i < 168; i++) {
                    dataLoaded[i] = new Array();
                    for (var j = 0; j < 3; j++) {
                        dataLoaded[i][j] = dataBody["value"][i][j];
                    }
                    if (dataLoaded[i][2] > max) {
                        max = dataLoaded[i][2];
                    }
                }

                dataLoaded = dataLoaded.map(function (item) {
                    return [item[1], item[0], item[2] || '-'];
                });
                heatChart.setOption({        //加载数据图表
                    visualMap: {
                        max: Math.floor(max * 0.95)
                    },
                    series: [{
                        // 根据名字对应到相应的系列
                        name: '数据点数量',
                        data: dataLoaded
                    }]
                });
            }
            else {
                alert("图表请求数据失败!");
                heatChart.hideLoading();
            }
        },
        error: function (errorMsg) {
            //请求失败时执行该函数
            alert("图表请求数据失败!");
            heatChart.hideLoading();
        }
    })
});