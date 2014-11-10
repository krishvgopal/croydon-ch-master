
CKEDITOR.plugins.add('MailMergeFields',
{
    init: function (editor) {
        var pluginName = 'MailMergeFields';
        editor.ui.addButton('Merge Fields',
            {
                label: 'Merge Fields Plugin',
                command: 'OpenWindow',
                icon: CKEDITOR.plugins.getPath('newplugin') + 'anchor.png'
            });
        var cmd = editor.addCommand('OpenWindow', { exec: showMyDialog });
    }
});
function showMyDialog(e) {
    window.open('/Default.aspx', 'MyWindow', 'width=800,height=700,scrollbars=no,scrolling=no,location=no,toolbar=no');
}