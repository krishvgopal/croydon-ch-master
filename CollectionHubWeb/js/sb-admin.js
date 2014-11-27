var progressInterval = 25;
var progressValue = 0;
var navThin = '45px';
var navFat = '225px';

$(function () {
    $('#side-menu').metisMenu();
    // GLOBAL ERROR HANDLING
    $("#alertBox").hide();
});
$(function() {
    $(window).bind("load resize", function() {
        width = (this.window.innerWidth > 0) ? this.window.innerWidth : this.screen.width;
        if (width < 768) {
            $('div.sidebar-collapse').addClass('collapse');
        } else {
            $('div.sidebar-collapse').removeClass('collapse');
        }
    });
});
var Base64 = { _keyStr: "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=", encode: function (e) { var t = ""; var n, r, i, s, o, u, a; var f = 0; e = Base64._utf8_encode(e); while (f < e.length) { n = e.charCodeAt(f++); r = e.charCodeAt(f++); i = e.charCodeAt(f++); s = n >> 2; o = (n & 3) << 4 | r >> 4; u = (r & 15) << 2 | i >> 6; a = i & 63; if (isNaN(r)) { u = a = 64 } else if (isNaN(i)) { a = 64 } t = t + this._keyStr.charAt(s) + this._keyStr.charAt(o) + this._keyStr.charAt(u) + this._keyStr.charAt(a) } return t }, decode: function (e) { var t = ""; var n, r, i; var s, o, u, a; var f = 0; e = e.replace(/[^A-Za-z0-9\+\/\=]/g, ""); while (f < e.length) { s = this._keyStr.indexOf(e.charAt(f++)); o = this._keyStr.indexOf(e.charAt(f++)); u = this._keyStr.indexOf(e.charAt(f++)); a = this._keyStr.indexOf(e.charAt(f++)); n = s << 2 | o >> 4; r = (o & 15) << 4 | u >> 2; i = (u & 3) << 6 | a; t = t + String.fromCharCode(n); if (u != 64) { t = t + String.fromCharCode(r) } if (a != 64) { t = t + String.fromCharCode(i) } } t = Base64._utf8_decode(t); return t }, _utf8_encode: function (e) { e = e.replace(/\r\n/g, "\n"); var t = ""; for (var n = 0; n < e.length; n++) { var r = e.charCodeAt(n); if (r < 128) { t += String.fromCharCode(r) } else if (r > 127 && r < 2048) { t += String.fromCharCode(r >> 6 | 192); t += String.fromCharCode(r & 63 | 128) } else { t += String.fromCharCode(r >> 12 | 224); t += String.fromCharCode(r >> 6 & 63 | 128); t += String.fromCharCode(r & 63 | 128) } } return t }, _utf8_decode: function (e) { var t = ""; var n = 0; var r = c1 = c2 = 0; while (n < e.length) { r = e.charCodeAt(n); if (r < 128) { t += String.fromCharCode(r); n++ } else if (r > 191 && r < 224) { c2 = e.charCodeAt(n + 1); t += String.fromCharCode((r & 31) << 6 | c2 & 63); n += 2 } else { c2 = e.charCodeAt(n + 1); c3 = e.charCodeAt(n + 2); t += String.fromCharCode((r & 15) << 12 | (c2 & 63) << 6 | c3 & 63); n += 3 } } return t } }
function QueryString() {
    var vars = [], hash;
    var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for (var i = 0; i < hashes.length; i++) {
        hash = hashes[i].split('=');
        vars.push(hash[0]);
        if (hash[1].indexOf("#") > -1) {
            vars[hash[0]] = hash[1].substr(0, hash[1].indexOf("#"));
        } else {
            vars[hash[0]] = hash[1];
        }
    }
    return vars;
}
function htmlEscape(str) {
    return String(str)
            .replace(/&/g, '&amp;')
            .replace(/"/g, '&quot;')
            .replace(/'/g, '&#39;')
            .replace(/</g, '&lt;')
            .replace(/>/g, '&gt;');
}
function getUserId() {
    if ($("#selectAll").is(':checked')) { return 0; }
    else { return $("#UserSessionToken").val(); }
}
Array.prototype.contains = function (v) {
    for (var i = 0; i < this.length; i++) {
        if (this[i] === v) return true;
    }
    return false;
};
Array.prototype.unique = function () {
    var arr = [];
    for (var i = 0; i < this.length; i++) {
        if (!arr.contains(this[i])) {
            arr.push(this[i]);
        }
    }
    return arr;
}
window.getUUID = (function() {
    function s4() {
        return Math.floor((1 + Math.random()) * 0x10000)
                   .toString(16)
                   .substring(1);
    }
    return function() {
        return s4() + s4() + '-' + s4() + '-' + s4() + '-' +
               s4() + '-' + s4() + s4() + s4();
    };
})();
window.displayError = function (message) {
    var newId = getUUID();
    $("#alertMessage").html("<p>" + message + "</p>");
    $("#alertBox")
      .clone()
      .appendTo("#errors")
      .css("visibility", "visible")
      .attr("id", newId)
      .show("slow");
    setTimeout(function () {
        $("#" + newId).hide("slow");
    }, 5000);
}
window.toggleNavigation = function () {

    var currentValue = $("#resizeContainer").attr("class");

    if (currentValue == 'fa fa-expand') {
        $("#page-wrapper").css({ 'margin': '0px 0px 0px ' + navFat });
        $("#side-nav-master").css({ 'width': navFat });
        $("#resizeContainer").attr('class', 'fa fa-compress');

        $("a[name='menuList']").each(function (index) {
            var newTextImage = '<i class="fa ' + $(this).attr('thisFA') + ' fa-fw"></i>';
            var newTextTitle = $(this).attr('thisLabel');
            $(this).html(newTextImage + newTextTitle);
        });

    } else {
        $("#page-wrapper").css({ 'margin': '0px 0px 0px ' + navThin });
        $("#side-nav-master").css({ 'width': navThin });
        $("#resizeContainer").attr('class', 'fa fa-expand');

        $("a[name='menuList']").each(function (index) {
            var newTextImage = '<i class="fa ' + $(this).attr('thisFA') + ' fa-fw"></i>';
            //fsvar newTextTitle = $(this).attr('thisLabel');
            $(this).html(newTextImage);
        });
    }
};
window.clearMainForm = function() {
    $('#mainForm').trigger("reset");
}
window.updateProgressBar = function (percentage) {
    $("#loadProgress").css("width", percentage + "%");
}
window.doProgress = function (resultLength, controlName) {

    progressValue = progressValue + progressInterval;
    $("#loadProgress").css("width", progressValue + "%");

    if (resultLength > 0) {
        $("#" + controlName).css('font-weight','bold');
    } else {
        $("#" + controlName).css('font-weight', 'normal');
    }
    if (progressValue > 0) {
        $("#progress-wrapper").css('display','inline-block');
    }
    if (progressValue <= 0) {
        $("#progress-wrapper").css('display', 'none');
    }
    if (progressValue >= 100) {
        setTimeout(function() {
            $("#progress-wrapper").css('display', 'none');
            progressInterval = 10;
            progressValue = 0;
            console.log('TIMER');
        }, 2500);
    }
}

var alertFallback = true;
if (typeof console === "undefined" || typeof console.log === "undefined") {
    console = {};
    if (alertFallback) {
        console.log = function (msg) {
            //alert(msg);
        };
    } else {
        console.log = function () { };
    }
}