//// JScript 文件

function OpenNormalWindow(Url,Name,height,width,scrollbar)
{
	var str = "scrollbars="+scrollbar+",toolbar=no,resize=no,height=" + height + ",innerHeight=" + height;
	str += ",width=" + width + ",innerWidth=" + width;
	if (window.screen) {
		var ah = screen.availHeight - 30;
		var aw = screen.availWidth - 10;
		var xc = (aw - width) / 2;
		var yc = (ah - height) / 2;
		str += ",left=" + xc + ",screenX=" + xc;
		str += ",top=" + yc + ",screenY=" + yc;
	}
	window.open(Url,Name,str);
}
function fnOpenWindowExSetTop(strUrl, nWidth, nHeight, nTop) {
    //alert(strUrl+"==="+nWidth+"==="+nHeight);
    var xMax = 0;
    xMax = screen.width;

    var xOffset = (xMax - nWidth) / 2;
    window.open(strUrl, "", "height=" + nHeight.toString() + ", width=" + nWidth.toString() + ", top=" + nTop + ", left=" + xOffset + ", toolbar=no, menubar=no, scrollbars=1, resizable=yes,location=no, status=no");
    return false;
}

function OpenResizeWindow(Url,Name,height,width,scrollbar)
{
	var str = "scrollbars="+scrollbar+",toolbar=no,resizable=yes,status=1,height=" + height + ",innerHeight=" + height;
	str += ",width=" + width + ",innerWidth=" + width;
	if (window.screen) {
		var ah = screen.availHeight - 30;
		var aw = screen.availWidth - 10;
		var xc = (aw - width) / 2;
		var yc = (ah - height) / 2;
		str += ",left=" + xc + ",screenX=" + xc;
		str += ",top=" + yc + ",screenY=" + yc;
	}
	window.open(Url,Name,str);
}
function OpenMaxWin(url)
{
   var w=screen.availWidth;
   var h=screen.availHeight;
   window.open(url,'',"width="+w+",height="+h+",left=0,top=0,toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1");
}

function OpenMaxWin2(url)
{
   var w=screen.availWidth-8;
   var h=screen.availHeight;
   window.open(url,'',"width="+w+",height="+h+",left=0,top=0,toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1");
}

function OpenDialogWindow(Url,Width,Height,WindowObj)
{
	var ReturnStr=showModalDialog(Url,WindowObj,'dialogWidth:'+Width+'px;dialogHeight:'+Height+'px; dialogTop: ' + (screen.availHeight - Height - 30) / 2 + 'px;dialogLeft: ' + (screen.availWidth - Width - 10) / 2 + 'px; edge: Raised; center: Yes; help: Yes; resizable: Yes; status: Yes;');
	return ReturnStr;
}

function CheckAll(checkAllBox)
{			
	var actVar = checkAllBox.checked ;
	for(i=0;i< document.all.length;i++)
	{
	    e=document.all.item(i)
		if ( e.type=='checkbox' && e.disabled==false)
			e.checked= actVar ;
	}
}

function CheckAllByID(checkAllBox,intN)
{
	var actVar = checkAllBox.checked ;
	for(i=0;i< document.all.length;i++)
	{
	    e=document.all.item(i)
		if ( e.type=='checkbox' && e.disabled==false && e.id=='RecID'+intN)
		{		  
		  e.checked= actVar;		  
		}	
	}
}

function DeleteOne(url,selID)
{
    if(selID !=""){
		if(confirm('您执行的是删除操作，确定删除吗？'))
		{			
			if(url.indexOf('?')>-1)
			{
			    window.location = url+'&action=Delete&selID='+selID;
			}
			else
			{
			    window.location = url+'?action=Delete&selID='+selID;
			}
			return false;
		}
		else
		{
		    return false;
		}
	}
	else
	{
	    return false;
	}
}

function DelRec(url){
	var selID = GetAllSelRec();	
	if(selID !=""){
		if(confirm('您执行的是删除操作，确定删除吗？'))
		{			
			if(url.indexOf('?')>-1)
			{
			    window.location = url+'&action=Delete&selID='+selID;
			}
			else
			{
			    window.location = url+'?action=Delete&selID='+selID;
			}			
			return false;
		}
		else
		{
		    return false;
		}
	}
	else
	{
	    return false;
	}
}

