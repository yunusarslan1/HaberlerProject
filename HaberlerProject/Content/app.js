var successNotify = function (title, text) {
    new PNotify({
        title: title,
        text: text,
        type: 'success'
    });
}

var errorNotify = function (title, text) {
    new PNotify({
        title: title,
        text: text,
        type: 'error'
    });
}

var infoNotify = function (title, text) {
    new PNotify({
        title: title,
        text: text,
        icon: ''
    });
}


if (tempnotify) {
    console.log("tempnotify", tempnotify);

    var t = tempnotify.split(',');
    var status = t[0];
    var process = t[1];
    var text = t[2];

    var title = "";
    var message = "";
    if (process == "edit") {
        title = "Güncelleme işlemi";
        message = text + (status == "success" ? " başarıyla güncellendi" : " güncellenemedi");
    } else {
        title = process == "create" ? "Ekleme işlemi" : "Silme işlemi";
        message = status == "success" ? text + (process == "create" ? " başarıyla eklendi" : " başarıyla silindi") : text + (process == "create" ? " eklenemedi" : " silinemedi");
    }

    if (status == "success") {
        successNotify(title, message);
    } else if (status == "error") {
        errorNotify(title, message);
    }



}

function GetAppDomain() {
    var port = "";
    if (window.location.port != null && window.location.port != "") {
        if (window.location.port != 80) {
            port = ":" + window.location.port;
        }
    }
    var rootPath = window.location.pathname.split('/');
    var path = "";
    if (rootPath.length > 0) {
        path = rootPath[1];
    }
    return window.location.protocol + "//" + window.location.hostname + port + "/" + path + "/";
}

function GetDomain() {
    var port = "";
    if (window.location.port !== null && window.location.port !== "") {
        if (window.location.port !== 80) {
            port = ":" + window.location.port;
        }
    }
    var rootPath = window.location.pathname.split('/');
    var path = "";
    //if (rootPath.length > 0) {
    //    path = rootPath[1];
    //}
    return window.location.protocol + "//" + window.location.hostname + port + "/";
}

function objectifyForm(formArray) {
    var returnArray = {};
    for (var i = 0; i < formArray.length; i++) {
        returnArray[formArray[i]['name']] = formArray[i]['value'];
    }
    return returnArray;
}

var moneyFormatted = function (value) {
    return '$' + parseFloat(value, 10).toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, "$1,").toString()
}

function pad(str, max) {
    str = str.toString();
    return str.length < max ? pad("0" + str, max) : str;
}

var dateFormatted = function (value) {
    if (value) {
        var date = new Date(parseInt(value.substr(6, 13), 10));
        var curr_month = pad((date.getMonth() + 1), 2);
        var curr_date = pad(((date.getDate())), 2);
        var curr_year = date.getFullYear();
        return curr_year + "/" + curr_month + "/" + curr_date;
    } else return "";
}

function openModal(content, opt) {
    opt = Object.assign({
        autoOpen: false,
        modal: true,
        resizable: false,
        width: 550,
        height: 650,
        title: 'Details',
        data: [],
        show: { effect: 'clip', duration: 350, times: 3 },
        hide: { effect: 'clip', duration: 350, times: 3 },
        position: ["center", "center"],
        success: function (data) {
            console.log('success', data);
        },
        error: function (error) {
            console.log('error', error);
        }
    }, opt);
    var dialogItem = $("<div></div>");
    $('body').append(dialogItem);
    $(dialogItem).html(content);
    $(dialogItem).dialog(opt).dialog('open');
    $(dialogItem).on('dialogclose', function (event) {
        $(this).remove();
    });
    $(dialogItem).on('dialog-success', opt.success);
    $(dialogItem).on('dialog-error', opt.error);
    $(dialogItem).on("click", ".close-dialog", function () {
        $(dialogItem).dialog("close");
    });
    $(dialogItem).data('data', JSON.stringify(opt.data));
}