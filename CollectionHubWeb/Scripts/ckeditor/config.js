/**
 * @license Copyright (c) 2003-2014, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.md or http://ckeditor.com/license
 */
CKEDITOR.editorConfig = function( config ) {
    // Define changes to default configuration here. For example:
    // config.language = 'fr';
    // config.uiColor = '#AADC6E';

    // Referencing the new plugin
    // config.extraPlugins = 'MailMergeFields';
    config.extraPlugins = 'strinsert';
    config.toolbar = [
            ['Bold', 'Italic', 'Underline', 'StrikeThrough', '-', 'Undo', 'Redo', '-', 'Cut', 'Copy', 'Paste', 'Find', 'Replace', '-', 'Outdent', 'Indent', '-', 'Print'],
            ['NumberedList', 'BulletedList', '-', 'JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock'],
            ['Styles', 'Format', 'Font', 'FontSize'], '/',
            ['Image', 'Table', '-', 'Link', 'Flash', 'Smiley', 'TextColor', 'BGColor', 'Source'], ['strinsert']
        ];
    config.filebrowserBrowseUrl         = '/scripts/ckeditor/ckfinder/ckfinder.html';
    config.filebrowserImageBrowseUrl    = '/scripts/ckeditor/ckfinder/ckfinder.html?type=Images';
    config.filebrowserUploadUrl         = '/scripts/ckeditor/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Files';
    config.filebrowserImageUploadUrl    = '/scripts/ckeditor/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Images';
    config.filebrowserFlashUploadUrl = '/scripts/ckeditor/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Flash';
    //config.filebrowserFlashBrowseUrl = '/ckfinder/ckfinder.html?type=Flash';
};