function OPRec(op,opName,url){
	var selID = GetAllSelRec();	
	if(selID !=""){
		if(confirm('您执行的是'+opName+'操作，确定'+opName+'吗？'))
		{			
			if(url.indexOf('?')>-1)
			{
			    window.location = url+'&action='+op+'&selID='+selID;
			}
			else
			{
			    window.location = url+'?action='+op+'&selID='+selID;
			}			
			return false;
		}
		else
		{
		    return false;
		}
	}
	else
	{
	    return false;
	}
}

function EditRec(url,width,height)
{
    var SelID=GetOneSelRec();    
    if(SelID != "")
    {
        OpenResizeWindow(url + '&selID=' + SelID, 'editWindow', height, width, 0);
	}
	return false;
}

function EditRecOneWindow(url)
{
    var SelID=GetOneSelRec();    
    if(SelID != "")
    {
	    location.href=url+'&selID='+SelID;
	}
	return false;
}

function GetOneSelRec()
{
    var SelID="0";
    var checkedcount=0;
	for(i=0;i< document.all.length;i++)
	{
	    e=document.all.item(i)
		if ( e.type=='checkbox' && e.checked==true && e.id != 'chkAll')
		{
			if (checkedcount==0)
			{
			    SelID=e.value;
			}
            checkedcount +=1;
        }				
	}
	if (checkedcount==0)
	{
	    alert("请选择您需要操作的行！");
	    return false;
	}
	else if (checkedcount>1)
	{
	    alert("不能同时选择多行进行修改！");
	    return false;
	}
	return SelID;
}

function GetAllSelRec()
{
    var SelID="";    
	for(i=0;i< document.all.length;i++)
	{
	    e=document.all.item(i)
		if ( e.type=='checkbox' && e.checked==true && e.id != 'chkAll')
		{			
			SelID+=e.value+',';			            
        }				
	}
	if (SelID=='')
	{
	    alert("请选择您需要操作的行！");
	    return false;
	}	
	return SelID;
}

function JumpPage(PageUrl,TranStr)
{
    var element = document.getElementById("txtJumpPage");
    if(isNaN(element.value))
    {
        alert("请输入数字！");
        element.focus;
        return false;
    }
    else
    {
        window.location = PageUrl+'?Page='+element.value+TranStr;
		return false;
    }
}

function initXMLView(xslPath)
{
    try
    {
        var xmlDom = new ActiveXObject("Microsoft.XMLDOM")
        var xml = document.getElementById("xmlFile");
	    xmlDom.async = false;
	    xmlDom.loadXML (xml.value);

	    var xslDom = new ActiveXObject("Microsoft.XMLDOM");
	    xslDom.async = false		;
	    xslDom.load (xslPath);
	    var source = document.getElementById("xmlSource");

	    source.innerHTML = xmlDom.transformNode(xslDom);  
    }
    catch(e)
    {
    }

}

function initXMLContentView(xslPath)
{
    try
    {
        var xmlDom = new ActiveXObject("Microsoft.XMLDOM")
        var xml = document.getElementById("xmlFile");
	    xmlDom.async = false;
	    xmlDom.loadXML (xml.value);

	    var xslDom = new ActiveXObject("Microsoft.XMLDOM");
	    xslDom.async = false		;
	    xslDom.load (xslPath);
	    var source = document.getElementById("xmlSource");

	    source.innerHTML = xmlDom.transformNode(xslDom);  
	    
	    var content = document.getElementById("xmlContent");   
        var nodeCDATA;
        nodeCDATA = xmlDom.getElementsByTagName("NewContent");
        //alert(nodeCDATA.length);   
        if(nodeCDATA.length>0)
            content.innerHTML = nodeCDATA[0].text;
    }
    catch(e)
    {
    }

}

function GetRadioBtnListValue(rbtnName) {
    var e = document.getElementsByName(rbtnName);
    for (var i = 0; i < e.length; i++) {
        if (e.item(i).checked && e.item(i).type == "radio") {
            return e.item(i).value;
        }
    }
    return "";
} 

