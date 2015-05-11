/// <reference path="S:\Delivery\Aspectize.core\AspectizeIntellisense.js" />

Global.AceClientService = {

    aasService: 'AceClientService',
    aasPublished: true,

    InsertCode: function (editorName, code) {
        var editor = ace.edit(editorName);
        editor.insert(code);
    }
}

Aspectize.Extend("AceEditor", {

    Properties: { Value: '', Mode: 'javascript', Theme: 'chrome', ShowGutter: true, HighLightActiveLine: false },
    Events: ['rr'],

    Init: function (elem) {

        var editor = ace.edit(elem.id);
        editor.setTheme("ace/theme/" + Aspectize.UiExtensions.GetProperty(elem, 'Theme'));
        editor.getSession().setMode("ace/mode/" + Aspectize.UiExtensions.GetProperty(elem, 'Mode'));
        editor.renderer.setShowGutter(Aspectize.UiExtensions.GetProperty(elem, 'ShowGutter'));
        editor.setHighlightActiveLine(Aspectize.UiExtensions.GetProperty(elem, 'HighLightActiveLine'));
        editor.setShowPrintMargin(false);

        editor.setOptions({
            enableBasicAutocompletion: true,
            enableSnippets: true,
            enableLiveAutocompletion: false
        });

        editor.on("change", function (e) {
            Aspectize.UiExtensions.ChangeProperty(elem, 'Value', editor.getValue());
        });


        var langTools = ace.require("ace/ext/language_tools");
        //var rhymeCompleter = {
        //    getCompletions: function (editor, session, pos, prefix, callback) {
        //        if (prefix.length === 0) { callback(null, []); return }
        //        $.getJSON(
        //            "http://rhymebrain.com/talk?function=getRhymes&word=" + prefix,
        //            function (wordList) {
        //                // wordList like [{"word":"flow","freq":24,"score":300,"flags":"bc","syllables":"1"}]
        //                callback(null, wordList.map(function (ea) {
        //                    return { name: ea.word, value: ea.word, score: ea.score, meta: "rhyme" }
        //                }));
        //            })
        //    }
        //}
        //langTools.addCompleter(rhymeCompleter);

        editor.commands.on("afterExec", function (e) {
            if (e.command.name == "insertstring" && /^[\w.]$/.test(e.args)) {
                editor.execCommand("startAutocomplete")
            }
        })

        var completer = {
            getCompletions: function (editor, session, pos, prefix, callback) {
                var text = editor.getValue();
                if (prefix.length === 0) {
                    callback(null, []);
                    return;
                }
                var completions = [];
                completions.push({ caption: "Aspectize", snippet: "Aspectize", meta: "aspectize" });
                callback(null, completions);
            }
        }
        langTools.addCompleter(completer);


        Aspectize.UiExtensions.AddPropertyChangeObserver(elem, function (sender, arg) {

            var editor = ace.edit(elem.id);

            if (arg.Name === 'Value') {
                editor.getSession().setValue(arg.Value);
            } else if (arg.Name === 'Mode') {
                editor.getSession().setMode("ace/mode/" + arg.Value);
            } else if (arg.Name === 'Theme') {
                editor.setTheme("ace/theme/" + arg.Value);
            } else if (arg.Name === 'HighLightActiveLine') {
                editor.setHighlightActiveLine(arg.Value);
            }

        });
    }

});
