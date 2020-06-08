//== Class definition
function sendFile(file, editor, welEditable) {
    data = new FormData();
    data.append("file", file);
    $.ajax({
        data: data,
        type: "POST",
        url: '/Product/AddEditorImage',
        cache: false,
        contentType: false,
        processData: false,
        success: function (url) {
            $('.summernote').summernote('insertImage', url);
        }
    });
}   

var SummernoteDemo = function () {    
    //== Private functions
    var demos = function () {
        $('.summernote').summernote({
            height: 150,
            callbacks: {
                onImageUpload: function (files, editor, welEditable) {
                    sendFile(files[0], editor, welEditable);
                }    
            }
        });
    }

    return {
        // public functions
        init: function() {
            demos(); 
        }
    };
}();

//== Initialization
jQuery(document).ready(function() {
    SummernoteDemo.init();
});