function showprocesspic(modelid)
{
     OpenResizeWindow("../WorkFlowPage/ShowProcessPic.aspx?modelid="+modelid,"processDetail",450,900,'yes');
}

function showdetail(processid)
{
     OpenResizeWindow("../WorkFlowPage/ProcessDetail.aspx?processid="+processid,"processDetail",300,620,'yes');
}

function dw(s){
	document.write(s);
}

function isNull(_sVal){
	return (_sVal === "" || _sVal == null || _sVal == "undefined");
}

function adjustImg(src) {
	try{
		var lw=screen.width-80;
		if(parent!=undefined) {
			if (parent.frmRight.innerWidth!=undefined)
				lw = parent.frmRight.innerWidth - 60;
			else if (parent.document.all.right.width!=undefined)
				lw = parent.document.all.right.width - 60;			
		}
		if (src.width>lw) src.width=lw;
	}catch(e) {}
}

String.prototype.trim = function(){
	return this.replace(/(^[ |　]*)|([ |　]*$)/g, "");
}

String.prototype.cn_length = function(){
	var i, sum;
	sum = 0;
	for(i=0; i < this.length; i++){
		sum ++;
		if (this.charCodeAt(i) > 255){
	  		sum ++;
	  	}
	}
	return sum;
}
String.prototype.cn_substring = function(len){
	var a = 0;
	var tmp = "";
	for (var i = 0; i < len; i++){
		if (this.charCodeAt(i) > 255){
			a += 2;
		}
		else{
			a++;
		}
		if(a > len){
			return tmp;
		}
		tmp += this.charAt(i); 
	}
	return tmp;
}

function strlen(s)
{
    if(typeof(s.cn_length) == 'function') return s.cn_length();
    return s.length+s.replace(/[\x00-\x80]/g,'').length;
}

function substr(s, len)
{
    if(typeof(s.cn_substring) == 'function')
    return  s.cn_substring(len);
    var a = 0;
    var tmp = s;
    if(strlen(s) <= len)return s;
    var leng = s.length;
    var i = 0,j = leng,k,l;

    for(i = leng,k = len/2 ; i > k; i--)
    {
        tmp = substring(0, i );
        if(strlen(tmp) <= len)break;
    }
	return tmp;
}

function GetRadioBtnListValue(rbtnName)
{
    var e = document.getElementsByName(rbtnName);
    for(var i=0;i<e.length;i++ )
    {                
        
        if(e.item(i).checked && e.item(i).type=="radio")
        {                    
            return e.item(i).value ;
        }
    }  
    return "";
} 

function writeShortStr(s)
{
    if(window.screen.width <= 1024)
    {
        var newStr = s;
        if(strlen(s) > 60)
        {
            var str = s.replace(/<a.*?>(.*)<\/a>/ig,"$1");        
            newStr = substr(str,60)+"...";
        }
        document.write(newStr);
        //document.write(s.replace(str,newStr));
    }
    else
    {
        document.write(s);
    }
}

function IsNum(strvalue)
{
     for(var i = 0; i < strvalue.length; i++)
     {
        if(strvalue.charAt(i) < "0" || strvalue.charAt(i) > "9")
           return false;
     }
     return true;        
}

function CheckJump()
{
    var page=window.document.getElementById("JumpPage").value.trim();
    if(page=="")  {alert("请输入页码"); return false;}
    if(!IsNum(page))
    {
        alert("页码格式错误");
        window.document.getElementById("JumpPage").value=""; 
        return false; 
    }
    var total=window.document.getElementById("TotalPage").innerText;
    if(parseInt(total)<parseInt(page))
    {
        alert("不能超出最大页码");
        window.document.getElementById("JumpPage").value=""; 
        return false;
    }
 
    return true;   
}

