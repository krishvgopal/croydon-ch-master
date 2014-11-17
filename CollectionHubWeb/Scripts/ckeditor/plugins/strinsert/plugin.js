CKEDITOR.plugins.add('strinsert',
{
    requires : ['richcombo'],
    init : function( editor )
    {
        editor.ui.addRichCombo('strinsert',
        {
            label: 'Merge Fields',
            title: 'Merge Fields',
            voiceLabel: 'Merge Fields',
            className: 'wide_combo',
            multiSelect: false,
            panel:
            {
                css: [ editor.config.contentsCss, CKEDITOR.skin.getPath('editor') ],
                voiceLabel: editor.lang.panelVoiceLabel
            },

            init: function () {

                var objectPass = this;
                //var editorId = editor.ui.editor.name;
          
                this.startGroup("Insert Merge Field");
                
                $.ajax({
                    async: false,
                    type: "POST",
                    url: "DataService.aspx/GetDataMergeFields",
                    data: "{'viewName':'" + $("#templateName").attr('ViewTable') + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (result) {
                        if (result.hasOwnProperty("d")) { result = result.d; }
                        $.each(result, function(index, value) {
                            objectPass.add(value.Fieldname, value.Fieldname, value.Fieldname);
                        });
                    }
                });
            },
            onClick: function( value )
            {
                editor.focus();
                editor.fire( 'saveSnapshot' );
                editor.insertHtml(value);
                editor.fire( 'saveSnapshot' );
            }
        });
    }
});