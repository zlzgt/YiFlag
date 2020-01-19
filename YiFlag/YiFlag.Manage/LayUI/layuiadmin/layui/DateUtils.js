Date.prototype.toLocaleString = function () {
    var y = this.getFullYear();
    var m = this.getMonth() + 1;
    m = m < 10 ? '0' + m : m;
    var d = this.getDate();
    d = d < 10 ? ("0" + d) : d;
    var h = this.getHours();
    h = h < 10 ? ("0" + h) : h;
    var M = this.getMinutes();
    M = M < 10 ? ("0" + M) : M;
    var S = this.getSeconds();
    S = S < 10 ? ("0" + S) : S;
    return y + "-" + m + "-" + d + " " + h + ":" + M + ":" + S;
};
function getDateStr(dd) {

  if(dd===null)
  {      return '';}
    else {
         if (dd.indexOf('Date') > -1) {
    
            var datestr = dd.replace('/Date(', '').replace(')/', '');

            return new Date(parseInt(datestr)).toLocaleString();
        } else {
            return layui.util.toDateString(dd, 'yyyy-MM-dd HH:mm');


        }
    }
}
/// <summary>
/// 格式化文件大小的JS方法
/// </summary>
/// <param name="filesize">文件的大小,传入的是一个bytes为单位的参数</param>
/// <returns>格式化后的值</returns>
function renderSize(filesize) {
    if (null == value || value == '') {
        return "0 Bytes";
    }
    var unitArr = new Array("Bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB");
    var index = 0;
    var srcsize = parseFloat(value);
    index = Math.floor(Math.log(srcsize) / Math.log(1024));
    var size = srcsize / Math.pow(1024, index);
    size = size.toFixed(2);//保留的小数位数
    return size + unitArr[index];
}