function OpenSelector(IDControl,NameControl,ctlstring)
{
      document.getElementById("selectNameControl").value = NameControl;
      document.getElementById("selectIDControl").value = IDControl;     
	  var w=550;
	  var h=500;
	  var lPos = (screen.width) ? (screen.width-w)/2 : 0;
	  var tPos = (screen.height) ? (screen.height-h)/2 : 0;  
	  if(ctlstring.indexOf("&")<0)
	  { ctlstring="parapath="+ctlstring+"&paraclass=U&returnfullpath=0&backid=0"; }  
	  varurl="../UserSelector/UserSelector.aspx?"+ctlstring;  
      window.open(varurl, "userselector", "height="+h+", width="+w+", left="+lPos+", top="+tPos+", toolbar=0, location=0,directories=0,status=0,menuBar=0,scrollBars=0,resizable=1");                               
}

function addToDestListCommon(selectstring)
{
     //window.alert("backid:"+backid);
    //window.alert("selectstring:"+selectstring);
    if (!isNull(document.getElementById("hidChangeManger"))) {
       
        document.getElementById("hidChangeManger").value = selectstring;
        document.getElementById("btnAddManager").click();

    }
    
	 var IdText = "";
	 var NameText = "";
     if(selectstring=="")
	 {
	    document.getElementById(document.getElementById("selectNameControl").value).value = NameText;
	    document.getElementById(document.getElementById("selectIDControl").value).value = IdText;
	    return;
	 }
    
	 var infoarr = selectstring.split(",");
     for(var i=0;i<infoarr.length;i++)
     {
        var infodetail = infoarr[i].split("|");
        if(infodetail[2] == "U")
        {
            if(i == infoarr.length - 1)
            {
                IdText = IdText + infodetail[0];
                NameText = NameText + infodetail[1];
            }
            else
            {
                IdText = IdText + infodetail[0] +",";
                NameText = NameText + infodetail[1] +",";
            }
        }
        else if(infodetail[2] == "D")
        {
            if(i == infoarr.length - 1)
            {
                IdText = IdText + infodetail[0];
                NameText = NameText + infodetail[1];
            }
            else
            {
                IdText = IdText + infodetail[0] +",";
                NameText = NameText + infodetail[1] +",";
            }
         }
      }

	document.getElementById(document.getElementById("selectNameControl").value).value = NameText;
	document.getElementById(document.getElementById("selectIDControl").value).value = IdText;

}

function OpenDeptSelector(IDControl, NameControl, ctlstring) {
    document.getElementById("selectNameControl").value = NameControl;
    document.getElementById("selectIDControl").value = IDControl;
    var w = 500;
    var h = 400;
    var lPos = (screen.width) ? (screen.width - w) / 2 : 0;
    var tPos = (screen.height) ? (screen.height - h) / 2 : 0;
    if (ctlstring.indexOf("&") < 0)
    { ctlstring = "parapath=" + ctlstring + "&returnfullpath=0&backid=0"; }
    varurl = "../UserSelector/DeptSelector.aspx?" + ctlstring;
    window.open(varurl, "userselector", "height=" + h + ", width=" + w + ", left=" + lPos + ", top=" + tPos + ", toolbar=0, location=0,directories=0,status=0,menuBar=0,scrollBars=0,resizable=1");
}
function addDeptToDestList(selectstring) {
    //window.alert("backid:"+backid);
    //window.alert("selectstring:"+selectstring);
    var IdText = "";
    var NameText = "";
    if (selectstring == "") {
        document.getElementById(document.getElementById("selectNameControl").value).value = NameText;
        document.getElementById(document.getElementById("selectIDControl").value).value = IdText;
        return;
    }

    var infoarr = selectstring.split(",");
    for (var i = 0; i < infoarr.length; i++) {
        var infodetail = infoarr[i].split("|");        
        if (i == infoarr.length - 1) {
            IdText = IdText + infodetail[0];
            NameText = NameText + infodetail[1];
        }
        else {
            IdText = IdText + infodetail[0] + ",";
            NameText = NameText + infodetail[1] + ",";
        }
        
    }

    document.getElementById(document.getElementById("selectNameControl").value).value = NameText;
    document.getElementById(document.getElementById("selectIDControl").value).value = IdText;

}

