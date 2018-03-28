


var playgroundSpecial = (function () {

    var scriptMan = null;

    function newScriptManager() {

        var allScripts = [];

        function newState() {

            var done = false;

            return {

                IsReady: function () { return done; },

                Finished: function (r) { done = true; }
            }
        }

        function loadScript(url) {

            var state = newState();

            allScripts.push(state);

            var head = document.getElementsByTagName('head')[0];

            var s = document.createElement('script');
            s.type = "text/javascript"; s.language = "javascript"; s.src = url;

            s.addEventListener('load', function () { state.Finished(); }, false);

            head.appendChild(s);
        }

        function callContinuation(next) {

            var ready = true;

            for (var n = 0; n < allScripts.length; n++) {

                ready = ready && allScripts[n].IsReady();

                if (!ready) break;
            }

            if (ready) { next(); }

            return ready;
        }

        return {

            Load: loadScript,

            Then: function (next) {

                var done = false;
                var delay = 0;

                function whileNotDone() {

                    try {
                        done = callContinuation(next);
                    } catch (x) {
                        done = true;
                        Aspectize.ReportException(x);
                    }

                    if (!done) {
                        window.setTimeout(whileNotDone, delay); delay += 50;
                    }
                }

                whileNotDone();
            }
        };
    }
  
    function init(initSvcName) {

        var host = Aspectize.Host;

        host.ResourcesServiceName = initSvcName;

        var versionKey = host.UrlArgs.Id;

        var url = window.location.href.split('?')[0];
        var urlDecoder = /^(.*?)app\.ashx$/;
        var match = urlDecoder.exec(url);

        if (match && versionKey) {            

            window.AspectizeBuildDynamicViews = null;

            var cmdExt = ".js.cmd.ashx?versionKey=" + versionKey;
            var svcPath = match[1] + initSvcName;

            var cmdLib = svcPath + '.LoadJsLibrary' + cmdExt;
            var cmdViews = svcPath + '.LoadJsViews' + cmdExt;
            var cmdMain = svcPath + '.LoadMain' + cmdExt;

            scriptMan = newScriptManager();

            scriptMan.Load(cmdLib);
            scriptMan.Load(cmdViews);
            scriptMan.Load(cmdMain);
        }       
    }
    function run(next) {

        if (scriptMan) scriptMan.Then(next);
    }

    return { Init: init, Run: run };

})();

Aspectize.OnMainLoaded = function () {

    playgroundSpecial.Init('AppInitService');
};

function Main() {

    playgroundSpecial.Run(function () {

        sMain();

        var style = document.createElement('style');
        style.type = 'text/css';
        style.innerHTML = Aspectize.Host.ExecuteCommand('Server/AppInitService.LoadCSS', Aspectize.Host.UrlArgs.Id);
        document.getElementsByTagName('head')[0].appendChild(style);
    });
}