//对返回值的处理  backid:0正常 1会签 2承办 3协办
function addToDestList(selectstring,backid)
{     
	 addToDestListCommon(selectstring);
}

function fnOpenWindowEx(strUrl, nWidth, nHeight) {
    //alert(strUrl+"==="+nWidth+"==="+nHeight);
    var xMax = 0;
    var yMax = 0;
    xMax = screen.width;
    yMax = screen.height;

    var xOffset = (xMax - nWidth) / 2;
    var yOffset = (yMax - nHeight) / 2;
    var sname = strUrl.indexOf('CreditRiskRating/Approve.aspx') > -1 ? strUrl.replace(/\/|\.|:|\&|\?|=/g, '') : "";
    window.open(strUrl, sname, "height=" + nHeight.toString() + ", width=" + nWidth.toString() + ", top=" + yOffset + ", left=" + xOffset + ", toolbar=no, menubar=no, scrollbars=1, resizable=yes,location=no, status=no");
    return false;
}

//判断是否是数字（小数）
function checkNum(obj)
{
    var re = /^-?[1-9]*(\.\d*)?$|^-?d^(\.\d*)?$/;
    if (obj.value !=0 && !re.test(obj.value))
    {
        if (isNaN(obj.value))
        {
            alert("非法数字");
            obj.value = "";
            obj.focus();
            return false;
        }
    }
}

//判断是否是数字（小数），并格式化显示
function CheckAndFormatMoney(obj)
{
    re = /,/g
    s = obj.value.trim().replace(re, "");
    var re = /^-?[1-9]*(\.\d*)?$|^-?d^(\.\d*)?$/;
    if (s != 0 && !re.test(s))
    {
        if (isNaN(s))
        {
            alert("非法数字");
            obj.value = "";
            obj.focus();
            return false;
        }
    }

    if(s.trim() != "")
        FormatMoney(obj);
    
}


//判断是否是数字（小数），并格式化显示
function FormatMoney(obj) {

    re = /,/g
    s = obj.value.replace(re, "");

    if (s < 0 && s.indexOf(".") == -1)
        s = s + ".00";    
        
    s = s.toString().replace(/^(\d*)$/, "$1.");
    s = (s + "00").replace(/(\d*\.\d\d)\d*/, "$1");
    s = s.replace(".", ",");
    var re1 = /(\d)(\d{3},)/;
    while (re1.test(s))
        s = s.replace(re1, "$1,$2");
    obj.value = s.replace(/,(\d\d)$/, ".$1");
   
}

//从当前URL中提取某个参数的值
function getQueryString(key)
{
    var value = "";
    //获取当前文档的URL,为后面分析它做准备
    var sURL = window.document.URL.replace("#","");
    //URL中是否包含查询字符串
    if (sURL.indexOf("?") > 0)
    {
        //分解URL,第二的元素为完整的查询字符串
        //即arrayParams[1]的值为【first=1&second=2】
        var arrayParams = sURL.split("?");
        //分解查询字符串
        //arrayURLParams[0]的值为【first=1 】
        //arrayURLParams[2]的值为【second=2】
        var arrayURLParams = arrayParams[1].split("&");

        //遍历分解后的键值对
        for (var i = 0; i < arrayURLParams.length; i++)
        {
            //分解一个键值对
            var sParam = arrayURLParams[i].split("=");
            if ((sParam[0] == key) && (sParam[1] != ""))
            {
                //找到匹配的的键,且值不为空
                value = sParam[1];
                break;
            }
        }
    }
    return value;
}

function TableToExcel()
{
    var sTableName = "StatTable";
    if (arguments[0]) {
        sTableName = arguments[0];
    }
    document.getElementById("hideTableContent").value = document.getElementById(sTableName).outerHTML.replace("border=0", "border=1");
}

function setHeight()
{
    ResetHeight(436);
}

function ResetHeight(MinValue)
{
    try
    {
        if (this.document.body.clientHeight > MinValue)
        {
            parent.document.getElementById("ListFrame").height = this.document.body.clientHeight;
        }
        else
        {
            parent.document.getElementById("ListFrame").height = MinValue;
        }

    } catch (e) { }
}